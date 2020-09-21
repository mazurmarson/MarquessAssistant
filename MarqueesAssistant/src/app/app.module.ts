import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { HttpClientModule } from '@angular/common/http';
import { from } from 'rxjs';
import { MarqueeComponent } from './marquee/marquee.component';
import { EventsComponent } from './events/events.component';
import { PlacesComponent } from './places/places.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';

@NgModule({
  declarations: [					
    AppComponent,
      ValueComponent,
      MarqueeComponent,
      EventsComponent,
      PlacesComponent,
      NavComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
