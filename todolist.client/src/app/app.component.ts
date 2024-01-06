import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Task } from 'models/task';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
    public tasks: Task[] = [];

    constructor(private http: HttpClient) {}

    ngOnInit() {
        this.getAllTasks();
    }

    getAllTasks() {
        this.http.get<Task[]>('/task').subscribe({
            next: (result) => {
                this.tasks = result;
            },
            error: (e) => {
                console.error(e);
            }
        });
    }

    createTask(task: Task) {
        this.http.post<number>('/task', task).subscribe({
            next: (result) => {
                task.id = result;
                this.tasks.push(task);
            },
            error: (e) => {
                console.error(e);
            }
        });
    }

    updateTask(task: Task) {
        this.http.put<boolean>('/task', task).subscribe({
            next: (result) => {
                if (!result)
                {
                    console.error(result);
                    return;
                }

                const index = this.tasks.findIndex(t => t.id == task.id);
                this.tasks[index] = task;
            },
            error: (e) => {
                console.error(e);
            }
        });
    }

    deleteTask(id: number) {
        this.http.delete<boolean>(`/task/${id}`).subscribe({
            next: (result) => {
                if (!result)
                {
                    console.error(result);
                    return;
                }
                
                const index = this.tasks.findIndex(t => t.id == id);
                this.tasks.splice(index, 1);
            },
            error: (e) => {
                console.error(e);
            }
        });
    }
}
