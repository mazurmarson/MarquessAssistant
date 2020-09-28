import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-marquee-add',
  templateUrl: './marquee-add.component.html',
  styleUrls: ['./marquee-add.component.css']
})
export class MarqueeAddComponent implements OnInit {

  model: any = {};
  @Output() cancelAdding = new EventEmitter();
  id: number;

  constructor(private alertify: AlertifyService, private marqueeService: MarqueeService, private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

  }

  ngOnInit() {
  }

  // addMarquee()
  // {
  //   console.log(this.model.isUp);
  // }

  addMarquee()
  {

    this.marqueeService.addMarquee(this.model, this.id).subscribe( () => {
     this.alertify.success('Dodano namiot');
    }, error => {
      this.alertify.error('Wystąpił problem');
    });

  }

   cancel()
   {
    this.cancelAdding.emit(false);
    this.alertify.message('Anulowano');
   }

}
