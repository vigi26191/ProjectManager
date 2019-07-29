import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { IUserModel } from '../_models/user.model';
import { UsersService } from './users.service';

describe('UsersService', () => {

  let service: UsersService;
  let httpMock: HttpTestingController;

  let DUMMY_USERS: IUserModel[] = [];

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UsersService],
      imports: [HttpClientTestingModule]
    });

    service = TestBed.get(UsersService);
    httpMock = TestBed.get(HttpTestingController);

    DUMMY_USERS = [
      {
        UserId: 1, FirstName: 'fName1', LastName: 'lName1', EmployeeId: 11
      },
      {
        UserId: 2, FirstName: 'fName2', LastName: 'lName2', EmployeeId: 22
      }
    ];
  });

  afterEach(() => { httpMock.verify(); });

  it('should get users and should return an Observable<IUserModel[]>', () => {
    service.getUsers().subscribe(response => {
      expect(response.length).toBe(2);
      expect(response).toEqual(DUMMY_USERS);
    });

    const req = httpMock.expectOne(service.controllerRoute + '/getUsers');
    expect(req.request.method).toBe('GET');
    req.flush(DUMMY_USERS);
  });

  it('should post correct data', () => {
    const user: IUserModel = {
      UserId: 3, FirstName: 'fName3', LastName: 'lName3', EmployeeId: 33
    };

    service.saveUser(user)
      .subscribe((response: any) => {
        expect(response.FirstName).toBe('fName3');
        expect(response.LastName).toBe('lName3');
        expect(response.EmployeeId).toBe(33);
      });

    const req = httpMock.expectOne(service.controllerRoute + '/saveUser');
    expect(req.request.method).toBe('POST');
    req.flush(user);
  });

  it('should delete user based on UserId parameter', () => {
    const users = [
      {
        UserId: 4, FirstName: 'fName4', LastName: 'lName4', EmployeeId: 44
      },
      {
        UserId: 5, FirstName: 'fName5', LastName: 'lName5', EmployeeId: 55
      }
    ];

    const userToBeRemoved = users[0];

    service.removeUser(userToBeRemoved.UserId)
      .subscribe((response: any) => {
        expect(this.users.length).toBe(1);
      });

    const req = httpMock.expectOne(service.controllerRoute + '/removeUser/' + userToBeRemoved.UserId);
    expect(req.request.method).toBe('POST');
    req.flush(users);
  });

});
