import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Apiresponsebreakdown } from 'src/app/_models/apiresponsebreakdown';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';

@Component({
  selector: 'app-breakdown-list',
  templateUrl: './breakdown-list.component.html',
  styleUrls: ['./breakdown-list.component.css']
})
export class BreakdownListComponent implements OnInit {

  apiResponse: Apiresponsebreakdown;
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



  constructor(private http: HttpClient, private alertify: AlertifyService,
    private router: Router, private breakdownService: BreakdownService, private authService: AuthService, private modalService: BsModalService) { }

  ngOnInit() {
    this.getBreakdowns();
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
    this.deleteBreakdown(Number(this.idToBeDeleted));
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();

  }


  getBreakdowns()
  {
    this.breakdownService.getSearchedBreakdowns(this.pageNumber, this.pageSize, this.searchString, this.sortBy, this.startDate, this.endDate)
    .subscribe( (apiResponseBreakdown: Apiresponsebreakdown) => {
      this.apiResponse = apiResponseBreakdown;
      this.totalItems = apiResponseBreakdown.totalPages * apiResponseBreakdown.pageSize;
      console.log(apiResponseBreakdown);
    }, error => {
      this.alertify.error(error);
    } );
  }

  deleteBreakdown(id: number)
  {
    this.breakdownService.deleteBreakdown(id).subscribe( response => {
      this.alertify.success('Usunięto awarie');
      this.getBreakdowns();
    }, error => {
      this.alertify.error(error);
    });
  }

  checkRole()
  {
    return this.authService.checkRole();
  }

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getBreakdowns();
   
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

  compareDates(data: Date)
  {
    
    
    if(data.toString() === '0001-01-01T00:00:00')
    {
     
      return true;
    }
    else 
    {
      
      return false;
    }
  }



}
