import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-events-add',
  templateUrl: './events-add.component.html',
  styleUrls: ['./events-add.component.css']
})
export class EventsAddComponent implements OnInit {

  model: any = {};
  id: number;
  shows: string[] = ["Agroshow", "Festiwal muzyczny", "Festiwal jedzenia", "Festyn", "Motoshow","Wydarzenie sportowe", "Inne"];
  
  constructor(private alertify: AlertifyService, private marqueeService: MarqueeService, private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
  }

  ngOnInit() {
    
  }

  addEvent()
  {
    this.marqueeService.addEvent(this.model, this.id).subscribe( () => {
      this.alertify.success('Dodano wydarzenie');
    }, error => {
      this.alertify.error(error); 
    });
  }



}
