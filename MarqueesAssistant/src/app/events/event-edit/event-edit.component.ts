import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-event-edit',
  templateUrl: './event-edit.component.html',
  styleUrls: ['./event-edit.component.css']
})
export class EventEditComponent implements OnInit {

  model: any;
  id: number;
  isLoaded: boolean = false;
  shows: string[] = ["Agroshow", "Festiwal muzyczny", "Festiwal jedzenia", "Festyn", "Motoshow","Wydarzenie sportowe", "Inne"];

  constructor(private marqueeService: MarqueeService, private alertify: AlertifyService, private route: ActivatedRoute, public router: Router) {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
   }

  ngOnInit() {
    this.getEvent(this.id);
    console.log(this.id);
  }

  getEvent(id: number)
  {
    this.marqueeService.getEvent(id).subscribe( (eventt: Event ) => {
      this.model = eventt;
      console.log(this.model.id);
      this.isLoaded = true;
    } );
  }

  editEvent()
  {
    this.marqueeService.editEvent(this.model).subscribe( () => {
      this.alertify.success('Edycja udana');
      this.router.navigate(['/eventsList']);
    }, error => {
      this.alertify.error(error);
    });
  }
  
  backToPrevious()
  {
    this.router.navigate(['/eventsList']);
  }

}
