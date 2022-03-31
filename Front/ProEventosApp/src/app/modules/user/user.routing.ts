import { Routes } from '@angular/router';
import { AuthdGuard } from 'src/app/core/auth.guard';
import { LoginComponent } from './login/login.component';
import { PerfilComponent } from './perfil/perfil.component';
import { RegistrationComponent } from './registration/registration.component';

export const UserRouting: Routes = [
    
    { path: 'login', component: LoginComponent },
    { path: 'cadastro', component: RegistrationComponent },
    { path: 'perfil', component: PerfilComponent, canActivate: [AuthdGuard]},

];