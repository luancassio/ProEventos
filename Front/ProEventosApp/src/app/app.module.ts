import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';


import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxCurrencyModule } from 'ngx-currency'; 
import { defineLocale } from 'ngx-bootstrap/chronos';

import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ptBrLocale } from 'ngx-bootstrap/locale';
import { SharedModule } from './shared/shared.module';
import { EventoModule } from './modules/evento/evento.module';
import { PalestranteModule } from './modules/palestrante/palestrante.module';
import { UserModule } from './modules/user/user.module';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
defineLocale('pt-br', ptBrLocale);

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule ,
    HttpClientModule,
    BrowserAnimationsModule,

    SharedModule,
    EventoModule,
    PalestranteModule,
    UserModule,

    NgbModule,
    SweetAlert2Module.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    NgxCurrencyModule,
   
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AppModule { }
