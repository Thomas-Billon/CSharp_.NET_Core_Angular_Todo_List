import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Task } from '@/entities/task';

@Component({
  selector: '[app-task]',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})

export class TaskComponent {
    public isTitleSelected: boolean = false;

    @Input() task!: Task;
    @Input() isNew: boolean = false;

    @Output() create = new EventEmitter<Task>();
    @Output() update = new EventEmitter<Task>();
    @Output() delete = new EventEmitter<Task>();

    ngOnInit() {
        this.isTitleSelected = this.isNew;
    }

    editTitle(title: string) {
        this.task.title = title;

        if (this.isNew) {
            this.create.emit();
        }
        else {
            this.isTitleSelected = false;
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

    selectTitle(event: Event) {
        if (this.isNew) {
            return;
        }

        this.isTitleSelected = true;

        // Set timeout to reflect correct data state and focus properly
        setTimeout(() => {
            (document.querySelector(`#input-${this.task.id}`) as HTMLElement).focus();
        },0); 
    }

    unselectTitle(event: Event) {
        if (this.isNew) {
            return;
        }

        // Exclude title to avoid unselecting immediately
        if ((event.target as HTMLElement).id != `title-${this.task.id}`)
        {
            this.isTitleSelected = false;
        }
    }
}
