import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-place-select',
  templateUrl: './place-select.component.html',
  styleUrls: ['./place-select.component.css']
})
export class PlaceSelectComponent implements OnInit {


  places: any;

  constructor(private http: HttpClient, private marqueeService: MarqueeService, private router: Router) { }

  ngOnInit() {
    this.getPlaces();
  }

  getPlaces()
  {
    this.marqueeService.getPlaces().subscribe(response => {
      this.places = response;
    }, error => {
      console.log(error);
    });
  }

  selectPlace(id: number)
  {
    this.router.navigate(['eventsAdd/',id]);
  }
}
