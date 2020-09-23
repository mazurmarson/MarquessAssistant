import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login()
  {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Zalogowales sie do aplikacji');
 
    }, error => {
      this.alertify.error('Błąd logowania');
    }
    );
  }

  loggedIn()
  {

    return this.authService.loggedIn();
  }

  logout()
  {
    localStorage.removeItem('token');
    this.alertify.message('Zostałeś wylogowany');
  }

}