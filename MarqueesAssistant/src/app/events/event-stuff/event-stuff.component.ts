import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Marquee } from 'src/app/_models/marquee';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { MarqueeService } from 'src/app/_services/marquee.service';

@Component({
  selector: 'app-event-stuff',
  templateUrl: './event-stuff.component.html',
  styleUrls: ['./event-stuff.component.css']
})
export class EventStuffComponent implements OnInit {
  
  model: any;
  placeName: any;
  WeatherData: any;
  id: number;
  marquees: Marquee[];
  smallLegs: number = 0;
  bigLegs: number = 0;
  rafters3m: number = 0;
  rafters6m: number = 0;
  rafters9m: number = 0;
  rafters12m: number = 0;
  rafters15m: number =0;
  smallPlates: number = 0;
  bigPlates: number = 0;
  walls: number = 0;
  roofs3m: number = 0;
  roofs6m: number = 0;
  roofs9m: number = 0;
  roofs12m: number = 0;
  roofs15m: number = 0;
  perlines: number = 0;
  smallHookups: number = 0;
  bigHookups: number =0;
  smallApexes: number = 0;
  bigApexes: number=0;
  endleg3to9: number =0;
  longEndlegs: number =0;
  shortEndlegs: number =0;
  PlaceIsFounded: boolean = false;
  otherDangerConditionVar:string;

  bases:number;
  ilosc:number =0;

