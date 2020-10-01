
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from 'src/app/_models/message';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { WorkerService } from 'src/app/_services/worker.service';

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


  constructor(private route: ActivatedRoute, private workerService: WorkerService, private authService: AuthService, private alertifyService: AlertifyService,) {
    this.route.params.subscribe(params => {
      this.id2 = Number(params['id']);
      this.model.recipientId = this.id2;
   
    });
    
   }

  ngOnInit() {
    this.getConversation();
    
  }

  
  getConversation()
  {
  
    this.workerService.getConversation(this.id, this.id2).subscribe( response => {
      this.messages = response;
      
    }, error => {
      this.alertifyService.error('Wystąpił błąd');
    });
  }

  sendMessage()
  {
    console.log(this.model);
    console.log(this.liczba);
    this.workerService.sendMessage(this.id, this.model).subscribe( () => {
      this.alertifyService.success('Wysłano wiadomość');
    }, error => {
      this.alertifyService.error('Wystąpił problem');
    }); 

  }


}
