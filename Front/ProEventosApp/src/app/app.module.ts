import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { EventoComponent } from './components/evento/evento.component';
import { NavComponent } from './shared/components/nav/nav.component';
import { TituloComponent } from './shared/components/titulo/titulo.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { ContatoComponent } from './components/contato/contato.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
import { PalestranteComponent } from './components/palestrante/palestrante.component';
import { EventoDetalheComponent } from './components/evento/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './components/evento/evento-lista/evento-lista.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { CustomFormsModule } from 'ng2-validation'
import { TableGenericComponent } from './shared/components/table-generic/table-generic.component';


@NgModule({
  declarations: [
    AppComponent,
    EventoComponent,
    NavComponent,
    TituloComponent,
    PerfilComponent,
    ContatoComponent,
    DashboardComponent,
    PalestranteComponent,
    EventoDetalheComponent,
    EventoListaComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    TableGenericComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    NgxSpinnerModule,
    ToastrModule.forRoot({
      timeOut: 2500,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
      progressAnimation: 'increasing'
    }),
    FormsModule,
    CustomFormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AppModule { }
