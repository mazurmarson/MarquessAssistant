import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Apiresponsemessage } from '../_models/apiresponsemessage';
import { AlertifyService } from './alertify.service';
import { WorkerService } from './worker.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection: signalR.HubConnection;
  constructor(private workerService: WorkerService, private alertifyService: AlertifyService) { }

  apiResponse: Apiresponsemessage;
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

  public newMessagesListenerForConversation = (id:number, id2:number, pageNumber:number, pageSize:number) => {
    this.hubConnection.on("MessageSended", () => {
      this.workerService.getPagedConversation(id, id2, pageNumber, pageSize).subscribe( (model: any) => {
        this.apiResponse = model;
        
       
      },error => {
        this.alertifyService.error(error);
        
      } )
    });
  }

 
    public newMessagesListenerForNav = (id:number) => {
      this.hubConnection.on("MessageSended", () => {
        this.workerService.anyMessages(id).subscribe(results => this.messageAmount = results);
     
    });
  }

  public getConnectionId = (login:string) => {
    this.hubConnection.invoke('getconnectionid',login).then(
      (data) => {
   
          this.myConnectionId = data;
        }
    ); 
  }

  public async GetUserIdConnection(id:string) {
    this.userConnectionId = await this.hubConnection.invoke('getuseridconnection',id);

  }

  // public DeleteUserIdConnection = (id:string) => {
  //   this.hubConnection.invoke('deleteuserfromlist',id);
  // }
}
