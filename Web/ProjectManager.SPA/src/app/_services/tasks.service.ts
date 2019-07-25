import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { ITaskModel } from 'src/app/_models/task.model';
import { ITaskLookup } from '../_models/task-lookup';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  controllerRoute: string = environment.baseApiUrl + 'task';

  constructor(private http: HttpClient) { }

  getTask(taskId: number): Observable<ITaskModel> {
    return this.http.get<ITaskModel>(this.controllerRoute + '/getTask/' + taskId);
  }

  getTasks(): Observable<ITaskModel[]> {
    return this.http.get<ITaskModel[]>(this.controllerRoute + '/getTasks');
  }

  getTaskLookUpdata(): Observable<ITaskLookup> {
    return this.http.get<ITaskLookup>(this.controllerRoute + '/lookupTask');
  }

  saveTask(taskData: ITaskModel): Observable<string> {
    return this.http.post<string>(this.controllerRoute + '/saveTask', taskData);
  }

  endTask(taskId: number): Observable<string> {
    return this.http.post<string>(this.controllerRoute + '/endTask/' + taskId, taskId);
  }
}
