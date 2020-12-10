import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { AlertifyService } from './alertify.service';
import { WorkerService } from './worker.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection: signalR.HubConnection;
  constructor(private workerService: WorkerService, private alertifyService: AlertifyService) { }

  public messages: any = {};
  public messageAmount: string;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/notify")
    .build();


   this.hubConnection.start().then(function () {
      console.log('Połączenie nawiązane poprawnie przez serwis');
    }).catch(function (err) {
      return console.error(err.toString());
    });
  }

  public newMessagesListenerForConversation = (id:number, id2:number) => {
    this.hubConnection.on("MessageSended", () => {
      this.workerService.getConversation(id, id2).subscribe( (model: any) => {
        this.messages = model;

       
      },error => {
        this.alertifyService.error(error);
      } )
    });
  }
 
    public newMessagesListenerForNav = (id:number) => {
      this.hubConnection.on("MessageSended", () => {
        this.workerService.anyMessages(id).subscribe(results => this.messageAmount = results);
        console.log("przeslano wiadomosc");
    });
  }
}
