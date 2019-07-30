import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { IProjectModel } from 'src/app/_models/project.model';
import { ProjectsService } from 'src/app/_services/projects.service';
import { CONSTANTS } from 'src/app/_constants/constants';
import { MESSAGES } from 'src/app/_messages/messages';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { IProjectLookup } from 'src/app/_models/project-lookup';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  projectForm: FormGroup;
  projects: IProjectModel[] = [];

  projectSaveType = 'Add';

  projectModel: IProjectModel = {
    ProjectId: 0,
    ProjectName: null,
    ProjectStartDate: null,
    ProjectEndDate: null,
    ProjectPriority: null,
    UserId: null,
    UserName: null,
    IsProjectSuspended: null
  };

  projectLookup: IProjectLookup;

  isSetDateChecked = false;

  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;

  dtOptions: DataTables.Settings = {
    pageLength: 5,
    lengthChange: false,
    pagingType: 'simple_numbers'
  };
  dtTrigger: Subject<any> = new Subject();

  datePickerConfig: Partial<BsDatepickerConfig>;

  rangeSliderConfig = { min: 0, max: 30, value: 0, step: 1 };
  rangeSliderTickMarks: number[] = [];

  bsModalRef: BsModalRef;
  @ViewChild('saveProjectTemplate', { static: false }) saveProjectTemplate: TemplateRef<any>;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private projectService: ProjectsService,
    private modalService: BsModalService,
    private alertify: AlertifyService
  ) {
    this.datePickerConfig = Object.assign({}, {
      showWeekNumbers: false,
      dateInputFormat: 'DD-MMM-YYYY'
    });

    for (let index = this.rangeSliderConfig.min; index <= this.rangeSliderConfig.max; index++) {
      this.rangeSliderTickMarks.push(index);
    }

  }

  ngOnInit() {
    this.buildForm();

    this.activatedRoute.data.subscribe(resolved => {
      this.projectLookup = resolved['lookUpData'];
      this.projects = this.projectLookup.Projects;
    });

    this.manageSetDateFields();
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next();
  }

  dataTableRender() {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();
      this.dtTrigger.next();
    });
  }

  buildForm(): void {
    this.projectForm = this.fb.group({
      ProjectName: [null, Validators.required],
      ProjectStartDate: [null],
      ProjectEndDate: [null],
      ProjectPriority: [null, Validators.required],
      UserId: [null, Validators.required]
    });
  }

  get ProjectForm() {
    return this.projectForm.controls;
  }

  get DateFields() {
    return [this.ProjectForm['ProjectStartDate'], this.ProjectForm['ProjectEndDate']]
  }

  getAllProjects(): void {
    this.projectService.getProjects()
      .subscribe(response => {
        this.projects = response;
      },
        (error) => { this.alertify.error(error); },
        () => { this.dataTableRender(); });
  }

  submitProjectForm(projectForm: FormGroup): void {
    if (this.validateProjectForm(projectForm)) {
      const projectRequest = this.projectForm.getRawValue();

      if (this.projectModel != null && this.projectModel.ProjectId != null && this.projectModel.ProjectId > 0) {
        projectRequest.ProjectId = this.projectModel.ProjectId;
      }

      this.projectService.saveProject(projectRequest)
        .subscribe(response => {
          this.resetProjectForm();
          this.alertify.success(response);
        },
          (error) => {
            this.alertify.error(error.error.Message);
          },
          () => {
            this.getAllProjects();
            this.closeProjectUserModal();
          });
    } else {
      Object.keys(this.ProjectForm).forEach(key => {
        this.ProjectForm[key].markAsTouched();
      });
    }
  }

  validateProjectForm(form: FormGroup): boolean {

    if (form.invalid) {
      Object.keys(this.ProjectForm).forEach(key => {
        this.ProjectForm[key].markAsTouched();
      });
      this.alertify.error(MESSAGES.MSG_INVALID_FORM);
      return false;
    }

    const projectStartDate = form.get('ProjectStartDate').value;
    const projectEndDate = form.get('ProjectEndDate').value;
    if (projectStartDate > projectEndDate) {
      this.alertify.error(MESSAGES.MSG_DATE_VALIDATION);
      return false;
    }

    return true;
  }

  editProject(project: IProjectModel): void {
    this.projectSaveType = CONSTANTS.UPDATE;

    this.projectModel = project;

    this.setValueToPrioritySlider(project.ProjectPriority == null ? 0 : project.ProjectPriority);

    this.projectForm.patchValue({
      ProjectName: project.ProjectName,
      ProjectPriority: project.ProjectPriority,
      ProjectStartDate: project.ProjectStartDate == null ? null : new Date(project.ProjectStartDate),
      ProjectEndDate: project.ProjectEndDate == null ? null : new Date(project.ProjectEndDate),
      UserId: project.UserId
    });

    if (this.projectModel.ProjectStartDate != null && this.projectModel.ProjectEndDate != null) {
      this.isSetDateChecked = true;
      this.manageSetDateFields();
    } else { this.isSetDateChecked = false; }

    this.openSaveProjectModal(this.saveProjectTemplate);
  }

  suspendProject(projectId: number): void {
    this.alertify.confirm(
      MESSAGES.MSG_SUSPEND_PROEJCT_CONFIRMATION, () => {
        this.resetProjectForm();
        this.projectService.suspendProject(projectId)
          .subscribe(response => {
            this.alertify.success(response);
          },
            (error) => {
              this.alertify.error(error.error.Message);
            },
            () => {
              this.getAllProjects();
            }
          );
      }
    );
  }

  manageSetDateFields(): void {
    this.setRequiredFields(this.DateFields, this.isSetDateChecked);
  }

  setValueToPrioritySlider(val: number): void {
    this.rangeSliderConfig.value = val;
  }

  setRequiredFields(controls: AbstractControl[], setAsRequiredFields: boolean): void {
    controls.forEach(control => {
      if (setAsRequiredFields) {
        control.enable();
        control.setValidators(Validators.required);
      } else {
        control.disable();
        control.setValue(null);
        control.setValidators(null);
      }

      control.updateValueAndValidity();
    });
  }

  resetProjectForm(): void {
    this.projectForm.reset();

    this.projectSaveType = CONSTANTS.ADD;

    this.projectModel = {
      ProjectId: 0,
      ProjectName: null,
      ProjectStartDate: null,
      ProjectEndDate: null,
      ProjectPriority: null,
      UserId: null,
      UserName: null,
      IsProjectSuspended: null
    };

    this.setValueToPrioritySlider(0);

    this.isSetDateChecked = false;
    this.setRequiredFields(this.DateFields, false);
  }

  addNewProject(saveProjectTemplate: TemplateRef<any>): void {
    this.resetProjectForm();
    this.openSaveProjectModal(saveProjectTemplate);
  }

  openSaveProjectModal(saveProjectTemplate: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(saveProjectTemplate, { class: 'modal-lg' });
  }

  closeProjectUserModal() {
    this.bsModalRef.hide();
  }

}
