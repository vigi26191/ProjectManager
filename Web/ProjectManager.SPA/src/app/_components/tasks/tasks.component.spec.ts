import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormsModule, FormGroup } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { HttpClientModule } from '@angular/common/http';

import { TasksComponent } from './tasks.component';
import { ITaskModel } from 'src/app/_models/task.model';
import { CONSTANTS } from 'src/app/_constants/constants';
import { DataTablesModule } from 'angular-datatables';
import { ToastrModule } from 'ng6-toastr-notifications';

describe('TasksComponent', () => {
  let component: TasksComponent;
  let fixture: ComponentFixture<TasksComponent>;
  let mockAlertifyService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, ReactiveFormsModule, FormsModule, BsDatepickerModule,
        HttpClientModule, DataTablesModule, ToastrModule.forRoot()],
      declarations: [TasksComponent],
    });

    fixture = TestBed.createComponent(TasksComponent);

    mockAlertifyService = jasmine.createSpyObj(['confirm', 'success', 'error']);

    component = fixture.componentInstance;
    component.ngOnInit();

  });

  it('form invalid when empty', () => {
    expect(component.TaskForm.valid).toBeFalsy();
  });

  it('should set range config value based on parameter passed', () => {
    const testValue = 3;
    component.setValueToPrioritySlider(testValue);
    expect(component.rangeSliderConfig.value).toBe(testValue);
  });

  it('validateTaskForm should return false when invalid data is submitted', () => {
    let form: FormGroup;
    form = component.taskForm;

    form['ProjectId'] = null;
    form['TaskName'] = null;
    form['UserId'] = null;

    expect(component.validateTaskForm(form)).toBe(false);
  });

  it('should display update section and bind model when a task is to be edited', () => {
    const task: ITaskModel = {
      TaskId: 1, TaskName: 'Task1', TaskPriority: 1, IsParentTask: null,
      TaskStartDate: new Date(), TaskEndDate: new Date(), IsTaskComplete: true,
      ProjectId: 3, UserId: 1, ParentTaskId: null, ParentTaskName: null
    };

    component.editTask(task);

    expect(component.taskModel).toBe(task);
    expect(component.sectionButtonText).toBe(CONSTANTS.UPDATE);
    expect(component.taskForm.get('TaskName').value).toEqual(task.TaskName);
    expect(component.taskForm.get('TaskPriority').value).toEqual(task.TaskPriority);
    expect(component.taskForm.get('IsParentTask').value).toEqual(task.IsParentTask);
    expect(component.taskForm.get('TaskStartDate').value).toEqual(task.TaskStartDate);
    expect(component.taskForm.get('TaskEndDate').value).toEqual(task.TaskEndDate);
    expect(component.taskForm.get('ProjectId').value).toEqual(task.ProjectId);
    expect(component.taskForm.get('UserId').value).toEqual(task.UserId);
  });


});
