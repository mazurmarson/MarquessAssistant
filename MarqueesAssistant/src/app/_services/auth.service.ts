import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }


login(model: any)
{
return this.http.post(this.baseUrl + 'login', model)
.pipe(map( (response: any) => {
  const worker = response;
  if(worker)
    {
      localStorage.setItem('token', worker.token);
    }
  } ));
}

}
