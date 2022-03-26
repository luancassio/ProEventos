import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { FormBaseComponent } from './components/form-base/form-base.component';
import { NavComponent } from './components/nav/nav.component';
import { TableGenericComponent } from './components/table-generic/table-generic.component';
import { TituloComponent } from './components/titulo/titulo.component';


@NgModule({
    declarations: [
        NavComponent,
        TableGenericComponent,
        TituloComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        CollapseModule.forRoot(),
        TooltipModule.forRoot(),
        BsDropdownModule.forRoot(),

    ],
    exports: [
        NavComponent,
        TableGenericComponent,
        TituloComponent
    ],
    providers: [],
})
export class SharedModule { }
