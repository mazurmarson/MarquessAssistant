import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-marquee',
  templateUrl: './marquee.component.html',
  styleUrls: ['./marquee.component.css']
})
export class MarqueeComponent implements OnInit {

  marquees: any;

  constructor(private http: HttpClient, private authSerivce: AuthService) { }

  ngOnInit() {
    this.getMarquees();
  }

  getMarquees()
  {
    if(this.authSerivce.loggedIn() === false )
    {
      
    }

    this.http.get('http://localhost:5000/api/marquees').subscribe(response => {
      this.marquees = response;
    }, error => {
      console.log(error);
    });
  }





}
