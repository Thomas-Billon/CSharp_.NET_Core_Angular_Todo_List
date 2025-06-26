import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  template: ''
})

export abstract class BaseComponent implements OnDestroy {

    protected ngUnsubscribe: Subject<void> = new Subject();

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
