import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { IProjectModel } from 'src/app/_models/project.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  controllerRoute: string = environment.baseApiUrl + 'project';

  constructor(private http: HttpClient) { }

  getProject(projectId: number): Observable<IProjectModel> {
    return this.http.get<IProjectModel>(this.controllerRoute + '/getProject/' + projectId);
  }

  getProjects(): Observable<IProjectModel[]> {
    return this.http.get<IProjectModel[]>(this.controllerRoute + '/getProjects');
  }

  saveProject(projectData: IProjectModel): Observable<string> {
    return this.http.post<string>(this.controllerRoute + '/saveProject', projectData);
  }

  suspendProject(projectId: number): Observable<string> {
    return this.http.post<string>(this.controllerRoute + '/suspendProject/' +  projectId, projectId);
  }
}
