import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContatoComponent } from './modules/contato/contato/contato.component';
import { DashboardComponent } from './modules/dashboard/dashboard/dashboard.component';
import { EventoComponent } from './modules/evento/evento/evento.component';
import { PalestranteComponent } from './modules/palestrante/palestrante/palestrante.component';
import { UserComponent } from './modules/user/user/user.component';

const routes: Routes = [
  {
    path: 'eventos',
    component: EventoComponent,
    loadChildren: () => import('./modules/evento/evento.module').then(m => m.EventoModule)
  },
  { path: 'palestrantes', component: PalestranteComponent,
    loadChildren: () => import('./modules/palestrante/palestrante.module').then(m => m.PalestranteModule) 
  },
 
  { path: 'user', component: UserComponent,
    loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule) 
  },
  
  { path: 'dashboard', component: DashboardComponent,
  loadChildren: () => import('./modules/dashboard/dashboard.module').then(m => m.DashboardModule) 
  },
  { path: 'contato', component: ContatoComponent, 
    loadChildren: () => import('./modules/contato/contato.module').then(m => m.ContatoModule) 
  },

  
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
