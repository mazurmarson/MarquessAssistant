import { from } from 'rxjs'
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { WorkersListComponent } from './workers-list/workers-list.component';
import { MarqueeComponent } from './marquee/marquee.component';
import { MarqueeListComponent } from './marquee/marquee-list/marquee-list.component';
import { MarqueeAddComponent } from './marquee/marquee-add/marquee-add.component';
import { PlacesComponent } from './places/places.component';
import { PlacesAddComponent } from './places/places-add/places-add.component';
import { EventsComponent } from './events/events.component';
import { EventsAddComponent } from './events/events-add/events-add.component';
import { PlaceSelectComponent } from './events/place-select/place-select.component';
import { EventSelectComponent } from './marquee/event-select/event-select.component';
import { PlaceEditComponent } from './places/place-edit/place-edit.component';
import { EventEditComponent } from './events/event-edit/event-edit.component';
import { MarqueeEditComponent } from './marquee/marquee-edit/marquee-edit.component';
import { AuthGuard } from './_guard/auth.guard';
import { MessageComponent } from './message/message.component';
import { ConversationComponent } from './message/conversation/conversation.component';
import { EventStuffComponent } from './events/event-stuff/event-stuff.component';

export const appRoutes: Routes = [
    {
        path: 'home', component: HomeComponent
    },
    {
        path: 'workers', component: WorkersListComponent, canActivate: [AuthGuard]
    },
    {
        path: 'marquees', component: MarqueeComponent
    },
    {
        path: 'marqueesList', component: MarqueeListComponent
    },
    {
        path: 'marqueesAdd/:id', component: MarqueeAddComponent
    },
    {
        path: 'placesList', component: PlacesComponent
    },
    {
        path: 'placesAdd', component: PlacesAddComponent
    },
    {
        path: 'eventsList', component: EventsComponent
    },
    {
        path: 'eventsAdd/:id', component: EventsAddComponent
    },
    {
        path: 'placeSelect', component: PlaceSelectComponent
    },
    {
        path: 'eventSelect', component: EventSelectComponent
    },
    {
        path: 'placeEdit/:id', component: PlaceEditComponent
    },
    {
        path: 'eventEdit/:id', component: EventEditComponent
    },
    {
        path: 'marqueeEdit/:id', component: MarqueeEditComponent
    },
    {
        path: 'messages/:id', component: MessageComponent
    },
    {
        path: 'conversation/:id', component: ConversationComponent
    },
    {
        path: 'eventStuff/:id', component: EventStuffComponent
    }
    




];