import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { CustomFormsModule } from 'ng2-validation';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginComponent } from './login/login.component';
import { PerfilComponent } from './perfil/perfil.component';
import { RegistrationComponent } from './registration/registration.component';
import { UserRouting } from './user.routing';
import { UserComponent } from './user/user.component';


@NgModule({
    declarations: [
        UserComponent,
        RegistrationComponent,
        PerfilComponent,
        LoginComponent
    ],
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CustomFormsModule,
        RouterModule.forChild(UserRouting),
        CommonModule,
        SharedModule,   
    ],
    exports: [

    ],
    providers: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class UserModule { }
