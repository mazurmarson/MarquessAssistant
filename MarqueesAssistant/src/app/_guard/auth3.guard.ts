import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class Auth3Guard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService)
  {

  }

  canActivate(): boolean {
    if (this.authService.loggedIn())
    {
      if(this.authService.checkRole() == 'admin')
      {
        return true;
      }
      this.alertify.error('Tę akcję może wykonać tylko admin');
    }
    else
    this.alertify.error('Nie masz uprawnien');
  }


  
}
