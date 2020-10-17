import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Marquee } from 'src/app/_models/marquee';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-event-stuff',
  templateUrl: './event-stuff.component.html',
  styleUrls: ['./event-stuff.component.css']
})
export class EventStuffComponent implements OnInit {

  id: number;
  marquees: Marquee[];
  smallLegs: number = 0;
  bigLegs: number = 0;
  rafters3m: number = 0;
  rafters6m: number = 0;
  rafters9m: number = 0;
  rafters12m: number = 0;
  rafters15m: number =0;
  smallPlates: number = 0;
  bigPlates: number = 0;
  walls: number = 0;
  roofs3m: number = 0;
  roofs6m: number = 0;
  roofs9m: number = 0;
  roofs12m: number = 0;
  roofs15m: number = 0;
  perlines: number = 0;
  smallHookups: number = 0;
  bigHookups: number =0;
  smallApexes: number = 0;
  bigApexes: number=0;
  endleg3to9: number =0;
  longEndlegs: number =0;
  shortEndlegs: number =0;

  bases:number;
  ilosc:number =0;

  constructor(private marqueeService: MarqueeService, private alertify: AlertifyService, private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
    
  }

  ngOnInit() {
   this.loadMarquees();
  
   
  }

  loadMarquees()
  {
    this.marqueeService.getEventStuff(this.id).subscribe( (marquees: Marquee[] ) => {
      this.marquees = marquees;
      this.countStuff();
    }, error => {
      this.alertify.error(error);
    } );
  }

  countStuff()
  {

    const zmienna = this.marquees.map((marquee) => {
      this.bases = marquee.length / 3;
      switch(marquee.width){
        case 3:
          this.smallLegs = this.smallLegs + (this.bases * 2) + 2;
          this.rafters3m += (this.bases * 2) + 2;
          this.smallPlates += (this.bases * 2) + 2;
          this.walls += (this.bases * 2) + 2;
          this.roofs3m += this.bases;
          this.perlines += this.bases;
          this.smallHookups += this.bases * 2;
          this.smallApexes += this.bases + 1;

          break;
  
          case 6:
            this.smallLegs = this.smallLegs + (this.bases * 2) +2;
            this.rafters6m += (this.bases * 2) + 2;
            this.smallPlates += (this.bases * 2) + 4;
            this.walls += (this.bases * 2) +4;
            this.roofs6m += (this.bases);
            this.perlines += (this.bases * 3);
            this.smallHookups += (this.bases * 2);
            this.smallApexes += (this.bases) + 1;
            this.endleg3to9 += 2;
            
            break;
  
           case 9:
            this.smallLegs = this.smallLegs + (this.bases * 2)+2;
            this.rafters9m += (this.bases * 2) + 2;
            this.smallPlates += (this.bases * 2) + 6;
            this.walls += (this.bases * 2) + 6;
            this.roofs9m += (this.bases);
            this.perlines += (this.bases * 7);
            this.smallHookups += (this.bases *2);
            this.smallApexes += (this.bases) + 1;
            this.endleg3to9 += 4; 
            
            break;

            case 12:
              this.bigLegs += (this.bases *2) +2;
              this.rafters12m += (this.bases * 2 ) + 2;
              this.bigPlates += (this.bases * 2) + 8;
              this.walls += (this.bases * 2) + 8;
              this.roofs12m += (this.bases);
              this.perlines += (this.bases * 7);
              this.bigHookups += (this.bases * 2);
              this.bigApexes += (this.bases) +1;
              this.longEndlegs += 2;
              this.shortEndlegs += 4;
            
            break;

            case 15:
              
            break;
      }
    } );

  


    console.log("Ilosc nóg " + this.smallLegs);
    console.log("Ilosc krokwi " + this.rafters3m);
    console.log("Ilosc podstawek " + this.smallPlates);
    console.log("Ilosc ścian " + this.walls);
    console.log("Ilosc dachów " + this.roofs3m);
    console.log("Ilosc wewnętrznych łączników " + this.perlines);
    console.log("Ilosc zewnętrznych łączników " + this.smallHookups);
    console.log("Ilosc wierzchołków " + this.smallApexes);
  }


  
  



}
