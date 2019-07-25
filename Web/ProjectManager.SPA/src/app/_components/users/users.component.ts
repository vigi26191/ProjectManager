import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { IUserModel } from 'src/app/_models/user.model';
import { UsersService } from 'src/app/_services/users.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { CONSTANTS } from 'src/app/_constants/constants';
import { MESSAGES } from 'src/app/_messages/messages';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  userForm: FormGroup;
  userModel: IUserModel = { UserId: 0, FirstName: null, LastName: null, EmployeeId: null };
  users: IUserModel[] = [];
  userSaveType = 'Add';

  dtOptions: DataTables.Settings = {
    pageLength: 3,
    lengthChange: false,
    pagingType: 'simple_numbers'
  };

  constructor(
    private fb: FormBuilder,
    private userService: UsersService,
    private alertify: AlertifyService
  ) {
  }

  ngOnInit() {
    this.buildForm();

    this.getAllUsers();
  }

  buildForm(): void {
    this.userForm = this.fb.group({
      FirstName: [null, Validators.required],
      LastName: [null, Validators.required],
      EmployeeId: [null, Validators.required]
    });
  }

  get UserForm() {
    return this.userForm.controls;
  }

  getAllUsers(): void {
    this.userService.getUsers()
      .subscribe(response => {
        this.users = response;
      },
        (error) => { this.alertify.error(error); },
        () => { });
  }

  submitUserForm(userForm: FormGroup): void {
    if (userForm.valid) {
      const userRequest = this.userForm.getRawValue();
      userRequest.UserId = this.userModel.UserId;

      this.userService.saveUser(userRequest)
        .subscribe(response => {
          this.alertify.success(response);
          this.resetUserForm();
        },
          (error) => {
            this.alertify.success(error.error.Message);
          },
          () => { this.getAllUsers(); });
    } else {
      Object.keys(this.UserForm).forEach(key => {
        this.UserForm[key].markAsTouched();
      });
    }
  }

  editUser(userId: number): void {
    const userToEdit = this.users.filter(f => f.UserId === userId)[0];

    this.UserForm['FirstName'].setValue(userToEdit.FirstName);
    this.UserForm['LastName'].setValue(userToEdit.LastName);
    this.UserForm['EmployeeId'].setValue(userToEdit.EmployeeId);
    this.userModel = userToEdit;

    this.userSaveType = CONSTANTS.UPDATE;
  }

  removeUser(userId: number): void {
    this.alertify.confirm(
      MESSAGES.MSG_REMOVE_USER_CONFIRMATION, () => {
        this.userService.removeUser(userId)
          .subscribe(response => {
            this.alertify.success(response);
          },
            (error) => {
              this.alertify.error(error.error.Message);
            },
            () => { this.getAllUsers(); });
      }
    );
  }

  resetUserForm(): void {
    this.userForm.reset();
    this.userModel = { UserId: 0, FirstName: null, LastName: null, EmployeeId: null };
    this.userSaveType = CONSTANTS.ADD;
  }

}
