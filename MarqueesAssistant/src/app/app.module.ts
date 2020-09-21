import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { HttpClientModule } from '@angular/common/http';
import { from } from 'rxjs';
import { MarqueeComponent } from './marquee/marquee.component';
import { EventsComponent } from './events/events.component';
import { PlacesComponent } from './places/places.component';

@NgModule({
  declarations: [				
    AppComponent,
      ValueComponent,
      MarqueeComponent,
      EventsComponent,
      PlacesComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
