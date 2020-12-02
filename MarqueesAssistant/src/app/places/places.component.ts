import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Apiresponseplace } from '../_models/apiResponsePlace';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { MarqueeService } from '../_services/marquee.service';

@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.css']
})
export class PlacesComponent implements OnInit {

  apiResponse: Apiresponseplace;
  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  searchString = '';
  sortBy: number = 1;

  constructor(private http: HttpClient, private marqueeService: MarqueeService, private alertify: AlertifyService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.getPlaces();
  }
  
  checkRole()
  {
    return this.authService.checkRole();
  }

  // getPlaces()
  // {
  //   this.marqueeService.getPlaces().subscribe(response => {
  //     this.places = response;
  //   }, error => {
  //     console.log(error);
  //   });
  // }

  getPlaces()
  {
    this.marqueeService.getSearchedPlaces(this.pageNumber, this.pageSize, this.searchString, this.sortBy)
    .subscribe( ( apiResponsepPlaces: Apiresponseplace ) => {
      this.apiResponse = apiResponsepPlaces;
      this.totalItems = apiResponsepPlaces.totalPages * apiResponsepPlaces.pageSize;
      console.log(this.totalItems);
    }, error => {
      this.alertify.error(error);
    }
    );
  }

  deletePlace(id: number)
  {
    
    this.marqueeService.deletePlace(id).subscribe(response => {
      this.alertify.success('Usunięto miejsce');
    }, error => {
      this.alertify.error(error);
    });
  }

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getPlaces();
   
  }

  selectPlace(id: number)
  {
    this.router.navigate(['placeEdit/'+ id]);

    
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
