import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';

@Component({
  selector: 'app-breakdown-list',
  templateUrl: './breakdown-list.component.html',
  styleUrls: ['./breakdown-list.component.css']
})
export class BreakdownListComponent implements OnInit {

 breakdowns: any;

  constructor(private http: HttpClient, private alertify: AlertifyService,
    private router: Router, private breakdownService: BreakdownService, private authService: AuthService) { }

  ngOnInit() {
    this.getBreakdowns();
  }

  getBreakdowns()
  {
    this.breakdownService.getBreakdowns().subscribe( response => {
      this.breakdowns = response;
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteBreakdown(id: number)
  {
    this.breakdownService.deleteBreakdown(id).subscribe( response => {
      this.alertify.success('Usunięto awarie');
    }, error => {
      this.alertify.error(error);
    });
  }

  checkRole()
  {
    return this.authService.checkRole();
  }

}
