import { Task } from '@/models/task';
import { Injectable } from '@angular/core';
import { BaseService } from '@/services/base.service';
import { Subject } from 'rxjs';

@Injectable()
export class TaskService extends BaseService {

    private taskUrl: string = "task";

    public getAllTasks(callback: (result?: Task[]) => void, ngUnsubscribe: Subject<void>) {
        this.get({ url: this.taskUrl, callback, ngUnsubscribe });
    }
}
