import { Component, OnInit } from '@angular/core';

import { Message } from '../_models/message';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { WorkerService } from '../_services/worker.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  constructor(private workerService: WorkerService, private authService: AuthService, private alertifyService: AlertifyService) { }

  messages: any;
  id: number = this.authService.decodedToken?.nameid;
  idFriend: number;

  ngOnInit() {
    this.getMessages();
  }

  getMessages()
  {
    this.workerService.getMessages(this.id).subscribe( response => {
      this.messages = response;
      
    }, error => {
      this.alertifyService.error('Wystąpił błąd');
    });
  }

  getIdWorker(message:any)
  {
    if(message.recipientId == this.id)
    {
     // console.log(message.senderId);
      return message.senderId;
    // this.idFriend = message.senderId;
      
     
    }
    else
    {
      return message.recipientId;
    }
  }



}
