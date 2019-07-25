import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { UsersService } from './users.service';
import { IUserModel } from '../_models/user.model';

describe('TaskManagerService', () => {

  let service: UsersService;
  let httpMock: HttpTestingController;

  //let DUMMY_USERS: IUserModel[] = [];

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UsersService],
      imports: [HttpClientTestingModule]
    });

    service = TestBed.get(UsersService);
    httpMock = TestBed.get(HttpTestingController);

    // DUMMY_USERS = [
    //   {
    //     Id: 1, TaskName: 'Task1', Priority: 1, StartDate: new Date(), EndDate: new Date(),
    //     ParentTask: null, ParentTaskId: null, IsTaskComplete: false
    //   },
    //   {
    //     Id: 2, TaskName: 'Task2', Priority: 2, StartDate: new Date(), EndDate: new Date(),
    //     ParentTask: null, ParentTaskId: null, IsTaskComplete: false
    //   }
    // ];
  });

  afterEach(() => { httpMock.verify(); });

  // it('should get tasks and should return an Observable<ITaskManagerModel[]>', () => {
  //   service.getTasks().subscribe(tasks => {
  //     expect(tasks.length).toBe(2);
  //     expect(tasks).toEqual(DUMMY_TASKS);
  //   });

  //   const req = httpMock.expectOne(service.controllerRoute + '/lookupTaskManager');
  //   expect(req.request.method).toBe('GET');
  //   req.flush(DUMMY_TASKS);
  // });

  // it('should return filtered items', () => {
  //   const filterItems: ITaskManagerFilterCriteria = {
  //     ParentTaskId: null, TaskName: 'T', StartDate: null, EndDate: null,
  //     PriorityFrom: null, PriorityTo: null, IsTaskComplete: null
  //   };

  //   service.filterTaskManagerData(filterItems).subscribe(
  //     (task: any) => {
  //       expect(task[0].TaskName.indexOf(filterItems.TaskName) !== -1).toBe(true);
  //       expect(task[1].TaskName.indexOf(filterItems.TaskName) !== -1).toBe(true);
  //       expect(task.length).toBe(2);
  //     }
  //   );

  //   const req = httpMock.expectOne(service.controllerRoute + '/filterTasks');
  //   expect(req.request.method).toBe('POST');
  //   req.flush(DUMMY_TASKS);
  // });

  // it('should post correct data', () => {
  //   const newTask: ITaskManagerModel = {
  //     Id: 3, TaskName: 'Task3', Priority: 1, StartDate: new Date(), EndDate: new Date(),
  //     ParentTask: null, ParentTaskId: null, IsTaskComplete: false
  //   };

  //   service.saveTaskManager(newTask)
  //     .subscribe((task: any) => {
  //       expect(task.TaskName).toBe('Task3');
  //     });

  //   const req = httpMock.expectOne(service.controllerRoute + '/saveTask');
  //   expect(req.request.method).toBe('POST');
  //   req.flush(newTask);
  // });

  // it('should end task based on id parameter', () => {
  //   const newTask: ITaskManagerModel = {
  //     Id: 3, TaskName: 'Task3', Priority: 1, StartDate: new Date(), EndDate: new Date(),
  //     ParentTask: null, ParentTaskId: null, IsTaskComplete: true
  //   };

  //   service.endTask(newTask.Id)
  //     .subscribe((task: any) => {
  //       expect(task.IsTaskComplete).toBe(true);
  //     });

  //   const req = httpMock.expectOne(service.controllerRoute + '/endTask/' + newTask.Id);
  //   expect(req.request.method).toBe('POST');
  //   req.flush(newTask);
  // });

});
