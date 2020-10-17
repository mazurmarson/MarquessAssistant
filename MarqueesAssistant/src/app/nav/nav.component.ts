import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { WorkerService } from '../_services/worker.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  model: any = {};
  constructor(private router: Router,public authService: AuthService, private alertify: AlertifyService, private workerService: WorkerService) { 

  }
  test: string;
  id: number;
  ngOnInit() {
    
    this.anyMessages();
    setInterval( () => {
      this.anyMessages();
    }, 10000);
  }

  login()
  {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Zalogowales sie do aplikacji');
      
    }, error => {
      this.alertify.error('Błąd logowania');
    }, () => {
      this.router.navigate(['/marquees']);
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
    this.router.navigate(['/home']);
  }

  anyMessages()
  {
   
    this.id = this.authService.decodedToken?.nameid;
    
    this.workerService.anyMessages(this.id).subscribe(results => this.test = results);



    
  }

}
