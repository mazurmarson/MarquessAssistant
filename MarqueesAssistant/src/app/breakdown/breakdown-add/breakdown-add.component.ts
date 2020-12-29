import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-breakdown-add',
  templateUrl: './breakdown-add.component.html',
  styleUrls: ['./breakdown-add.component.css']
})
export class BreakdownAddComponent implements OnInit {

  id: number;
  model: any  = {};
  constructor(private alertify: AlertifyService, private breakdownService: BreakdownService, private route: ActivatedRoute, public router: Router) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
     
    });
  }

  ngOnInit() {
  }

  addBreakdown()
  {
    this.breakdownService.addBreakdown(this.model, this.id).subscribe( () => {
      this.alertify.success('Dodano awarie');
      this.router.navigate(['/breakdownsList']);
    }, error => {
      this.alertify.error(error);
    });
 //   console.log(this.model);
  }

}
