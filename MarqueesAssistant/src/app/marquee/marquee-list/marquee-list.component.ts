import { Component, OnInit, TemplateRef } from '@angular/core';
import { Marquee } from 'src/app/_models/marquee';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/_services/auth.service';
import { Apiresponsemarquee } from 'src/app/_models/apiresponsemarquee';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PaginationWordsService } from 'src/app/_services/paginationWords.service';



@Component({
  selector: 'app-marquee-list',
  templateUrl: './marquee-list.component.html',
  styleUrls: ['./marquee-list.component.css']
})
export class MarqueeListComponent implements OnInit {

  apiResponse: Apiresponsemarquee;
  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  searchString = '';
  sortBy: number = 1;
  idToBeDeleted = '';
  modalRef: BsModalRef;
  message: string;
  sizesOfPage: number[] = [5,10,20,30,40,50];
  

  constructor(private marqueeService: MarqueeService, private alertify: AlertifyService, private authService: AuthService, private modalService: BsModalService, public paginationWords: PaginationWordsService) { }

  ngOnInit() {
    this.getMarquees();
    
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
    this.deleteMarquee(Number(this.idToBeDeleted));
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();

  }

  checkRole()
  {
    return this.authService.checkRole();
  }

  getMarquees()
  {
    this.marqueeService.getSearchedMarquees(this.pageNumber, this.pageSize, this.searchString, this.sortBy)
    .subscribe(( apiResponseMarquee: Apiresponsemarquee) => {
      this.apiResponse = apiResponseMarquee;
      this.totalItems = apiResponseMarquee.totalPages * apiResponseMarquee.pageSize;
    }, error => {
      this.alertify.error(error);
    });
  }

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getMarquees();
   
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


  deleteMarquee(id: number)
  {
    this.marqueeService.deleteMarquue(id).subscribe(response => {
      this.alertify.success('Usunięto namiot');
      this.getMarquees();
    }, error => {
      this.alertify.error('Wystąpił błąd');
    });
  }

}


// workers: Worker[];

// constructor(private workerService: WorkerService, private alertify: AlertifyService) { }

// ngOnInit() {
//   this.loadWorkers();
// }

// loadWorkers()
// {
//   this.workerService.getWorkers().subscribe( (workers: Worker[] ) => {
//     this.workers = workers;
//   }, error => {
//     this.alertify.error(error);
//   });
// }