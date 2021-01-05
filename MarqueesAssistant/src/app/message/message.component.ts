import { Component, OnInit } from '@angular/core';

import { Message } from '../_models/message';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { PaginationWordsService } from '../_services/paginationWords.service';
import { WorkerService } from '../_services/worker.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  constructor(private workerService: WorkerService, private authService: AuthService, private alertifyService: AlertifyService, public paginationWords: PaginationWordsService) { }

  messages: any = {};
  id: number = this.authService.decodedToken?.nameid;
  idFriend: number;
  pageSize = 10;
  pageNumber = 1;
  totalItems: number;


  ngOnInit() {
    this.getMessages();
  }

  getMessages()
  {
    this.workerService.getPagedMessages(this.id, this.pageNumber, this.pageSize).subscribe( response => {
      this.messages = response;
     
    }, error => {
      this.alertifyService.error('Wystąpił błąd');
    });
  }

  getIdWorker(message:any)
  {
    if(message.message.recipientId == this.id)
    {
 
      return message.message.senderId;
    // this.idFriend = message.senderId;
      
     
    }
    else
    {
      return message.message.recipientId;
    }
  }

  
  setPage(page: number)
  {
    this.pageNumber = page;
    this.getMessages();
   
  }



}
