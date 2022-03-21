import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { FormBaseComponent } from 'src/app/shared/components/form-base/form-base.component';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  eventoForm: FormGroup;

  constructor(private fb: FormBuilder) {
    super();

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

}
