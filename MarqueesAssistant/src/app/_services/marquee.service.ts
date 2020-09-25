import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Marquee } from '../_models/marquee';

@Injectable({
  providedIn: 'root'
})
export class MarqueeService {

  baseUrl = environment.apiUrl;


constructor(private http: HttpClient) { }

  getMarquees(): Observable<Marquee[]>{
    return this.http.get<Marquee[]>(this.baseUrl + 'marquees');
  }

}

// @Injectable({
//   providedIn: 'root'
// })
// export class WorkerService {

//   baseUrl = environment.apiUrl;

// constructor(private http: HttpClient) { }


//   getWorkers(): Observable<Worker[]>{
//     return this.http.get<Worker[]>(this.baseUrl + 'workers');
//   }

//   getWorker(id: number): Observable<Worker>
//   {
//     return this.http.get<Worker>(this.baseUrl + 'workers/' + id);
//   }

// }
