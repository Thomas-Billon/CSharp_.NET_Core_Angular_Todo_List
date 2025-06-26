import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, takeUntil } from "rxjs";

@Injectable()
export abstract class BaseService {

    private apiHost: string = "https://localhost:7029";

    constructor(
        private http: HttpClient,
    ) {}

    protected get<T>({ url, callback, ngUnsubscribe }: {
        url: string,
        callback?: (result?: T) => void,
        ngUnsubscribe?: Subject<void>
    }): void {

        if (!ngUnsubscribe) {
            ngUnsubscribe = new Subject();
        }

        this.http.get<T>(`${this.apiHost}/${url}`)
            .pipe(takeUntil(ngUnsubscribe))
            .subscribe({
                next: callback,
                error: (e) => {
                    console.error(e);
                }
            });
    }
}