  constructor(private marqueeService: MarqueeService, private alertify: AlertifyService, private route: ActivatedRoute, private authService: AuthService) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
    
  }

  ngOnInit() {
      this.getEventPlaceName();
      this.loadMarquees();
  //    this.getWeatherData();

      this.WeatherData = {
       main : {},
       isDay: true

     };
   
  }

  townNameIsLoaded()
  {
    if(this.placeName == null)
    {
      return false;
    }
    else
    {
      return true;
    }
  }

  getWeatherData()
  {
     
  //  console.log('http://api.openweathermap.org/data/2.5/weather?q=' + this.placeName + '&lang=pl&appid=3179fc058bf45bb4168f2c6f73f7d863');
 //   
 //fetch('http://api.openweathermap.org/data/2.5/weather?q=krosno&lang=pl&appid=3179fc058bf45bb4168f2c6f73f7d863')
 fetch('http://api.openweathermap.org/data/2.5/weather?q=' + this.placeName + '&lang=pl&appid=3179fc058bf45bb4168f2c6f73f7d863')
    .then(response=>response.json())
    .then(data=>{this.setWeatherData(data);})
    console.log(this.placeName);
  }

  getEventPlaceName()
  {
    this.marqueeService.getEventPlaceName(this.id).subscribe((response : any) => {
      this.placeName = response.content;
      console.log(this.placeName);
      this.getWeatherData();
    }, error => {
      this.alertify.error(error);
    });
  }

  setWeatherData(data)
  {
    if(data.cod=='404')
    {
      console.log('Warunek działa');
      return false;
    }
    else
    {
      console.log(data.weather[0].description);
      this.WeatherData = data;
      let currentDate = new Date();
      this.WeatherData.temp_celcius = (this.WeatherData.main.temp - 273.15).toFixed(2);
      this.WeatherData.temp_min = (this.WeatherData.main.temp_min - 273.15).toFixed(0);
      this.WeatherData.temp_max = (this.WeatherData.main.temp_max - 273.15).toFixed(0);
      this.WeatherData.temp_feels_like = (this.WeatherData.main.feels_like - 273.15).toFixed(0);
      this.WeatherData.wind_speed = (this.WeatherData.wind.speed * 3.6).toFixed(2);
       this.WeatherData.description = (this.WeatherData.weather[0].description);
       this.WeatherData.id = this.WeatherData.weather[0].id;
       console.log(this.WeatherData);
       this.WeatherData.town = (this.WeatherData.name);
      // this.WeatherData.cosTam = (this.WeatherData.weather.main);
    this.WeatherData.cloudiness = (this.WeatherData.clouds.all);
      this.PlaceIsFounded = true;
     
    }

  }






  loadMarquees()
  {
    this.marqueeService.getEventStuff(this.id).subscribe( (marquees: Marquee[] ) => {
      this.marquees = marquees;
      this.countStuff();
    }, error => {
      this.alertify.error(error);
    } );
  }

  countStuff()
  {

    const zmienna = this.marquees.map((marquee) => {
      this.bases = marquee.length / 3;
      switch(marquee.width){
        case 3:
          this.smallLegs = this.smallLegs + (this.bases * 2) + 2;
          this.rafters3m += (this.bases * 2) + 2;
          this.smallPlates += (this.bases * 2) + 2;
          this.walls += (this.bases * 2) + 2;
          this.roofs3m += this.bases;
          this.perlines += this.bases;
          this.smallHookups += this.bases * 2;
          this.smallApexes += this.bases + 1;

          break;
  
          case 6:
            this.smallLegs = this.smallLegs + (this.bases * 2) +2;
            this.rafters6m += (this.bases * 2) + 2;
            this.smallPlates += (this.bases * 2) + 4;
            this.walls += (this.bases * 2) +4;
            this.roofs6m += (this.bases);
            this.perlines += (this.bases * 3);
            this.smallHookups += (this.bases * 2);
            this.smallApexes += (this.bases) + 1;
            this.endleg3to9 += 2;
            
            break;
  
           case 9:
            this.smallLegs = this.smallLegs + (this.bases * 2)+2;
            this.rafters9m += (this.bases * 2) + 2;
            this.smallPlates += (this.bases * 2) + 6;
            this.walls += (this.bases * 2) + 6;
            this.roofs9m += (this.bases);
            this.perlines += (this.bases * 7);
            this.smallHookups += (this.bases *2);
            this.smallApexes += (this.bases) + 1;
            this.endleg3to9 += 4; 
            
            break;

            case 12:
              this.bigLegs += (this.bases *2) +2;
              this.rafters12m += (this.bases * 2 ) + 2;
              this.bigPlates += (this.bases * 2) + 8;
              this.walls += (this.bases * 2) + 8;
              this.roofs12m += (this.bases);
              this.perlines += (this.bases * 7);
              this.bigHookups += (this.bases * 2);
              this.bigApexes += (this.bases) +1;
              this.longEndlegs += 2;
              this.shortEndlegs += 4;
            
            break;

            case 15:
              
            break;
      }
    } );

  



  }


  deleteMarquee(id: number)
  {
    this.marqueeService.deleteMarquue(id).subscribe(response => {
      this.alertify.success('Usunięto namiot');
    }, error => {
      this.alertify.error('Wystąpił błąd');
    });
  }

  checkRole()
  {
    return this.authService.checkRole();
  }

  
  tooStrongWind()
  {
    if(this.WeatherData.wind_speed > 40)
    {
      return true;
    }
    else
    {
      return false 
    }
  }

  dangerTemperature()
  {
    if(this.WeatherData.temp_celcius > 30)
    {
      return 1;
    }
    if(this.WeatherData.temp_celcius < 5)
    {
      return 2;
    }
    return 0;

  }

  otherDangerCondition()
  {
    if(this.WeatherData.id >= 200 && this.WeatherData.id <= 232 )
    {
      
      this.otherDangerConditionVar = 'Uwaga: Burza!';
      return true;
    }

    if(this.WeatherData.id == 701 || this.WeatherData.id == 721 || this.WeatherData.id == 741)
    {
      
      this.otherDangerConditionVar = 'Uwaga: Mgła!';
      return true;
    }

    if(this.WeatherData.id == 602 || this.WeatherData.id == 622)
    {
      
      this.otherDangerConditionVar = 'Uwaga: Intensywny śnieg!';
      return true;
    }

    if(this.WeatherData.id == 611 || this.WeatherData.id == 613 || this.WeatherData.id == 616)
    {
      
      this.otherDangerConditionVar = 'Uwaga: Śnieg z deszczem!';
      return true;
    }

    if(this.WeatherData.id == 511)
    {
     
      this.otherDangerConditionVar = 'Uwaga: Zmarznięty deszcz';
      return true;
    }

    if(this.WeatherData.id == 521 || this.WeatherData.id == 522 || this.WeatherData.id == 531)
    {
      
      this.otherDangerConditionVar = 'Uwaga: Intensywny deszcz';
      return true;
    }

    return false;


  }


  



}
