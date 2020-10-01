import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { WorkerService } from '../_services/worker.service';
import { Worker } from '../_models/worker';
import { BrowserModule } from '@angular/platform-browser'


@Component({
  selector: 'app-workers-list',
  templateUrl: './workers-list.component.html',
  styleUrls: ['./workers-list.component.css']
})
export class WorkersListComponent implements OnInit {

  workers: Worker[];

  constructor(private workerService: WorkerService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadWorkers();
  }

  loadWorkers()
  {
    this.workerService.getWorkers().subscribe( (workers: Worker[] ) => {
      this.workers = workers;
    }, error => {
      this.alertify.error(error);
    });
  }

}
