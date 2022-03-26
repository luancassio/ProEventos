import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { ContatoRouting } from './contato.routing';
import { ContatoComponent } from './contato/contato.component';


@NgModule({
    declarations: [
        ContatoComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(ContatoRouting),

    ],
    exports: [
        ContatoComponent
    ],
    providers: [],
})
export class ContatoModule { }
