import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { TasksService } from '../_services/tasks.service';
import { ITaskLookup } from '../_models/task-lookup';

@Injectable({
    providedIn: 'root'
})
export class TaskResolver implements Resolve<ITaskLookup> {
    constructor(
        private tasksService: TasksService
    ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<ITaskLookup> {
        return this.tasksService.getTaskLookUpdata().pipe();
    }
}
