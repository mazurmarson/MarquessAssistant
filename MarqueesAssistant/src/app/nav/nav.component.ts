import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../_services/auth.service'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login()
  {
    this.authService.login(this.model).subscribe(next => {
      console.log('Zalogowales sie do aplikacji');
 
    }, error => {
      console.log('Błąd logowania');
    }
    );
  }

  loggedIn()
  {
    const token = localStorage.getItem('token');
    return !!token; //wykrzykniki to sprawdzeni czy w tokenie cos sie znajduje
  }

  logout()
  {
    localStorage.removeItem('token');
    console.log('Zostałeś wylogowany');
  }

}
