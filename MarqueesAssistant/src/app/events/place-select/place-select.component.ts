import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Apiresponseplace } from 'src/app/_models/apiResponsePlace';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-place-select',
  templateUrl: './place-select.component.html',
  styleUrls: ['./place-select.component.css']
})
export class PlaceSelectComponent implements OnInit {


  apiResponse: Apiresponseplace;
  totalItems: number;
  pageSize = 10;
  pageNumber = 1;
  searchString = '';
  sortBy: number = 1;


  constructor(private http: HttpClient, private marqueeService: MarqueeService, private router: Router
    , private alertify: AlertifyService , private authService: AuthService )  { }

  ngOnInit() {
    this.getPlaces();
  }



  selectPlace(id: number)
  {
    this.router.navigate(['eventsAdd/',id]);
  }

  checkRole()
  {
    return this.authService.checkRole();
  }

  getPlaces()
  {
    this.marqueeService.getSearchedPlaces(this.pageNumber, this.pageSize, this.searchString, this.sortBy)
    .subscribe( ( apiResponsepPlaces: Apiresponseplace ) => {
      this.apiResponse = apiResponsepPlaces;
      this.totalItems = apiResponsepPlaces.totalPages * apiResponsepPlaces.pageSize;
      
    }, error => {
      this.alertify.error(error);
    }
    );
  }

  setPage(page: number)
  {
    this.pageNumber = page;
    this.getPlaces();

    
    
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




