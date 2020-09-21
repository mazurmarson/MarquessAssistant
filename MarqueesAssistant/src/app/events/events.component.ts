import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  events: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEvents();
  }

  getEvents()
  {
    this.http.get('http://localhost:5000/api/events').subscribe(response => {
      this.events = response;
    }, error => {
      console.log(error);
    });
  }

}
