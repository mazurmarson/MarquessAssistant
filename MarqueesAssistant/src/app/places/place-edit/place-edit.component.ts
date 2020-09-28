import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { Place } from 'src/app/_models/place';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-place-edit',
  templateUrl: './place-edit.component.html',
  styleUrls: ['./place-edit.component.css']
})
export class PlaceEditComponent implements OnInit {

  place: Place;
  id: number;
  model: any;
  isLoaded: boolean = false;
  

  constructor(private alertify: AlertifyService, private marqueeService: MarqueeService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
   }

  ngOnInit() {
   this.getPlace(this.id);

  }



  getPlace(id:number)
  {
    this.marqueeService.getPlace(id).subscribe( (place: Place) => {
      this.model = place;
      this.isLoaded = true;
    } );
    
  }

  



  editPlace()
  {
    this.marqueeService.editPlace(this.model).subscribe( () => {
      this.alertify.success('Edycja udana');
    }, error => {
      this.alertify.error('Wystąpił problem');
    });

    
  }

}
