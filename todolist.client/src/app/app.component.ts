import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Task } from '@/entities/task';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
    public tasks: Task[] = [];
    public newTask!: Task;

    constructor(private http: HttpClient) {}

    ngOnInit() {
        this.getAllTasks();
        this.resetNewTask();
    }

    resetNewTask() {
        this.newTask = {id: 0, title: '', isCompleted: false};
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
        this.http.post<Task>('/task', task).subscribe({
            next: (result) => {
                if (!result) {
                    console.error(result);
                    return;
                }

                task.id = result.id;
                this.tasks.push(task);
                this.resetNewTask();
            },
            error: (e) => {
                console.error(e);
            }
        });
    }

    updateTask(task: Task) {
        this.http.put<boolean>('/task', task).subscribe({
            next: (result) => {
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
                const index = this.tasks.findIndex(t => t.id == id);
                this.tasks.splice(index, 1);
            },
            error: (e) => {
                console.error(e);
            }
        });
    }
}
