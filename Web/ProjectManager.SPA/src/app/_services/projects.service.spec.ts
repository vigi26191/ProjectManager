import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { IProjectModel } from '../_models/project.model';
import { ProjectsService } from './projects.service';

describe('ProjectsService', () => {

  let service: ProjectsService;
  let httpMock: HttpTestingController;

  let DUMMY_PROJECTS: IProjectModel[] = [];

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProjectsService],
      imports: [HttpClientTestingModule]
    });

    service = TestBed.get(ProjectsService);
    httpMock = TestBed.get(HttpTestingController);

    DUMMY_PROJECTS = [
      {
        ProjectId: 1, ProjectName: 'Project1', ProjectStartDate: new Date(), ProjectEndDate: new Date(),
        ProjectPriority: 1, UserId: 1, UserName: 'User1', IsProjectSuspended: null
      },
      {
        ProjectId: 2, ProjectName: 'Project2', ProjectStartDate: new Date(), ProjectEndDate: new Date(),
        ProjectPriority: 2, UserId: 2, UserName: 'User2', IsProjectSuspended: null
      }
    ];
  });

  afterEach(() => { httpMock.verify(); });

  it('should get projects and should return an Observable<IProjectModel[]>', () => {
    service.getProjects().subscribe(response => {
      expect(response.length).toBe(2);
      expect(response).toEqual(DUMMY_PROJECTS);
    });

    const req = httpMock.expectOne(service.controllerRoute + '/getProjects');
    expect(req.request.method).toBe('GET');
    req.flush(DUMMY_PROJECTS);
  });

  it('should post correct data', () => {
    const user: IProjectModel = {
      ProjectId: 3, ProjectName: 'Project3', ProjectStartDate: new Date(), ProjectEndDate: new Date(),
      ProjectPriority: 3, UserId: 3, UserName: 'User3', IsProjectSuspended: false
    };

    service.saveProject(user)
      .subscribe((response: any) => {
        expect(response.ProjectName).toBe('Project3');
        expect(response.UserId).toBe(3);
        expect(response.IsProjectSuspended).toBe(false);
      });

    const req = httpMock.expectOne(service.controllerRoute + '/saveProject');
    expect(req.request.method).toBe('POST');
    req.flush(user);
  });

  it('should suspend project based on projectId parameter', () => {
    const project: IProjectModel = {
      ProjectId: 4, ProjectName: 'Project4', ProjectStartDate: new Date(), ProjectEndDate: new Date(),
      ProjectPriority: 4, UserId: 4, UserName: 'User4', IsProjectSuspended: true
    };

    service.suspendProject(project.ProjectId)
      .subscribe((response: any) => {
        expect(response.IsProjectSuspended).toBe(true);
      });

    const req = httpMock.expectOne(service.controllerRoute + '/suspendProject/' + project.ProjectId);
    expect(req.request.method).toBe('POST');
    req.flush(project);
  });

});
