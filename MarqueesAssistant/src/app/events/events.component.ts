import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { MarqueeService } from '../_services/marquee.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  events: any;
  constructor(private http: HttpClient, private marqueeService: MarqueeService, private alertifyService: AlertifyService) { }

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

  deleteEvent(id: number)
  {
    this.marqueeService.deleteEvent(id).subscribe(responsne => {
      this.alertifyService.success('Usunięto wydarzenie');
    }, error => {
      this.alertifyService.error('Wystąpił błąd');
    });
  }



}
