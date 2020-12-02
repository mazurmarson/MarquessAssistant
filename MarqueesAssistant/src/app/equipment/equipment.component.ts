import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { BreakdownService } from '../_services/breakdown.service';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit {

  equipments: any;

  constructor(private http: HttpClient, private alertify: AlertifyService, private router: Router, private breakdownService: BreakdownService, private authService: AuthService) { }

  ngOnInit() {
    this.getEquipments();
  }

  getEquipments()
  {
    this.breakdownService.getEquipments().subscribe(response => {
      this.equipments = response;
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteEquipment(id: number)
  {
    this.breakdownService.deleteEquipment(id).subscribe(response => {
      this.alertify.success('Usunięto sprzęt');
    }, error => {
      this.alertify.error(error);
    });
  }

  checkRole()
  {
    return this.authService.checkRole();
  }


}
