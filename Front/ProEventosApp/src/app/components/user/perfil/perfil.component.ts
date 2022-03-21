import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements!: ElementRef[];


  perfilForm!: FormGroup;

  constructor(private fb: FormBuilder) { 
    super();

    this.validationMessages = {
      title: {
        required: 'Titulo é obrigatório',
      },
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
      telephone: {
        required: 'Telefone é obrigatório',
      },
      funcOptions: {
        required: 'Função é obrigatório',
      },
      description: {
        required: 'Descrição é obrigatório',
        rangeLength: 'A Descrição deve possuir entre 10 e 300 caracteres',

      },
      password: {
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres',
      },
      confirmPassword: {
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
    
    let password = new FormControl('', [ CustomValidators.rangeLength([6, 15])]);
    
    let confirmPassword = new FormControl('', 
    [CustomValidators.rangeLength([6, 15]),  CustomValidators.equalTo(password)]);

    this.perfilForm = this.fb.group({
      title: ['', [Validators.required]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      telephone: ['', [Validators.required]],
      funcOptions: ['', [Validators.required, Validators.maxLength(15)]],
      description: ['', [Validators.required, CustomValidators.rangeLength([10, 300])]],
      password: password,
      confirmPassword: confirmPassword
    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.perfilForm);
  }
  public resetForm(): void{
    this.perfilForm.reset();
  }

}
