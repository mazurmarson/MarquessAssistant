import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { from } from 'rxjs';
import { MarqueeComponent } from './marquee/marquee.component';
import { EventsComponent } from './events/events.component';
import { PlacesComponent } from './places/places.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, NgControl, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { WorkerService } from './_services/worker.service';
import { WorkersListComponent } from './workers-list/workers-list.component';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { CommonModule } from '@angular/common';
import { MarqueeListComponent } from './marquee/marquee-list/marquee-list.component';
import { MarqueeAddComponent } from './marquee/marquee-add/marquee-add.component';
import { PlacesAddComponent } from './places/places-add/places-add.component';
import { EventsAddComponent } from './events/events-add/events-add.component';
import { PlaceSelectComponent } from './events/place-select/place-select.component';
import { EventSelectComponent } from './marquee/event-select/event-select.component';
import { PlaceEditComponent } from './places/place-edit/place-edit.component';
import { EventEditComponent } from './events/event-edit/event-edit.component';
import { MarqueeEditComponent } from './marquee/marquee-edit/marquee-edit.component';
import { AuthGuard } from './_guard/auth.guard';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { MessageComponent } from './message/message.component';
import { ConversationComponent } from './message/conversation/conversation.component';
import { EventStuffComponent } from './events/event-stuff/event-stuff.component';
import { MarqueeStuffComponent } from './marquee/marquee-stuff/marquee-stuff.component';
import { Auth2Guard } from './_guard/auth2.guard';
import { Auth3Guard } from './_guard/auth3.guard';
import { WorkerEditComponent } from './workers-list/WorkerEdit/WorkerEdit.component';
import { WorkerEditByAdminComponent } from './workers-list/worker-edit-by-admin/worker-edit-by-admin.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';



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
      WorkersListComponent,
      MarqueeListComponent,
      MarqueeAddComponent,
      PlacesAddComponent,
      EventsAddComponent,
      PlaceSelectComponent,
      EventSelectComponent, 
      PlaceEditComponent,
      EventEditComponent,
      MarqueeEditComponent,
      MessageComponent,
      ConversationComponent,
      EventStuffComponent,
      MarqueeStuffComponent,
      WorkerEditComponent,
      WorkerEditByAdminComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    PaginationModule.forRoot(),
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
    WorkerService,
    AuthGuard,
    Auth2Guard,
    Auth3Guard,

    ErrorInterceptorProvider

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
