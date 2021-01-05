import { Component, OnInit, TemplateRef } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { WorkerService } from '../_services/worker.service';
import { Worker } from '../_models/worker';
import { BrowserModule } from '@angular/platform-browser'
import { AuthService } from '../_services/auth.service';
import { Apiresponse } from '../_models/apiresponse';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-workers-list',
  templateUrl: './workers-list.component.html',
  styleUrls: ['./workers-list.component.css']
})
export class WorkersListComponent implements OnInit {

  apiResponse: Apiresponse;
  userId:number;
  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  searchString = '';
  sortBy: number = 1;
  idToBeDeleted = '';
  modalRef: BsModalRef;
  message: string;
  sizesOfPage: number[] = [5,10,20,30,40,50];


  constructor(private workerService: WorkerService, private alertify: AlertifyService, private authService: AuthService, private modalService: BsModalService) {
    
   }

  ngOnInit() {
    
    this.loadWorkers();
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

    this.deleteWorker(Number(this.idToBeDeleted));
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();

  }




  loadWorkers()
  {
    this.workerService.getWorkers(this.pageNumber, this.pageSize).subscribe( (apiReponse: Apiresponse ) => {
      this.apiResponse = apiReponse;
      this.totalItems = apiReponse.pageSize * apiReponse.totalPages;
      
    }, error => {
      this.alertify.error(error);
    });
  }

  loadWorkersWithSearch()
  {
    
    this.workerService.getSearchedWorkers(this.pageNumber, this.pageSize, this.searchString, this.sortBy).subscribe( (apiResponse: Apiresponse) => {
      this.apiResponse = apiResponse;
      this.totalItems = apiResponse.pageSize * apiResponse.totalPages;
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
    this.loadWorkers();
   
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

  deleteWorker(id: number)
  {
    this.workerService.deleteWorker(id).subscribe(response => {
      this.alertify.success('Usunięto użytkownika');
      this.loadWorkers();
    }, error => {
      this.alertify.error(error);
    });
  }

  isUser(id:number)
  {
    if(id == this.authService.getWorkerId())
    {
   
      return true;
    }
    else
    {
      return false;
    }
  }

}
