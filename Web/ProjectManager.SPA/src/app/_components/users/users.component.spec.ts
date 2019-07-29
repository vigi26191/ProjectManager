import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormsModule, FormGroup } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { HttpClientModule } from '@angular/common/http';
import { CONSTANTS } from 'src/app/_constants/constants';
import { DataTablesModule } from 'angular-datatables';
import { ToastrModule } from 'ng6-toastr-notifications';
import { ModalModule } from 'ngx-bootstrap';
import { IUserModel } from 'src/app/_models/user.model';
import { UsersComponent } from './users.component';

describe('UsersComponent', () => {
  let component: UsersComponent;
  let fixture: ComponentFixture<UsersComponent>;
  let mockAlertifyService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, ReactiveFormsModule, FormsModule,
        HttpClientModule, DataTablesModule, ToastrModule.forRoot(), ModalModule.forRoot()],
      declarations: [UsersComponent],
    });

    fixture = TestBed.createComponent(UsersComponent);

    mockAlertifyService = jasmine.createSpyObj(['confirm', 'success', 'error']);

    component = fixture.componentInstance;
    component.ngOnInit();

  });

  it('User form invalid when empty', () => {
    expect(component.UserForm.valid).toBeFalsy();
  });

  it('editUser function should bind model to userform when a user is to be edited', () => {
    const user: IUserModel = {
      UserId: 1, FirstName: 'Test', LastName: 'Name', EmployeeId: 123
    };

    component.editUser(user);

    expect(component.userModel).toBe(user);
    expect(component.userSaveType).toBe(CONSTANTS.UPDATE);
    expect(component.userForm.get('FirstName').value).toEqual(user.FirstName);
    expect(component.userForm.get('LastName').value).toEqual(user.LastName);
    expect(component.userForm.get('EmployeeId').value).toEqual(user.EmployeeId);
  });

});
