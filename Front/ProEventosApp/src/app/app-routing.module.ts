import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContatoComponent } from './components/contato/contato.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EventoDetalheComponent } from './components/evento/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './components/evento/evento-lista/evento-lista.component';
import { EventoComponent } from './components/evento/evento.component';
import { PalestranteComponent } from './components/palestrante/palestrante.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { UserComponent } from './components/user/user.component';

const routes: Routes = [
  { path: 'eventos', redirectTo: 'eventos/lista' },
  { 
    path: 'eventos', component: EventoComponent,
    children: [
      { path: 'detalhe/:id', component: EventoDetalheComponent },
      { path: 'detalhe', component: EventoDetalheComponent },
      { path: 'lista', component: EventoListaComponent },
    ] 
  },
  { 
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'cadastro', component: RegistrationComponent }
    ]
  },
  { path: 'user/perfil', component: PerfilComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'palestrantes', component: PalestranteComponent },
  { path: 'contato', component: ContatoComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
