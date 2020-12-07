import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Apiresponseevent } from '../_models/apiresponseevent';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { MarqueeService } from '../_services/marquee.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  apiResponse: Apiresponseevent;
  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  searchString = '';
  sortBy: number = 1;
  startDate: string;
  endDate: string;

  constructor(private http: HttpClient, private marqueeService: MarqueeService, private alertifyService: AlertifyService, private authService: AuthService) { }

  ngOnInit() {
    this.getEvents();
  }

  checkRole()
  {
    return this.authService.checkRole();
  }


  getEvents()
  {
    this.marqueeService.getSearchedEvents(this.pageNumber, this.pageSize, this.searchString, this.sortBy, this.startDate, this.endDate)
    .subscribe(( apiResponseEvent: Apiresponseevent) => {
      this.apiResponse = apiResponseEvent;
      this.totalItems = apiResponseEvent.totalPages * apiResponseEvent.pageSize;
      console.log(this.startDate);
    }, error => {
      this.alertifyService.error(error);
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

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getEvents();
   
  }

  setColor(id: number)
  {
    if(this.sortBy === id)
    {

      return 'arrow';
    }
    else
    {
      return 'arrowCurrent';
    }
  }





}
