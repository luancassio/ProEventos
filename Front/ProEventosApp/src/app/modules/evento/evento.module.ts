import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { SharedModule } from 'src/app/shared/shared.module';

import { EventoDetalheComponent } from './evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './evento-lista/evento-lista.component';
import { EventoComponent } from './evento/evento.component';

import { EventoRouting } from './evento.routing';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from 'ngx-spinner';
import { EventoService } from 'src/app/services/evento.service';
import { LoteService } from 'src/app/services/lote.service';



@NgModule({
    declarations: [
        EventoComponent,
        EventoDetalheComponent,
        EventoListaComponent
    ],
    imports: [
        RouterModule.forChild(EventoRouting),
        CommonModule,
        ReactiveFormsModule,
        FormsModule,
        SharedModule,
        BsDatepickerModule.forRoot(),
        TooltipModule.forRoot(),
        NgxSpinnerModule,
    ],
    exports: [
        EventoComponent
    ],
    providers: [
        EventoService,
        LoteService
    ],
})
export class EventoModule { }
