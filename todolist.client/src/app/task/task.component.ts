import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Task } from 'models/task';

@Component({
  selector: '[app-task]',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})

export class TaskComponent {
    public isLabelSelected: boolean = false;

    @Input() task!: Task;
    @Input() isNew: boolean = false;
    @Output() create = new EventEmitter<Task>();
    @Output() update = new EventEmitter<Task>();
    @Output() delete = new EventEmitter<Task>();

    ngOnInit() {
        this.isLabelSelected = this.isNew;
    }

    editLabel(label: string) {
        this.task.label = label;

        if (this.isNew) {
            this.create.emit();
        }
        else {
            this.isLabelSelected = false;
            this.update.emit();
        }
    }

    editIsCompleted(isCompleted: boolean) {
        this.task.isCompleted = isCompleted;

        if (this.isNew) {
            this.create.emit();
        }
        else {
            this.update.emit();
        }
    }

    selectLabel(event: Event) {
        if (this.isNew) {
            return;
        }

        this.isLabelSelected = true;

        // Set timeout to reflect correct data state and focus properly
        setTimeout(() => {
            (document.querySelector(`#input-${this.task.id}`) as HTMLElement).focus();
        },0); 
    }

    unselectLabel(event: Event) {
        if (this.isNew) {
            return;
        }

        // Exclude label to avoid unselecting immediately
        if ((event.target as HTMLElement).id != `label-${this.task.id}`)
        {
            this.isLabelSelected = false;
        }
    }
}
