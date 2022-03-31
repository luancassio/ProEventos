import { Component, OnInit } from '@angular/core';
import { User } from './core/models/identity/User';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(public accountService: AccountService){}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(): void{
    let user = {} as User;

    if (localStorage.getItem('user')) {
      user = JSON.parse(localStorage.getItem('user') ?? '{}');
    }else{
      user = null;
    }
    if (user) {
      this.accountService.setCurrentUser(user);
    
    }
  }
    

}
