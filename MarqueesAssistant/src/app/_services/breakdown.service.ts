import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Apiresponsebreakdown } from '../_models/apiresponsebreakdown';
import { Apiresponseequipment } from '../_models/apiresponseequipment';
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

getSearchedEquipment(pageNumber?: number, pageSize?: number, searchString?:string, sortBy?:number)
{
  return this.http.get<Apiresponseequipment>(this.baseUrl + 'equipments?pageNumber=' + pageNumber +
  '&pageSize=' + pageSize + '&searchString=' + searchString + '&sortBy=' + sortBy);

}

getSearchedBreakdowns(pageNumber?: number, pageSize?: number, searchString?:string, sortBy?:number, startRange?: string , endRange?: string)
{
  if(startRange === undefined && endRange === undefined)
  {
    return this.http.get<Apiresponsebreakdown>(this.baseUrl + 'breakdowns?pageNumber=' + pageNumber +
    '&pageSize=' + pageSize + '&searchString=' + searchString + '&sortBy=' + sortBy );
  }

  
  if(startRange === undefined || endRange === undefined)
  {
    if(startRange === undefined)
    {
      return this.http.get<Apiresponsebreakdown>(this.baseUrl + 'breakdowns?pageNumber=' + pageNumber +
      '&pageSize=' + pageSize + '&searchString=' + searchString + '&sortBy=' + sortBy +  '&endRange=' + endRange );
    }
    else
    {
      return this.http.get<Apiresponsebreakdown>(this.baseUrl + 'breakdowns?pageNumber=' + pageNumber +
      '&pageSize=' + pageSize + '&searchString=' + searchString + '&sortBy=' + sortBy + '&startRange=' + 
      startRange );
    }

  }
  return this.http.get<Apiresponsebreakdown>(this.baseUrl + 'breakdowns?pageNumber=' + pageNumber +
  '&pageSize=' + pageSize + '&searchString=' + searchString + '&sortBy=' + sortBy + '&startRange=' + 
  startRange + '&endRange=' + endRange );
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

getEquipmentBreakdownsPaged(id: number, pageNumber?: number, pageSize?: number,)
{
  return this.http.get(this.baseUrl + 'equipments/getEquipmentBreakdowns/'+id+ '?pageNumber=' + pageNumber +
  '&pageSize=' + pageSize);
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
