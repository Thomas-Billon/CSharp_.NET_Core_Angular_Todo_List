import { Component, EventEmitter, HostListener, Input, Output, ElementRef } from '@angular/core';
import { Task } from 'models/task';

@Component({
  selector: '[app-task]',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})

export class TaskComponent {
    public isLabelSelected: boolean = false;

    @Input() task!: Task;
    @Output() update = new EventEmitter<Task>();
    @Output() delete = new EventEmitter<Task>();

    editLabel(label: string) {
        this.task.label = label;
        this.isLabelSelected = false;

        this.update.emit();
    }

    editIsCompleted(isCompleted: boolean) {
        this.task.isCompleted = isCompleted;

        this.update.emit();
    }

    selectLabel(event: Event) {
        this.isLabelSelected = true;

        // Set timeout to reflect correct data state and focus properly
        setTimeout(() => {
            (document.querySelector(`#input-${this.task.id}`) as HTMLElement).focus();
        },0); 
    }

    unselectLabel(event: Event) {
        // Exclude label to avoid unselecting immediately
        if ((event.target as HTMLElement).id != `label-${this.task.id}`)
        {
            this.isLabelSelected = false;
        }
    }
}
