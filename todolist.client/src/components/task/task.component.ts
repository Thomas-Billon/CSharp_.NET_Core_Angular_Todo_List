import { Component, Input, OnInit } from '@angular/core';
import { DefaultTask, Task } from '@/models/task';

@Component({
  selector: '[app-task]',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})

export class TaskComponent {
    @Input() taskData?: Task;
    @Input() isNew: boolean = false;

    public task: Task = this.taskData ?? DefaultTask;
    public isSelected: boolean = false;

    changeTitle(title: string): void {
        this.task.title = title;
    }

    changeIsCompleted(isCompleted: boolean): void {
        this.task.isCompleted = isCompleted;
    }

    create() {

    }

    update() {

    }

    delete() {
        
    }

    onSelect() {
        this.isSelected = true;
    }

    onUnselect() {
        this.isSelected = false;
    }
}
