import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';
import { CustomValidators } from 'ng2-validation';
import { User } from 'src/app/core/models/identity/User';
import { AccountService } from 'src/app/services/account.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements!: ElementRef[];

  user = {} as User;

  registerForm!: FormGroup;

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { 
    super();

    this.validationMessages = {
      firstName: {
        required: 'Primeiro nome é obrigatório',
      },
      lastName: {
        required: 'Ultimo nome é obrigatório',
      },
      email: {
        required: 'E-mail é obrigatório',
        email: 'Deve ser um e-mail válido'
      },
      userName: {
        required: 'Usuário é obrigatório',
        maxlength: 'Deve ter no máximo 15 caracteres'
      },
      password: {
        required: 'Senha é obrigatório',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres',
      },
      confirmPassword: {
        required: 'Informe a senha novamente',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };

    super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  ngOnInit(): void {

    this.validationForm();
  }

  validationForm(): void{
    
    let password = new FormControl('', [Validators.required, CustomValidators.rangeLength([6, 15])]);
    
    let confirmPassword = new FormControl('', 
    [Validators.required, CustomValidators.rangeLength([6, 15]),  CustomValidators.equalTo(password)]);

    this.registerForm = this.fb.group({
      primeiroNome: ['', [Validators.required]],
      ultimoNome: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      username: ['', [Validators.required, Validators.maxLength(15)]],
      password: password,
      confirmPassword: confirmPassword

    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.registerForm);
  }
  public resetForm(): void{
    this.registerForm.reset();
  }

  register():void{
    this.user = {...this.registerForm.value};
    this.accountService.register(this.user).subscribe(() =>{
        this.router.navigateByUrl('/dashboard');
      Swal.fire('Sucesso','Usuário cadastrado com sucesso!', 'success');

    }, (err) => {
      Swal.fire('Erro','Erro ao cadastrar seus dados, tente novamente', 'error');
      console.error(err);
    });
  }

}
