import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';
import { PaginationWordsService } from 'src/app/_services/paginationWords.service';

@Component({
  selector: 'app-equipment-breakdowns',
  templateUrl: './equipment-breakdowns.component.html',
  styleUrls: ['./equipment-breakdowns.component.css']
})
export class EquipmentBreakdownsComponent implements OnInit {


  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  breakdowns: any = {};
  id: number;
  isLoaded: boolean = false;
  idToBeDeleted = '';
  modalRef: BsModalRef;
  message: string;

  constructor(private alertify: AlertifyService, private breakdownService: BreakdownService
    , private route: ActivatedRoute, private authService: AuthService, private modalService: BsModalService, public paginationWords: PaginationWordsService) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
      
    });
  }

  ngOnInit() { 
    this.getBreakdowns(this.id);

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

    this.deleteBreakdown(Number(this.idToBeDeleted));
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();

  }

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getBreakdowns(1);
   
  }


  getBreakdowns(id: number)
  {
    this.breakdownService.getEquipmentBreakdownsPaged(id,this.pageNumber, this.pageSize).subscribe( response => {
      this.breakdowns = response;
     
    }, error => {
      this.alertify.error(error);
     
    });
  }

  deleteBreakdown(id: number)
  {
    this.breakdownService.deleteBreakdown(id).subscribe( response => {
      this.alertify.success('Usunięto awarie');
      this.getBreakdowns(this.id);
    }, error => {
      this.alertify.error(error);
    });
  }

  checkRole()
  {
    return this.authService.checkRole();
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
