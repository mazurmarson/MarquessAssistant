import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { from } from 'rxjs';
import { MarqueeComponent } from './marquee/marquee.component';
import { EventsComponent } from './events/events.component';
import { PlacesComponent } from './places/places.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { WorkerService } from './_services/worker.service';
import { WorkersListComponent } from './workers-list/workers-list.component';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';

export function tokenGetter()
{
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [									
    AppComponent,
      MarqueeComponent,
      EventsComponent,
      PlacesComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      WorkersListComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
         tokenGetter,
         allowedDomains: ['localhost:5000'],
         disallowedRoutes: ['localhost:5000/api/auth']
      }
      
   }),
   RouterModule.forRoot(appRoutes)

  ],
  providers: [
    AuthService,
    AlertifyService,
    WorkerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
