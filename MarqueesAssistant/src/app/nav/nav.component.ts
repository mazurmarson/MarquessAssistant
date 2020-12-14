import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { WorkerService } from '../_services/worker.service';
import * as signalR from '@aspnet/signalr';
import { SignalrService } from '../_services/signalr.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  model: any = {};
  role: string;
  constructor(private router: Router,public authService: AuthService, private alertify: AlertifyService
    , private workerService: WorkerService, private http: HttpClient, public signalRService: SignalrService) { 

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
      this.signalRService.startConnection(this.authService.decodedToken?.nameid);
      this.id = this.authService.decodedToken?.nameid;
       this.role = this.authService.decodedToken?.role;
       this.anyMessages();
       this.signalRService.newMessagesListenerForNav(this.id);
       
   //  console.log(this.test);
    }
    window.onbeforeunload = () => this.ngOnDestroy();

  }

  ngOnDestroy()
  {
    this.signalRService.DeleteUserIdConnection(this.id.toString());
  }

  login()
  {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Zalogowales sie do aplikacji');
      this.anyMessages();
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

    
    this.workerService.anyMessages(this.id).subscribe(results => this.signalRService.messageAmount = results);
    


    
  }

}
