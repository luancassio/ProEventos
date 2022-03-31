import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, FormControl, Validators, AbstractControl, AbstractControlOptions } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from 'ng2-validation';
import { UserUpdate } from 'src/app/core/models/identity/UserUpdate';
import { AccountService } from 'src/app/services/account.service';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements!: ElementRef[];

  public userUpdate = {} as UserUpdate;
  public loading = true;


  perfilForm!: FormGroup;

  constructor(
    private fb: FormBuilder, 
    public accountService: AccountService, 
    private router: Router) { 
    super();

    this.validationMessages = {
      title: {
        required: 'Titulo é obrigatório',
      },
      primeiroNome: {
        required: 'Primeiro nome é obrigatório',
      },
      ultimoNome: {
        required: 'Ultimo nome é obrigatório',
      },
      email: {
        required: 'E-mail é obrigatório',
        email: 'Deve ser um e-mail válido'
      },
      telefone: {
        required: 'Telefone é obrigatório',
      },
      funcao: {
        required: 'Função é obrigatório',
      },
      descricao: {
        required: 'Descrição é obrigatório',
        rangeLength: 'A Descrição deve possuir entre 10 e 300 caracteres',

      },
      password: {
        minLength: 'A senha deve possuir entre 6 e 15 caracteres',
        maxLength: 'A senha deve possuir entre 6 e 15 caracteres',
      },
      confirmPassword: {
        minLength: 'A senha deve possuir entre 6 e 15 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };

    super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  ngOnInit(): void {
    this.validationForm();
    this.carregarUsuario();
  }

  validationForm(): void{
    
    // let password = new FormControl('', [Validators.nullValidator, Validators.minLength(6), Validators.maxLength(15)]);
    // let confirmPassword = new FormControl('', 
    // [Validators.nullValidator, Validators.minLength(6), Validators.maxLength(15), this.camposIguaisValidator]);

    this.perfilForm = this.fb.group({
      username: [''],
      titulo: ['NaoInformado', [Validators.required]],
      primeiroNome: ['', [Validators.required]],
      ultimoNome: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      funcao: ['NaoInformado', [Validators.required, Validators.maxLength(15)]],
      descricao: ['', [Validators.required, CustomValidators.rangeLength([10, 300])]],
      password: ['', [Validators.nullValidator, Validators.minLength(6), Validators.maxLength(15)]],
      confirmPassword: ['', [Validators.nullValidator, Validators.minLength(6), Validators.maxLength(15)]]
    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.perfilForm);
  }
  public resetForm(): void{
    this.perfilForm.reset();
  }


  // TODO melhorar
  public verificarSenha(): boolean{
    const password = this.perfilForm.get('password').value;
    const confirmPassword = this.perfilForm.get('confirmPassword').value;

    if(password !== confirmPassword){
      Swal.fire('Erro', 'Senhas estão diferentes', 'error');
      return false;
    }
    return true;
  }

  private carregarUsuario(): void{

    Swal.fire('Buscando usuário');
    Swal.showLoading();;
    this.accountService.getUser().subscribe((userRetorno: UserUpdate) =>{
      console.log(userRetorno);
      this.userUpdate = userRetorno;
      this.perfilForm.patchValue(this.userUpdate);
      Swal.fire('Sucesso','usuário carregado com sucesso','success');
    }, (err) => {
      console.error(err);
      Swal.fire('Erro','Erro ao buscar usuário','error');
      this.router.navigate(['/dashboard']);
    }).add(() => {  
      Swal.hideLoading(); 

    });
  }

  public atualizarUsuario(): void{
    if (this.verificarSenha()) {
      this.loading = false;

      this.userUpdate = {...this.perfilForm.value }
      Swal.fire('Atualizando usuário');
      Swal.showLoading();
      
      this.accountService.updateUser(this.userUpdate).subscribe(() =>{
        Swal.fire('Sucesso', 'Usuário atualizado com sucesso','success');
  
      }, (err) => {
        console.error(err);
        Swal.fire('Erro', 'Erro ao atualizar usuário', 'error');
      }).add(() => {
        Swal.hideLoading();
        this.loading = true;
      });
    }

  }

}
