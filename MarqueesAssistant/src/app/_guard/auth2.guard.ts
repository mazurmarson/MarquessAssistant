import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class Auth2Guard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService)
  {

  }


  canActivate(): boolean {
    if (this.authService.loggedIn())
    {
      if(this.authService.checkRole() != 'pracownik')
      {
        return true;
      }
      this.alertify.error('Nie jesteś upoważniony do dodawania, edycji i usuwania');
    }
    else
    this.alertify.error('Nie masz uprawnien');
  }


  
}
