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
import { Apiresponsemessage } from 'src/app/_models/apiresponsemessage';
import { PaginationWordsService } from 'src/app/_services/paginationWords.service';

@Component({
  selector: 'app-conversation',
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

  
  id: number = this.authService.decodedToken?.nameid;
  id2: number;
  model: any = {};
  liczba: number = 5;
  isLoaded: boolean = false;
  pageSize = 10;
  pageNumber = 1;
  totalItems: number;




  constructor(private route: ActivatedRoute, private workerService: WorkerService, private authService: AuthService, 
    private alertifyService: AlertifyService, public test: TestService, public signalRService: SignalrService, public paginationWords: PaginationWordsService) {
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
    this.signalRService.newMessagesListenerForConversation(this.id, this.id2, this.pageNumber, this.pageSize);
   
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
    this.workerService.getPagedConversation(this.id, this.id2, this.pageNumber, this.pageSize)
    .pipe(
      tap(messages => {
        
        for (let i = 0; i < messages.data.length; i++) {
          if (messages.data[i].isRead === false && messages.data[i].recipientId == this.id) {
            this.workerService.readMessage(this.id, messages.data[i].id);
            
          }
        }

      })
    )
    .subscribe(messages => {
      this.signalRService.apiResponse = messages;
      this.totalItems = this.signalRService.apiResponse.totalPages * this.signalRService.apiResponse.pageSize;

     
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
      this.model.content='';
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

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getConversation();
   
  }



}
