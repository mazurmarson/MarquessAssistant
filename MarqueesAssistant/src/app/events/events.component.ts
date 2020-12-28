import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
  idToBeDeleted = '';
  modalRef: BsModalRef;
  message: string;


  constructor(private http: HttpClient, private marqueeService: MarqueeService, private alertifyService: AlertifyService, private authService: AuthService, private modalService: BsModalService) { }

  ngOnInit() {
    this.getEvents();
  }

  openModal(template: TemplateRef<any>, id: any) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
    this.idToBeDeleted = id;
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.modalRef.hide();
    this.delete();
  }

  delete():void{
    console.log('deleted',this.idToBeDeleted,' record');
    this.deleteEvent(Number(this.idToBeDeleted));
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();

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
      this.getEvents();
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
