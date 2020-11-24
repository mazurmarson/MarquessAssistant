import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService)
  {

  }

  canActivate(): boolean {
    if (this.authService.loggedIn())
    {
      return true;
    }
    this.alertify.error('Nie masz uprawnien');
    this.router.navigate(['/home']);
    return false;
  }

  canCUD(): boolean{
    if(this.authService.checkRole() != 'pracownik')
    {
      return true;
    }
    this.alertify.error('Nie jesteś upoważniony do dodawania, edycji i usuwania');
  }
  
}
