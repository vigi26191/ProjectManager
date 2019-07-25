import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { ITaskModel } from 'src/app/_models/task.model';
import { TasksService } from 'src/app/_services/tasks.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { CONSTANTS } from 'src/app/_constants/constants';
import { MESSAGES } from 'src/app/_messages/messages';
import { Router, ActivatedRoute } from '@angular/router';
import { ROUTE_PATH } from 'src/app/_constants/route-names.constant';
import { ITaskLookup } from 'src/app/_models/task-lookup';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  taskForm: FormGroup;
  taskModel: ITaskModel;
  tasks: ITaskModel[];
  taskLookUp: ITaskLookup;

  isParentTask = false;

  showAddTaskSection = false;
  showViewTaskSection = false;

  sectionButtonText = CONSTANTS.ADD;

  taskManagerId: number;
  taskManagerParentId: number;

  datePickerConfig: Partial<BsDatepickerConfig>;

  rangeSliderConfig = { min: 0, max: 30, value: 0, step: 1 };
  rangeSliderTickMarks: number[] = [];

  dtOptions: DataTables.Settings = {
    pageLength: 5,
    lengthChange: false,
    pagingType: 'simple_numbers',
    ordering: true,
    searching: false
  };

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private tasksService: TasksService,
    private alertify: AlertifyService
  ) {
    activatedRoute.params.subscribe(taskSection => {
      if (taskSection.section == ROUTE_PATH.ADD_TASK) {
        this.activatedRoute.data.subscribe(resolved => {
          this.taskLookUp = resolved['lookUpData'];
        });

        this.activateTaskAddSection();
      }

      if (taskSection.section == ROUTE_PATH.VIEW_TASK) {
        this.getAllTasks();
        this.activateTaskViewSection();
      }
    });

    this.datePickerConfig = Object.assign({}, {
      showWeekNumbers: false,
      dateInputFormat: CONSTANTS.CONST_DATE_FORMAT
    });

    for (let index = this.rangeSliderConfig.min; index <= this.rangeSliderConfig.max; index++) {
      this.rangeSliderTickMarks.push(index);
    }
  }

  ngOnInit() {
    this.buildForm();
  }

  buildForm(): void {
    this.taskForm = this.fb.group({
      ProjectId: [null, Validators.required],
      TaskName: [null, Validators.required],
      IsParentTask: [null],
      TaskPriority: [null],
      ParentTaskId: [null],
      TaskStartDate: [null],
      TaskEndDate: [null],
      UserId: [null, Validators.required]
    });
  }

  get TaskForm() {
    return this.taskForm.controls;
  }

  get NotParentTaskFields() {
    return [
      this.TaskForm['TaskPriority'],
      this.TaskForm['ParentTaskId'],
      this.TaskForm['TaskStartDate'],
      this.TaskForm['TaskEndDate']
    ];
  }

  getAllTasks(): void {
    this.tasksService.getTasks()
      .subscribe(response => {
        this.tasks = response;
      },
        (error) => { this.alertify.error(error); },
        () => { });
  }

  submitTaskForm(taskForm: FormGroup): void {
    if (this.validateTaskForm(taskForm)) {
      const taskRequest = this.taskForm.getRawValue();
      taskRequest.IsParentTask = this.isParentTask;

      if (this.taskModel != null && this.taskModel.TaskId != null && this.taskModel.TaskId > 0) {
        taskRequest.TaskId = this.taskModel.TaskId;
      }

      this.tasksService.saveTask(taskRequest)
        .subscribe(response => {
          this.alertify.success(response);
          this.setDefaults();
          this.router.navigate([ROUTE_PATH.TASKS, ROUTE_PATH.VIEW_TASK]);
        },
          (error) => {
            this.alertify.error(error.error.Message);
          }
        );
    }
  }

  validateTaskForm(form: FormGroup): boolean {
    if (form.invalid) {
      Object.keys(this.TaskForm).forEach(key => {
        this.TaskForm[key].markAsTouched();
      });
      this.alertify.error(MESSAGES.MSG_INVALID_FORM);
      return false;
    }

    const taskStartDate = form.get('TaskStartDate').value;
    const taskEndDate = form.get('TaskEndDate').value;
    if (taskStartDate > taskEndDate) {
      this.alertify.error(MESSAGES.MSG_DATE_VALIDATION);
      return false;
    }

    return true;
  }

  editTask(task: ITaskModel): void {
    this.setDefaults();

    this.sectionButtonText = CONSTANTS.UPDATE;

    this.setValueToPrioritySlider(task.TaskPriority == null ? 0 : task.TaskPriority);

    this.isParentTask = task.IsParentTask;

    this.taskModel = task;
    this.taskForm.patchValue({
      ProjectId: task.ProjectId,
      TaskName: task.TaskName,
      IsParentTask: task.IsParentTask,
      TaskPriority: task.TaskPriority,
      ParentTaskId: task.ParentTaskId,
      TaskStartDate: task.TaskStartDate == null ? null : new Date(task.TaskStartDate),
      TaskEndDate: task.TaskEndDate == null ? null : new Date(task.TaskEndDate),
      UserId: task.UserId
    });
    debugger;
    if (task.IsParentTask) { this.toggleTaskFormFields(this.NotParentTaskFields, true); }

    this.router.navigate([ROUTE_PATH.TASKS, ROUTE_PATH.ADD_TASK]);
  }

  endTask(taskId: number): void {
    this.alertify.confirm(
      MESSAGES.MSG_END_TASK_CONFIRMATION, () => {
        this.tasksService.endTask(taskId)
          .subscribe(response => {
            this.alertify.success(response);
            this.getAllTasks();
          },
            error => {
              this.alertify.error(error);
            });
      }
    );
  }

  setDefaults(): void {
    this.taskForm.reset();

    this.isParentTask = false;

    this.setValueToPrioritySlider(0);

    this.sectionButtonText = CONSTANTS.ADD;

    this.taskModel = {
      TaskId: 0,
      TaskName: null,
      IsParentTask: null,
      TaskPriority: null,
      TaskStartDate: null,
      TaskEndDate: null,
      IsTaskComplete: null,
      ProjectId: null,
      UserId: null,
      ParentTaskId: null,
      ParentTaskName: null
    };

    this.toggleTaskFormFields(this.NotParentTaskFields, false);
  }

  manageTaskFormFields(): void {
    this.toggleTaskFormFields(this.NotParentTaskFields, this.isParentTask);
    this.setValueToPrioritySlider(0);
  }

  toggleTaskFormFields(controls: AbstractControl[], isParentTask: boolean): void {
    controls.forEach(control => {
      if (isParentTask) {
        control.disable();
        control.setValue(null);
      } else {
        control.enable();
      }

      control.updateValueAndValidity();
    });
  }

  activateTaskAddSection(): void {
    this.showAddTaskSection = true;
    this.showViewTaskSection = false;
  }

  activateTaskViewSection(): void {
    this.showAddTaskSection = false;
    this.showViewTaskSection = true;
  }

  setValueToPrioritySlider(val: number): void {
    this.rangeSliderConfig.value = val;
  }

}
