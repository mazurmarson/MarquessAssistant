import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-event-select',
  templateUrl: './event-select.component.html',
  styleUrls: ['./event-select.component.css']
})
export class EventSelectComponent implements OnInit {

  events: any;
  constructor(private http: HttpClient, private marqueeService: MarqueeService, private router: Router) { }

  ngOnInit() {
    this.getEvents();
  }

  getEvents()
  {
    this.http.get('http://localhost:5000/api/events').subscribe(response => {
      this.events = response;
    }, error => {
     
    });
  }
  
  selectEvent(id: number)
  {
    this.router.navigate(['marqueesAdd/',id]);
    
  }


}
