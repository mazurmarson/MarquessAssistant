import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
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

}
