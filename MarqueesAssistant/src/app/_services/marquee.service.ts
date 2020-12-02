import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Apiresponseplace } from '../_models/apiResponsePlace';
import { Marquee } from '../_models/marquee';
import { Place } from '../_models/place';

@Injectable({
  providedIn: 'root'
})
export class MarqueeService {

  baseUrl = environment.apiUrl;


constructor(private http: HttpClient) { }

  getMarquees(): Observable<Marquee[]>{
    return this.http.get<Marquee[]>(this.baseUrl + 'marquees');
  }

  getPlaces(): Observable<Place[]>{
    return this.http.get<Place[]>(this.baseUrl + 'places');
  }

  getSearchedPlaces(pageNumber?: number, pageSize?: number, searchString?:string, sortBy?:number)
  {
    return this.http.get<Apiresponseplace>(this.baseUrl + 'places?pageNumber=' + pageNumber +
    '&pageSize=' + pageSize + '&searchString=' + searchString + '&sortBy=' + sortBy);
  }

  getEvents(): Observable<Event[]>{
    return this.http.get<Event[]>(this.baseUrl + 'events');
  }

  getPlace(id: number): Observable<Place>
  {
    return this.http.get<Place>(this.baseUrl + 'places/' + id);
  }

  getEvent(id: number): Observable<Event>
  {
    return this.http.get<Event>(this.baseUrl + 'events/' + id);
  }

  getEventStuff(id:number):Observable<Marquee[]>{
    return this.http.get<Marquee[]>(this.baseUrl + 'events/stuff/' + id);
  }

  getEventPlaceName(id: number): Observable<any>
  {
    return this.http.get<any>(this.baseUrl + 'events/getEventPlaceName/' + id);
  }

  getMarquee(id: number): Observable<Marquee>
  {
    return this.http.get<Marquee>(this.baseUrl + 'marquees/' + id );
  }

  

  addMarquee(model: any, id: number)
  {
    return this.http.post(this.baseUrl + 'marquees/' + id, model);
  }
  
  addPlace(model: any)
  {
    return this.http.post(this.baseUrl + 'places', model);
  }

  addEvent(model: any, id: number)
  {
    return this.http.post(this.baseUrl + 'events/' + id, model);
  }

  deletePlace(id: number)
  {
    return this.http.delete(this.baseUrl + 'places/' + id);
  }

  deleteEvent(id: number)
  {
    return this.http.delete(this.baseUrl + 'events/' + id);
  }

  deleteMarquue(id: number)
  {
    return this.http.delete(this.baseUrl + 'marquees/' + id);
  }

  editPlace(model: any)
  {
    return this.http.put(this.baseUrl + 'places', model);
  }

  editEvent(model: any)
  {
    return this.http.put(this.baseUrl + 'events', model);
  }

  editMarquee(model: any)
  {
    return this.http.put(this.baseUrl + 'marquees', model);
  }

}

// register(model: any)
// {
//   return this.http.post(this.baseUrl + 'register', model);
// }

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
