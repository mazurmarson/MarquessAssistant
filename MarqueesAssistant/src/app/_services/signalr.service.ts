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

  public messages: any = [];
  public messageAmount: string;
  public myConnectionId : string;
  public userConnectionId: string;

  public startConnection = (login:string) => {
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/notify")
    .build();


   this.hubConnection.start().then(function () {
      console.log('Połączenie nawiązane poprawnie przez serwis');
    }).then( () => this.getConnectionId(login)).catch(function (err) {
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

  public getConnectionId = (login:string) => {
    this.hubConnection.invoke('getconnectionid',login).then(
      (data) => {
        console.log(data);
          this.myConnectionId = data;
        }
    ); 
  }

  public async GetUserIdConnection(id:string) {
    this.userConnectionId = await this.hubConnection.invoke('getuseridconnection',id);

  }

  public DeleteUserIdConnection = (id:string) => {
    this.hubConnection.invoke('deleteuserfromlist',id);
  }
}
