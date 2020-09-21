import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.css']
})
export class PlacesComponent implements OnInit {

  places: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getPlaces();
  }

  getPlaces()
  {
    this.http.get('http://localhost:5000/api/places').subscribe(response => {
      this.places = response;
    }, error => {
      console.log(error);
    });
  }

}
