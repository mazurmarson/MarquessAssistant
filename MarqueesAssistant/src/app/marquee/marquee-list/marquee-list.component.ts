import { Component, OnInit } from '@angular/core';
import { Marquee } from 'src/app/_models/marquee';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AuthService } from 'src/app/_services/auth.service';



@Component({
  selector: 'app-marquee-list',
  templateUrl: './marquee-list.component.html',
  styleUrls: ['./marquee-list.component.css']
})
export class MarqueeListComponent implements OnInit {

  marquees: Marquee[];
  

  constructor(private marqueeService: MarqueeService, private alertify: AlertifyService, private authService: AuthService) { }

  ngOnInit() {
    this.loadMarquees();
    
  }

  checkRole()
  {
    return this.authService.checkRole();
  }

  loadMarquees()
  {
    this.marqueeService.getMarquees().subscribe( (marquees: Marquee[] ) => {
      this.marquees = marquees;

    }, error => {
      this.alertify.error(error);
    } );
  }

  deleteMarquee(id: number)
  {
    this.marqueeService.deleteMarquue(id).subscribe(response => {
      this.alertify.success('Usunięto namiot');
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