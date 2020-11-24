import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { WorkerService } from '../_services/worker.service';
import { Worker } from '../_models/worker';
import { BrowserModule } from '@angular/platform-browser'
import { AuthService } from '../_services/auth.service';
import { Apiresponse } from '../_models/apiresponse';


@Component({
  selector: 'app-workers-list',
  templateUrl: './workers-list.component.html',
  styleUrls: ['./workers-list.component.css']
})
export class WorkersListComponent implements OnInit {

  apiResponse: Apiresponse;
  
  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  searchString = '';
  sortBy: number = 1;

  constructor(private workerService: WorkerService, private alertify: AlertifyService, private authService: AuthService) {
    
   }

  ngOnInit() {
    
    this.loadWorkers();
  }



  loadWorkers()
  {
    this.workerService.getWorkers(this.pageNumber, this.pageSize).subscribe( (apiReponse: Apiresponse ) => {
      this.apiResponse = apiReponse;
      this.totalItems = apiReponse.pageSize * apiReponse.totalPages;
      console.log(this.totalItems);
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
    }, error => {
      this.alertify.error('Wystąpił błąd');
    });
  }

}
