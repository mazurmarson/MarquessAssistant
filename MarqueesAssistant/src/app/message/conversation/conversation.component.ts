import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Message } from 'src/app/_models/message';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { TestService } from 'src/app/_services/test.service';
import { WorkerService } from 'src/app/_services/worker.service';
import * as signalR from '@aspnet/signalr';
import { SignalrService } from 'src/app/_services/signalr.service';

@Component({
  selector: 'app-conversation',
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

  messages: any;
  id: number = this.authService.decodedToken?.nameid;
  id2: number;
  model: any = {};
  liczba: number = 5;
  isLoaded: boolean = false;


  constructor(private route: ActivatedRoute, private workerService: WorkerService, private authService: AuthService, 
    private alertifyService: AlertifyService, public test: TestService, public signalRService: SignalrService) {
    this.route.params.subscribe(params => {
      this.id2 = Number(params['id']);
      this.model.recipientId = this.id2;
   
    });
    
   }

  ngOnInit() {
    this.getConversation();
  //  this.signalRService.startConnection();
   // this.test.startConnection();
   // this.test.newMessagesListener();
    this.signalRService.newMessagesListenerForConversation(this.id, this.id2);
   
   this.isLoaded = true;
  }

  ngOnDestroy()
{
  this.workerService.anyMessages(this.id).subscribe(results => this.signalRService.messageAmount = results);
}




  private startHttpRequest = () => {
    
  }

  
  // getConversation()
  // {
  
  //   this.workerService.getConversation(this.id, this.id2).subscribe( response => {
  //     this.messages = response;
      
  //   }, error => {
  //     this.alertifyService.error('Wystąpił błąd');
  //   });
  // }

  

  getConversation()
  {
    this.workerService.getConversation(this.id, this.id2)
    .pipe(
      tap(messages => {
        // tslint:disable-next-line: prefer-for-of
        for (let i = 0; i < messages.length; i++) {
          if (messages[i].isRead === false && messages[i].recipientId == this.id) {
            this.workerService.readMessage(this.id, messages[i].id);
            
          }
        }
      })
    )
    .subscribe(messages => {
      this.signalRService.messages = messages;
     
    }, error => {
      this.alertifyService.error(error);
    });
    this.workerService.anyMessages(this.id).subscribe(results => this.signalRService.messageAmount = results);
  }



  async sendMessage()
  {
    await this.signalRService.GetUserIdConnection(this.id2.toString());
    console.log(this.model);
    console.log(this.liczba);
    this.workerService.sendMessage(this.id,this.signalRService.userConnectionId ,this.model).subscribe( () => {
      this.alertifyService.success('Wysłano wiadomość');
      this.getConversation();
    }, error => {
      this.alertifyService.error('Wystąpił problem');
    }); 

  }

  readMessage(messageId: number)
  {
    this.workerService.readMessage(this.id, messageId);
  }

  getUserIdConnection()
  {
    this.signalRService.GetUserIdConnection(this.id2.toString());
  }


}
