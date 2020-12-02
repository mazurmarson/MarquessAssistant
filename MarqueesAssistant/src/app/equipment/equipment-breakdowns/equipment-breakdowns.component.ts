import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';

@Component({
  selector: 'app-equipment-breakdowns',
  templateUrl: './equipment-breakdowns.component.html',
  styleUrls: ['./equipment-breakdowns.component.css']
})
export class EquipmentBreakdownsComponent implements OnInit {

  breakdowns: any;
  id: number;
  isLoaded: boolean = false;

  constructor(private alertify: AlertifyService, private breakdownService: BreakdownService
    , private route: ActivatedRoute, private authService: AuthService) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
      
    });
  }

  ngOnInit() { 
    this.getBreakdowns(this.id);

  }


  getBreakdowns(id: number)
  {
    this.breakdownService.getEquipmentBreakdowns(id).subscribe( response => {
      this.breakdowns = response;
      console.log(this.breakdowns);
    }, error => {
      this.alertify.error(error);
     
    });
  }

  checkRole()
  {
    return this.authService.checkRole();
  }

}
