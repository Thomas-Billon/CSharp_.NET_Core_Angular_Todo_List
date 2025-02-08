import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgClickOutsideDirective } from 'ng-click-outside2';

import { TaskComponent } from './task.component';

@NgModule({
    declarations: [
        TaskComponent
    ],
    exports: [
        TaskComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        NgClickOutsideDirective
    ],
    providers: []
})

export class TaskModule { }
