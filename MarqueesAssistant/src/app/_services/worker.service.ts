import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Apiresponse } from '../_models/apiresponse';
import { Apiresponsemessage } from '../_models/apiresponsemessage';
import { Message } from '../_models/message';
import { Worker } from '../_models/worker';




@Injectable({
  providedIn: 'root'
})
export class WorkerService {

  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


  getWorkers(pageNumber?: number, pageSize?: number): Observable<Apiresponse>{
    return this.http.get<Apiresponse>(this.baseUrl + 'workers?' + 'pageNumber='
     + pageNumber + '&pageSize=' + pageSize);
  }

  getSearchedWorkers(pageNumber?: number, pageSize?: number, searchString?: string, sortBy?: number)
  {
    return this.http.get<Apiresponse>(this.baseUrl + 'workers/searchWorker?' + 'pageNumber=' + pageNumber + 
    '&pageSize=' + pageSize + '&searchString=' + searchString + '&sortBy=' + sortBy);
  }

  getWorker(id: number): Observable<Worker>
  {
    return this.http.get<Worker>(this.baseUrl + 'workers/' + id);
  }

  getMessages(id: number): Observable<Message[]>
  {
    return this.http.get<Message[]>(this.baseUrl + 'workers/' + id + '/messages' );
  }

  getPagedMessages(id: number,pageNumber?: number, pageSize?: number): Observable<Message[]>
  {
    return this.http.get<Message[]>(this.baseUrl + 'workers/' + id + '/messages' + '?pageNumber=' + pageNumber +
    '&pageSize=' + pageSize);
  }

  getConversation(idWorker: number, idWorker2: number): Observable<Message[]>
  {
    return this.http.get<Message[]>(this.baseUrl + 'workers/' + idWorker + '/messages/conversation/' + idWorker2);
  }

  getPagedConversation(idWorker: number, idWorker2: number ,pageNumber?: number, pageSize?: number )
  {
    return this.http.get<Apiresponsemessage>(this.baseUrl + 'workers/' + idWorker + '/messages/conversation/' + idWorker2 + '?pageNumber=' + pageNumber +
    '&pageSize=' + pageSize );

  }

  // sendMessage(idSender: number, model: any)
  // {
  //   return this.http.post(this.baseUrl + 'workers/' + idSender + '/messages',  model );
  // }

    sendMessage(idSender: number, connectionId:string ,model: any)
  {
    
    return this.http.post(this.baseUrl + 'workers/' + idSender + '/messages/' + connectionId,  model );
  }

  readMessage(idRecipient: number, idMessage: number)
  {
    return this.http.post(this.baseUrl + 'workers/' + idRecipient + '/messages/read/' + idMessage, {} ).subscribe();
  }

  anyMessages(idWorker: number)
  {
    return this.http.get(this.baseUrl + 'workers/' + idWorker + '/messages/anyMessages', {responseType: 'text'} );
  }

  deleteWorker(id:number)
{
  console.log(this.baseUrl +'workers/'  + id);
  return this.http.delete(this.baseUrl +'workers/' +  id);
}

}
