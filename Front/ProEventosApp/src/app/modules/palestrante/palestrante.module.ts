import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { PalestranteRouting } from './palestrante.routing';
import { PalestranteComponent } from './palestrante/palestrante.component';

@NgModule({
    declarations: [
        PalestranteComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(PalestranteRouting),

    ],
    exports: [
        PalestranteComponent
    ],
    providers: [],
})
export class PalestranteModule { }
