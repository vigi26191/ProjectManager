import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { ITaskModel } from '../_models/task.model';
import { TasksService } from './tasks.service';

describe('TasksService', () => {

  let service: TasksService;
  let httpMock: HttpTestingController;

  let DUMMY_TASKS: ITaskModel[] = [];

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TasksService],
      imports: [HttpClientTestingModule]
    });

    service = TestBed.get(TasksService);
    httpMock = TestBed.get(HttpTestingController);

    DUMMY_TASKS = [
      {
        TaskId: 1, TaskName: 'Task1', TaskPriority: 1, TaskStartDate: new Date(), TaskEndDate: new Date(),
        ParentTaskId: null, ParentTaskName: null, IsTaskComplete: false, IsParentTask: null,
        ProjectId: 123, UserId: 456
      },
      {
        TaskId: 2, TaskName: 'Task2', TaskPriority: 2, TaskStartDate: new Date(), TaskEndDate: new Date(),
        ParentTaskId: null, ParentTaskName: null, IsTaskComplete: false, IsParentTask: null,
        ProjectId: 123, UserId: 456
      }
    ];
  });

  afterEach(() => { httpMock.verify(); });

  it('should get tasks and should return an Observable<ITaskModel[]>', () => {
    service.getTasks().subscribe(tasks => {
      expect(tasks.length).toBe(2);
      expect(tasks).toEqual(DUMMY_TASKS);
    });

    const req = httpMock.expectOne(service.controllerRoute + '/getTasks');
    expect(req.request.method).toBe('GET');
    req.flush(DUMMY_TASKS);
  });

  it('should post correct data', () => {
    const newTask: ITaskModel = {
      TaskId: 3, TaskName: 'Task3', TaskPriority: 3, TaskStartDate: new Date(), TaskEndDate: new Date(),
      ParentTaskId: null, ParentTaskName: null, IsTaskComplete: false, IsParentTask: null,
      ProjectId: 123, UserId: 456
    };

    service.saveTask(newTask)
      .subscribe((task: any) => {
        expect(task.TaskName).toBe('Task3');
      });

    const req = httpMock.expectOne(service.controllerRoute + '/saveTask');
    expect(req.request.method).toBe('POST');
    req.flush(newTask);
  });

  it('should end task based on id parameter', () => {
    const task: ITaskModel = {
      TaskId: 4, TaskName: 'Task4', TaskPriority: 4, TaskStartDate: new Date(), TaskEndDate: new Date(),
      ParentTaskId: null, ParentTaskName: null, IsTaskComplete: true, IsParentTask: null,
      ProjectId: 123, UserId: 456
    };

    service.endTask(task.TaskId)
      .subscribe((response: any) => {
        expect(response.IsTaskComplete).toBe(true);
      });

    const req = httpMock.expectOne(service.controllerRoute + '/endTask/' + task.TaskId);
    expect(req.request.method).toBe('POST');
    req.flush(task);
  });

});
