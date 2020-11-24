import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { MarqueeService } from '../_services/marquee.service';

@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.css']
})
export class PlacesComponent implements OnInit {

  places: any;

  constructor(private http: HttpClient, private marqueeService: MarqueeService, private alertify: AlertifyService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.getPlaces();
  }
  
  checkRole()
  {
    return this.authService.checkRole();
  }

  getPlaces()
  {
    this.marqueeService.getPlaces().subscribe(response => {
      this.places = response;
    }, error => {
      console.log(error);
    });
  }

  deletePlace(id: number)
  {
    
    this.marqueeService.deletePlace(id).subscribe(response => {
      this.alertify.success('Usunięto miejsce');
    }, error => {
      this.alertify.error(error);
    });
  }

  selectPlace(id: number)
  {
    this.router.navigate(['placeEdit/'+ id]);

    
  }

}
