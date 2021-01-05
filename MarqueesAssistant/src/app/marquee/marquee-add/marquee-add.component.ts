import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-marquee-add',
  templateUrl: './marquee-add.component.html',
  styleUrls: ['./marquee-add.component.css']
})
export class MarqueeAddComponent implements OnInit {

  model: any = {};

  id: number;

  constructor(private alertify: AlertifyService, private marqueeService: MarqueeService, private route: ActivatedRoute, public router: Router) { 
    this.route.params.subscribe(params => {
      this.id = (params['id']);
    });

  }

  ngOnInit() {
  }



  addMarquee()
  {

    this.marqueeService.addMarquee(this.model, this.id).subscribe( () => {
     this.alertify.success('Dodano namiot');
     this.router.navigate(['/eventStuff',this.id]);
    }, error => {
      this.alertify.error(error);
    });

  }

  backToPrevious()
  {
    this.router.navigate(['/eventStuff',this.id]);
  }



}
