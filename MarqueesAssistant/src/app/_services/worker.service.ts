import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { Worker } from '../_models/worker';




@Injectable({
  providedIn: 'root'
})
export class WorkerService {

  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


  getWorkers(): Observable<Worker[]>{
    return this.http.get<Worker[]>(this.baseUrl + 'workers');
  }

  getWorker(id: number): Observable<Worker>
  {
    return this.http.get<Worker>(this.baseUrl + 'workers/' + id);
  }

  getMessages(id: number): Observable<Message[]>
  {
    return this.http.get<Message[]>(this.baseUrl + 'workers/' + id + '/messages' );
  }

  getConversation(idWorker: number, idWorker2: number): Observable<Message[]>
  {
    return this.http.get<Message[]>(this.baseUrl + 'workers/' + idWorker + '/messages/conversation/' + idWorker2);
  }

  sendMessage(idSender: number, model: any)
  {
    return this.http.post(this.baseUrl + 'workers/' + idSender + '/messages',  model );
  }

}
