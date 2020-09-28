import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';


@Component({
  selector: 'app-places-add',
  templateUrl: './places-add.component.html',
  styleUrls: ['./places-add.component.css']
})
export class PlacesAddComponent implements OnInit {

  model: any = {};
  

  constructor(private alertify: AlertifyService, private marqueeService: MarqueeService) { }

  ngOnInit() {
  }

  addPlace()
  {
    this.marqueeService.addPlace(this.model).subscribe( () => {
      this.alertify.success('Dodano miejsce');
    }, error => {
      this.alertify.error('Wystąpił problem');
    }
    );
  }

}
