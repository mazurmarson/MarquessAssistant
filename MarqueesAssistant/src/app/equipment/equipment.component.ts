import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Apiresponseequipment } from '../_models/apiresponseequipment';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { BreakdownService } from '../_services/breakdown.service';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit {

  apiResponse: Apiresponseequipment;
  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  searchString = '';
  sortBy: number = 1;


  constructor(private http: HttpClient, private alertify: AlertifyService, private router: Router, private breakdownService: BreakdownService, private authService: AuthService) { }

  ngOnInit() {
    this.getEquipments();
  }

  getEquipments()
  {
    this.breakdownService.getSearchedEquipment(this.pageNumber, this.pageSize, this.searchString, this.sortBy)
    .subscribe( (apiResponseEquipments: Apiresponseequipment) => {
      this.apiResponse = apiResponseEquipments;
      this.totalItems = apiResponseEquipments.totalPages * apiResponseEquipments.pageSize;
      console.log(apiResponseEquipments);
      console.log(this.totalItems);
    }, error => {
      this.alertify.error(error);
    } ) ;
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

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getEquipments();
   
  }
  
  setColor(id: number)
  {
    if(this.sortBy === id)
    {

      return 'arrow';
    }
    else
    {
      return 'arrowCurrent';
    }
  }




}
