import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { IUserModel } from 'src/app/_models/user.model';
import { UsersService } from 'src/app/_services/users.service';
import { Subject } from 'rxjs';
import { CONSTANTS } from 'src/app/_constants/constants';
import { MESSAGES } from 'src/app/_messages/messages';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { DataTableDirective } from 'angular-datatables';

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

  @ViewChild(DataTableDirective, { static: false })
  dtElement: DataTableDirective;

  dtOptions: DataTables.Settings = {
    pageLength: 5,
    lengthChange: false,
    pagingType: 'simple_numbers'
  };
  dtTrigger: Subject<any> = new Subject();

  bsModalRef: BsModalRef;
  @ViewChild('saveUserTemplate', { static: false }) saveUserTemplate: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private userService: UsersService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  ) {
  }

  ngOnInit() {
    this.buildForm();

    this.getAllUsers();
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
        () => {
          this.dataTableRender();
        });
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
          () => {
            this.getAllUsers();
            this.closeSaveUserModal();
          });
    } else {
      Object.keys(this.UserForm).forEach(key => {
        this.UserForm[key].markAsTouched();
      });
    }
  }

  editUser(user: IUserModel): void {
    this.UserForm['FirstName'].setValue(user.FirstName);
    this.UserForm['LastName'].setValue(user.LastName);
    this.UserForm['EmployeeId'].setValue(user.EmployeeId);
    this.userModel = user;

    this.userSaveType = CONSTANTS.UPDATE;

    this.openSaveUserModal(this.saveUserTemplate);
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

  addNewUser(saveUserTemplate: TemplateRef<any>): void {
    this.resetUserForm();
    this.openSaveUserModal(saveUserTemplate);
  }

  openSaveUserModal(saveUserTemplate: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(saveUserTemplate, { class: 'modal-md' });
  }

  closeSaveUserModal() {
    this.bsModalRef.hide();
  }

}
