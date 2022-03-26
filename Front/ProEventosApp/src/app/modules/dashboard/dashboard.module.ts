import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { DashboardRouting } from './dashboard.routing';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    declarations: [
        DashboardComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule.forChild(DashboardRouting),

    ],
    exports: [
        DashboardComponent
    ],
    providers: [],
})
export class DashboardModule { }
