import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserLogin } from 'src/app/core/models/identity/UserLogin';
import { AccountService } from 'src/app/services/account.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public model = {} as UserLogin;
  public loading = true;

  constructor(
    private accountService: AccountService, 
    private router: Router
    ) { }

  ngOnInit(): void {}

  login():void{
    this.loading = false;
    this.accountService.login(this.model).subscribe(() => {
      this.router.navigateByUrl('/dashboard');
      const user = JSON.parse(localStorage.getItem('user'));
      Swal.fire('Logado com Sucesso', `Bem vindo ${user.primeiroNome}`, 'success');
    }, (err) => {
      if (err.status == 401) {
        Swal.fire('Erro', 'Usuário ou senha inválidos', 'error');
        this.loading = true;
      }else{
        console.error(err);
      }
    }).add(() => {

      this.loading = true;
    });
  }

}
