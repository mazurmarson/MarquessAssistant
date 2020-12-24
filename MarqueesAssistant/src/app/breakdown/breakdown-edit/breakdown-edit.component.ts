import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Breakdown } from 'src/app/_models/breakdown';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';

@Component({
  selector: 'app-breakdown-edit',
  templateUrl: './breakdown-edit.component.html',
  styleUrls: ['./breakdown-edit.component.css']
})
export class BreakdownEditComponent implements OnInit {
  model: any = {};
  id:number;
  isLoaded:boolean;
  constructor(private alertify: AlertifyService, private breakdownServiece: BreakdownService, private route: ActivatedRoute) 
  { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
      
    });
  }

  ngOnInit() {
    this.getBreakdown();
  }

  editBreakdown()
  {
    this.breakdownServiece.editBreakdown(this.model).subscribe( () => {
      this.alertify.success('Edytowano sprzęt');
    }, error => {
      this.alertify.error(error);
    });
  }

  getBreakdown()
  {
    this.model = this.breakdownServiece.getBreakdown(this.id).subscribe( (breakdown: Breakdown) => {
      this.model = breakdown;
      this.isLoaded = true; 
    } ) ;
  }

}
