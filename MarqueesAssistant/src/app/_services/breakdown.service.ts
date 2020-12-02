import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Breakdown } from '../_models/breakdown';
import { Equipment } from '../_models/equipment';

@Injectable({
  providedIn: 'root'
})
export class BreakdownService {
  
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { 
  

  

}

getEquipments(): Observable<Equipment[]>
{
  return this.http.get<Equipment[]>(this.baseUrl + 'equipments');
}

getEquimpment(id: number): Observable<Equipment>
{
  return this.http.get<Equipment>(this.baseUrl + 'equipments/' + id );
}

addEquipment(model: any)
{
  return this.http.post(this.baseUrl + 'equipments', model);
}

deleteEquipment(id: number)
{
  return this.http.delete(this.baseUrl + 'equipments/' + id);
}

editEquipment(model: any, id: number)
{
  return this.http.put(this.baseUrl + 'equipments/' + id, model);
}

getEquipmentBreakdowns(id: number)
{
  return this.http.get(this.baseUrl + 'equipments/getEquipmentBreakdowns/'+id);
}

getBreakdowns(): Observable<Breakdown[]>
{
  return this.http.get<Breakdown[]>(this.baseUrl + 'breakdowns');
}

getBreakdown(id:number): Observable<Breakdown>
{
  return this.http.get<Breakdown>(this.baseUrl + 'breakdowns/'+id);
}

deleteBreakdown(id: number)
{
  return this.http.delete(this.baseUrl + 'breakdowns/' + id);
}

addBreakdown(model: any, id: number)
{
  return this.http.post(this.baseUrl + 'breakdowns/' + id , model);
}

editBreakdown(model: any)
{
  return this.http.put(this.baseUrl + 'breakdowns', model);
}





}
