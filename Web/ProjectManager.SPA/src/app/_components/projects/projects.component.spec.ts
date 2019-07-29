import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormsModule, FormGroup } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { HttpClientModule } from '@angular/common/http';
import { CONSTANTS } from 'src/app/_constants/constants';
import { DataTablesModule } from 'angular-datatables';
import { ToastrModule } from 'ng6-toastr-notifications';
import { ModalModule } from 'ngx-bootstrap';

import { ProjectsComponent } from './projects.component';
import { IProjectModel } from 'src/app/_models/project.model';

describe('ProjectsComponent', () => {
  let component: ProjectsComponent;
  let fixture: ComponentFixture<ProjectsComponent>;
  let mockAlertifyService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, ReactiveFormsModule, FormsModule, BsDatepickerModule.forRoot(),
        HttpClientModule, DataTablesModule, ToastrModule.forRoot(), ModalModule.forRoot()],
      declarations: [ProjectsComponent],
    });

    fixture = TestBed.createComponent(ProjectsComponent);

    mockAlertifyService = jasmine.createSpyObj(['confirm', 'success', 'error']);

    component = fixture.componentInstance;
    component.ngOnInit();

  });

  it('Project form invalid when empty', () => {
    expect(component.ProjectForm.valid).toBeFalsy();
  });

  it('should set range config value based on parameter passed', () => {
    const testValue = 3;
    component.setValueToPrioritySlider(testValue);
    expect(component.rangeSliderConfig.value).toBe(testValue);
  });

  it('validateProjectForm should return false when invalid data is submitted', () => {
    let form: FormGroup;
    form = component.projectForm;

    form['ProjectName'] = null;
    form['ProjectPriority'] = null;
    form['UserId'] = null;

    expect(component.validateProjectForm(form)).toBe(false);
  });

  // it('editProject function should bind model to projectform when a project is to be edited', () => {
  //   const project: IProjectModel = {
  //     ProjectId: 1, ProjectName: 'Task1', ProjectPriority: 3,
  //     ProjectStartDate: new Date(), ProjectEndDate: new Date(),
  //     UserId: 1, UserName: null, IsProjectSuspended: null
  //   };

  //   component.editProject(project);

  //   expect(component.projectModel).toBe(project);
  //   expect(component.projectSaveType).toBe(CONSTANTS.UPDATE);
  //   expect(component.projectForm.get('ProjectName').value).toEqual(project.ProjectName);
  //   expect(component.projectForm.get('ProjectPriority').value).toEqual(project.ProjectPriority);
  //   expect(component.projectForm.get('ProjectStartDate').value).toEqual(project.ProjectStartDate);
  //   expect(component.projectForm.get('ProjectEndDate').value).toEqual(project.ProjectEndDate);
  //   expect(component.projectForm.get('UserId').value).toEqual(project.UserId);
  // });


});
