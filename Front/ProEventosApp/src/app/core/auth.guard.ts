import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import Swal from 'sweetalert2';


@Injectable({
  providedIn: 'root'
})
export class AuthdGuard implements CanActivate {

  constructor(private router: Router){}

  canActivate(): boolean {
    if (localStorage.getItem('user') !== null) {
      return true;
    }
    Swal.fire('Aviso','Usuário não auntenticado', 'warning').then(() =>{
      this.router.navigateByUrl('/user/login');
    });
    return false;
  }
  
}
