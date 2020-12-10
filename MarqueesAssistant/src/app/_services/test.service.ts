import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { WorkerService } from './worker.service';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private hubConnection: signalR.HubConnection;
  constructor(private workerService: WorkerService) { }

    public model:any[];


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

  public newMessagesListener = () => {
    this.hubConnection.on("GetConversation", (model: any) => {
        this.model = model;
        console.log(model);
    });
  }
}
