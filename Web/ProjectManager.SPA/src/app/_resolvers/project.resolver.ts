import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ProjectsService } from '../_services/projects.service';
import { IProjectLookup } from '../_models/project-lookup';

@Injectable({
    providedIn: 'root'
})
export class ProjectResolver implements Resolve<IProjectLookup> {
    constructor(
        private projectsService: ProjectsService
    ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<IProjectLookup> {
        return this.projectsService.getProjectLookUpData().pipe();
    }
}
