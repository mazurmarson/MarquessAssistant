import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Marquee } from 'src/app/_models/marquee';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-marquee-edit',
  templateUrl: './marquee-edit.component.html',
  styleUrls: ['./marquee-edit.component.css']
})
export class MarqueeEditComponent implements OnInit {

  id: number;
  model: any;
  isLoaded: boolean = false;

  constructor(private alertify: AlertifyService, private marqueeService: MarqueeService, private route: ActivatedRoute, public router: Router) {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      
    });
   }

  ngOnInit() {
    this.getMarquee(this.id);
  }

  getMarquee(id:number)
  {
    this.marqueeService.getMarquee(id).subscribe( (marquee: Marquee) => {
      this.model = marquee;
      this.isLoaded = true;
    } );
  }

  editMarquee()
  {
    this.marqueeService.editMarquee(this.model).subscribe( () => {
      this.alertify.success('Edycja udana');
      this.router.navigate(['/eventStuff',this.model.eventId]);
    }, error => {
      this.alertify.error(error);
    });
  }

}
