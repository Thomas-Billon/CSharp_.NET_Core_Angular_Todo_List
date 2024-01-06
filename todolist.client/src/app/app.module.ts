import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgClickOutsideDirective } from 'ng-click-outside2';

import { AppComponent } from './app.component';
import { TaskComponent } from './task/task.component';

@NgModule({
    declarations: [
        AppComponent,
        TaskComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        NgClickOutsideDirective
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
