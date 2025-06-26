import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { TaskModule } from '@/components/task/task.module';
import { TaskService } from '@/services/task.service';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        TaskModule
    ],
    providers: [TaskService],
    bootstrap: [AppComponent]
})

export class AppModule { }
