import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-marquee',
  templateUrl: './marquee.component.html',
  styleUrls: ['./marquee.component.css']
})
export class MarqueeComponent implements OnInit {

  marquees: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getMarquees();
  }

  getMarquees()
  {
    this.http.get('http://localhost:5000/api/marquees').subscribe(response => {
      this.marquees = response;
    }, error => {
      console.log(error);
    });
  }





}
