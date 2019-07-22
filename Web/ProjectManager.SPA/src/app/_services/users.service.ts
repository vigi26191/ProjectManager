import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { IUserModel } from 'src/app/_models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  controllerRoute: string = environment.baseApiUrl + 'user';

  constructor(private http: HttpClient) { }

  getUser(userId: number): Observable<IUserModel> {
    return this.http.get<IUserModel>(this.controllerRoute + '/getUser/' + userId);
  }

  getUsers(): Observable<IUserModel[]> {
    return this.http.get<IUserModel[]>(this.controllerRoute + '/getUsers');
  }

  saveUser(userData: IUserModel): Observable<string> {
    return this.http.post<string>(this.controllerRoute + '/saveUser', userData);
  }

  removeUser(userId: number): Observable<string> {
    return this.http.post<string>(this.controllerRoute + '/removeUser/' +  userId, userId);
  }
}
