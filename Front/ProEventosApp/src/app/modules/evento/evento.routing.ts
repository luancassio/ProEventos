import { Routes } from '@angular/router';
import { EventoDetalheComponent } from './evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './evento-lista/evento-lista.component';


export const EventoRouting: Routes = [

    { path: 'detalhe/:id', component: EventoDetalheComponent },
    { path: 'detalhe', component: EventoDetalheComponent },
    { path: 'lista', component: EventoListaComponent },
    
];
