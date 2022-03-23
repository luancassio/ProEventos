import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

import { Evento } from 'src/app/core/models/interface/IEvento';
import { EventoService } from 'src/app/services/evento.service';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements!: ElementRef[];

  public eventoForm!: FormGroup;
  public evento = {} as Evento;
  public isId: boolean = false;
  public eventoId: number = 0;
  

  constructor(private fb: FormBuilder, 
              private localeService: BsLocaleService,
              private activeRoute: ActivatedRoute,
              private eventoService: EventoService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {
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

  public validationForm(): void{
    this.eventoForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', [Validators.required]],
      dataEvento: ['', [Validators.required]],
      qtdPessoa: ['', [Validators.required, Validators.max(10000)]],
      telefone: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      imageUrl: ['', [Validators.required]],
    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.eventoForm);
  }
  public resetForm(): void{
    this.eventoForm.reset();
  }

  public carregarEvento(): void{
    const eventoIdParam = this.activeRoute.snapshot.paramMap.get('id');

    // flag para saber quando vou editar ou salvar um novo evento;
    this.isId = eventoIdParam !== null ? true : false; 

    if (eventoIdParam !== null) {
        this.eventoId = +eventoIdParam;
        Swal.showLoading()
        this.eventoService.getEventoById(+eventoIdParam).subscribe((evento: Evento) =>{
        this.evento = {...evento};
        this.eventoForm.patchValue(this.evento);
      }, (error)=>{
        console.error(error);
      }).add(() => { Swal.hideLoading(); });
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
          this.eventoService.postEvento(this.evento).subscribe(() => {
          Swal.hideLoading();
          Swal.fire('Salvo!',`Evento salvo com Sucesso.`,'success');
        }, (err) => { 
          console.error(err);
          Swal.fire('Erro', 'Erro ao salvar evento!', 'error');
        }).add(() => { Swal.hideLoading(); });
        
      }
    }
  }

}
