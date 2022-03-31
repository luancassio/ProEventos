import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../core/models/identity/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSoucer = new ReplaySubject<User>(1);
  public currentUser$ = this.currentUserSoucer.asObservable();

  private baseUrl = environment.apiURL + 'api/account/'
  
  constructor(private http: HttpClient) { }

  public setCurrentUser(user: User): void{
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSoucer.next(user);
  }
  
  public login(model: any): Observable<void>{
    return this.http.post<User>(this.baseUrl + 'login', model)
    .pipe(take(1), map((response: User) =>{ 
      const user = response;
      if (user) {
        this.setCurrentUser(user);
      }
    }));
  }

  public logout(): void{
      localStorage.removeItem('user');
      this.currentUserSoucer.next(null);
      this.currentUserSoucer.complete();
  }

  public register(model: any): Observable<void>{
    return this.http.post<User>(this.baseUrl + 'RegisterUser', model)
    .pipe(take(1), map((response: User) =>{ 
      const user = response;
      if (user) {
        this.setCurrentUser(user);
      }
    }));
  }

}
