import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgClickOutsideDirective } from 'ng-click-outside2';
import { TaskComponent } from './task.component';
import { TaskService } from '@/services/task.service';

@NgModule({
    declarations: [
        TaskComponent
    ],
    exports: [
        TaskComponent
    ],
    imports: [
        CommonModule,
        NgClickOutsideDirective
    ],
    providers: [TaskService]
})

export class TaskModule { }
