import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, 
         FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import Swal from 'sweetalert2';

import { Evento } from 'src/app/core/models/interface/IEvento';
import { Lote } from 'src/app/core/models/interface/ILote';
import { EventoService } from 'src/app/services/evento.service';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';
import { CustomValidators } from 'ng2-validation';
import { LoteService } from 'src/app/services/lote.service';
import { Location } from '@angular/common';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements!: ElementRef[];

  public eventoForm!: FormGroup;
  public form!: FormGroup;
  public evento = {} as Evento;
  public isId: boolean = false;
  public eventoId: number = 0;
  mytime: Date = new Date();
  public imagemURL = '/assets/imagem/UploadImage.png'
  file!: File;
  get lotes(): FormArray { return this.eventoForm.get('lotes') as FormArray }

  
  public cssValidator(campoForm: FormControl | AbstractControl): any{
    return {'is-invalid': campoForm.errors && campoForm.touched };
  }

  constructor(private fb: FormBuilder, 
              private localeService: BsLocaleService,
              private activeRoute: ActivatedRoute,
              private router: Router,
              private eventoService: EventoService,
              private loteService: LoteService,
              private location: Location) {
    super();
    this.localeService.use('pt-br');

    this.validationMessages = {
      tema: {
        required: 'Tema é obrigatório',
        minlength: 'Tema deve ter no mínimo 4 caracteres',
        maxlength: 'Tema deve ter no máximo 50 caracteres'
      },
      local: {
        required: 'Local é obrigatório',
      },
      dataEvento: {
        required: 'Data é obrigatório',
      },
      qtdPessoa: {
        required: 'Qtd. pessoas é obrigatório',
        max: 'Qtd. de pessoa deveser no máximo 10.000',
      },
      telefone: {
        required: 'Telefone é obrigatório',
      },
      email: {
        required: 'E-mail é obrigatório',
        email: 'Deve ser um e-mail válido',
      },
      imageUrl: {
        required: 'Imagem é obrigatório',
      },
      estado: {
        required: 'Informe o Estado',
      }
    

    };

    super.configurarMensagensValidacaoBase(this.validationMessages);
   }

  ngOnInit(): void {
    this.carregarEvento();
    this.validationForm();
  }

  dataPickValue(data: any){
    console.log(data, 'data')
  }

  public validationForm(): void{
    this.eventoForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', [Validators.required]],
      dataEvento: ['', [Validators.required]],
      qtdPessoa: ['', [Validators.required, Validators.max(10000)]],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      imageUrl: [''],
      lotes: this.fb.array([])
    });
  }

  adicionarLote(): void{
    this.lotes.push(this.criarLote({id: 0} as Lote));
  }

  criarLote(lote: Lote): FormGroup{
    return this.fb.group({
      id: [lote.id],
      nome:  [lote.nome, [Validators.required]],
      quantidade: [lote.quantidade, [Validators.required ,  CustomValidators.range([50,500])]],
      preco: [lote.preco, [Validators.required]],
      dataInicio: [lote.dataInicio, [Validators.required]],
      dataFim: [lote.dataFim, [Validators.required]]
    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.eventoForm);
  }
  public resetForm(): void{
    this.eventoForm.reset();
  }

  retornaTituloLote(titulo: string): string{
    return titulo === null || titulo.trim() === '' ?
      'Nome do Lote' :  titulo;

  }

  public carregarEvento(): void{
    const eventoIdParam = this.activeRoute.snapshot.paramMap.get('id');

    // flag para saber quando vou editar ou salvar um novo evento;
    this.isId = eventoIdParam !== null ? true : false; 
    console.log(this.isId);
    

    if (eventoIdParam !== null) {
        this.eventoId = +eventoIdParam;
        Swal.fire("carregando", "Carregando Informações", 'info');
        Swal.showLoading()
        this.eventoService.getEventoById(+eventoIdParam).subscribe((evento: Evento) =>{
        this.evento = {...evento};
        this.eventoForm.patchValue(this.evento);

        if (this.evento.imageUrl !== '') {
          this.imagemURL =  environment.apiURL + 'resources/images/' + this.evento.imageUrl;
        }

        this.evento.lotes.forEach(lote => { 
          this.lotes.push(this.criarLote(lote)); 
        });
      }, (error)=>{
        console.error(error);
      }).add(() => { Swal.close(); });
    }
  }

  public salvar_e_Alterar(): void{
    if (this.isId) {
      Swal.fire('Atualizando evento');
      Swal.showLoading();
      if (this.eventoForm.valid) {
          this.evento = Object.assign({}, this.eventoForm.value);
          this.eventoService.putEvento(this.evento, this.eventoId).subscribe(() => {
          Swal.hideLoading();
          Swal.fire('Atulizado!',`Evento atulizado com Sucesso.`,'success');
        }, (err) => { 
          console.error(err);
          Swal.fire('Erro', 'Erro ao atualizar evento!', 'error');
        }).add(() => { Swal.hideLoading(); });
        
      }
      
    }else{
      Swal.fire('Salvando evento');
      Swal.showLoading();
      if (this.eventoForm.valid) {
          this.evento = Object.assign({}, this.eventoForm.value);
          this.eventoService.postEvento(this.evento).subscribe((evento: Evento) => {
          Swal.hideLoading();
          Swal.fire('Salvo!',`Evento salvo com Sucesso.`,'success');
          this.router.navigate([`eventos/detalhe/${evento.id}`]);
        }, (err) => { 
          console.error(err);
          Swal.fire('Erro', 'Erro ao salvar evento!', 'error');
        }).add(() => { 
          Swal.hideLoading(); 
        });
        
      }
    }
  }

  public salvarLotes(){
    Swal.fire('Salvando Lotes');
    Swal.showLoading();
    if (this.eventoForm.controls.lotes.valid) {
      console.log(this.eventoId, this.eventoForm.value.lotes);
      
      this.loteService.SaveLotes(this.eventoId, this.eventoForm.value.lotes).subscribe(() => {
        Swal.fire('Salvo!',`Lotes salvo com Sucesso.`,'success');
        this.lotes.reset();


      }, (err) => { 
        Swal.fire('Erro', 'Erro ao salvar lotes!', 'error');
        console.error(err); 
      }).add(() => {
        // Swal.hideLoading();
        location.reload();
      })
    }
  }

  excluirLote(indice: number){
    console.log(this.lotes.get(indice + '.id')?.value);
    console.log(this.lotes.get(indice + '.nome')?.value);
    console.log(this.eventoId);
    console.log(indice);
    
    // this.lotes.removeAt(indice);
    Swal.fire({
      title: 'Deletar Lote?',
      text: `Deseja deletar o lote ${this.lotes.get(indice + '.nome')?.value}`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim',
      cancelButtonText: 'Não'
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire('Deletando Lote!');
        Swal.showLoading();
        this.loteService.deleteLote(this.eventoId, this.lotes.get(indice + '.id')?.value).subscribe((response: any) => {
            
            
          }).add(() => {
            Swal.hideLoading();
            Swal.fire('Deletado!',`Lote ${this.lotes.get(indice + '.nome')?.value} deletado com Sucesso.`,'success');
            this.lotes.removeAt(indice);
          });
      }
    }).catch((err) =>{      
      Swal.fire('Erro', `Erro ao  deletar lote, erro: ${{err}}!`, 'error');
      Swal.hideLoading();
    });
  }

  onFileChange(evt: any): void{
    const reader = new FileReader();
    reader.onload = (event: any) => this.imagemURL = event.target.result;
    this.file = evt.target.files;
    reader.readAsDataURL(evt.target.files[0]);

    this.uploadImage();
  }

  uploadImage():void {
    Swal.fire('Upload', 'Fazendo Upload da imagem', 'info');
    Swal.showLoading();

  console.log(this.eventoId, this.file, 'this.eventoId, this.file');
  
    this.eventoService.postUpload(this.eventoId, this.file).subscribe(() => {
      this.router.navigate([`eventos/detalhe/${this.eventoId}`]);
      Swal.fire('Sucesso', 'Imagem salva com sucesso', 'success');
    }, (error) => {
      
      Swal.fire('Error', 'Error ao salvar imagem com sucesso', 'error');
      console.error(error);
    } )
    .add(() =>{
      Swal.hideLoading();
    });
  }
}
