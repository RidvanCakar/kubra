import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../Model';



@Injectable({
  providedIn: "root"
})
export class UserService {

  baseUrl: string = "http://localhost:5248/";

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + "api/User/GetUsers");
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + `api/User/GetUser/${id}`);
  }

  saveUser(user: User, roleID: number): Observable<User> {
    return this.http.post<User>(`${this.baseUrl}api/User/RegisterUser?roleID=${roleID}`, user);
  }

  loginUser(email: string, password: string): Observable<any> {
    const user = { email, password }
    return this.http.post<any>(this.baseUrl + "api/User/Login", user);
  }

  getUserRoles(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + "api/UserRoles/GetUserRoles");
  }

  getUserRole(id: number): Observable<any> {
    return this.http.get<any>(this.baseUrl + `api/UserRoles/GetUserRole/${id}`);
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(this.baseUrl +`api/User/DeleteUser/${id}`);
  }

  updateUser(user:User):Observable<User>{
    return this.http.put<User>(this.baseUrl+`api/User/PutUser/${user.id}`, user);
  }



}
