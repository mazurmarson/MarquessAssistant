import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { WorkerService } from '../_services/worker.service';
import * as signalR from '@aspnet/signalr';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  model: any = {};
  role: string;
  constructor(private router: Router,public authService: AuthService, private alertify: AlertifyService
    , private workerService: WorkerService, private http: HttpClient) { 

  }
  test: string;
  id: number;
  // ngOnInit() {
  //   if(this.loggedIn())
  //   {
  //     this.anyMessages();
  //     setInterval( () => {
  //       this.anyMessages();
        
  //     }, 10000);
  //     this.id = this.authService.decodedToken?.nameid;
  //     this.role = this.authService.decodedToken?.role;
  //   }

  // }

  ngOnInit() {
    if(this.loggedIn())
    {
             this.id = this.authService.decodedToken?.nameid;
       this.role = this.authService.decodedToken?.role;
      this.anyMessages();
      console.log(this.test);
    }
    
    const connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("http://localhost:5000/notify")
    .build();

  connection.start().then(function () {
    console.log('Połączenie nawiązane poprawnie');
  }).catch(function (err) {
    return console.error(err.toString());
  });

   connection.on("BroadcastMessage", (message: string) => {

       this.anyMessages();

    
   });
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

  isAdmin()
  {
    if(this.role === 'admin')
    {
      return true;
      
    }
    else
    {
      return false;
    }
    
  }

  logout()
  {
    localStorage.removeItem('token');
    this.alertify.message('Zostałeś wylogowany');
    this.router.navigate(['/home']);
  }

  anyMessages()
  {

    
    this.workerService.anyMessages(this.id).subscribe(results => this.test = results);
    


    
  }

}
