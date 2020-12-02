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
import { Auth2Guard } from './_guard/auth2.guard';
import { Auth3Guard } from './_guard/auth3.guard';


import { MessageComponent } from './message/message.component';
import { ConversationComponent } from './message/conversation/conversation.component';
import { EventStuffComponent } from './events/event-stuff/event-stuff.component';
import { RegisterComponent } from './register/register.component';
import { MarqueeStuffComponent } from './marquee/marquee-stuff/marquee-stuff.component';
import { WorkerEditComponent } from './workers-list/WorkerEdit/WorkerEdit.component';
import { WorkerEditByAdminComponent } from './workers-list/worker-edit-by-admin/worker-edit-by-admin.component';
import { BreakdownComponent } from './breakdown/breakdown.component';
import { BreakdownListComponent } from './breakdown/breakdown-list/breakdown-list.component';
import { EquipmentComponent } from './equipment/equipment.component';
import { EquipmentAddComponent } from './equipment/equipment-add/equipment-add.component';
import { EquipmentEditComponent } from './equipment/equipment-edit/equipment-edit.component';
import { EquipmentBreakdownsComponent } from './equipment/equipment-breakdowns/equipment-breakdowns.component';
import { BreakdownAddComponent } from './breakdown/breakdown-add/breakdown-add.component';
import { BreakdownEditComponent } from './breakdown/breakdown-edit/breakdown-edit.component';

export const appRoutes: Routes = [
    {
        path: 'home', component: HomeComponent
    },
    {
        path: 'workers', component: WorkersListComponent, canActivate: [AuthGuard]
    },
    {
        path: 'workers/pageSize', component: WorkersListComponent, canActivate: [AuthGuard]
    },
    {
        path: 'marquees', component: MarqueeComponent, canActivate: [AuthGuard]
    },
    {
        path: 'marqueesList', component: MarqueeListComponent, canActivate: [AuthGuard]
    },
    {
        path: 'marqueesAdd/:id', component: MarqueeAddComponent, canActivate: [Auth2Guard]
    },
    {
        path: 'placesList', component: PlacesComponent, canActivate: [AuthGuard]
    },
    {
        path: 'placesAdd', component: PlacesAddComponent, canActivate: [Auth2Guard]
    },
    {
        path: 'eventsList', component: EventsComponent, canActivate: [AuthGuard]
    },
    {
        path: 'eventsAdd/:id', component: EventsAddComponent, canActivate: [Auth2Guard]
    },
    {
        path: 'placeSelect', component: PlaceSelectComponent, canActivate: [AuthGuard]
    },
    {
        path: 'eventSelect', component: EventSelectComponent, canActivate: [AuthGuard]
    },
    {
        path: 'placeEdit/:id', component: PlaceEditComponent, canActivate: [AuthGuard]
    },
    {
        path: 'eventEdit/:id', component: EventEditComponent, canActivate: [AuthGuard]
    },
    {
        path: 'marqueeEdit/:id', component: MarqueeEditComponent, canActivate: [AuthGuard]
    },
    {
        path: 'messages/:id', component: MessageComponent, canActivate: [AuthGuard]
    },
    {
        path: 'conversation/:id', component: ConversationComponent, canActivate: [AuthGuard]
    },
    {
        path: 'eventStuff/:id', component: EventStuffComponent, canActivate: [AuthGuard]
    },
    {
        path: 'marqueeStuff/:id', component: MarqueeStuffComponent, canActivate: [AuthGuard]
    },
    {
        path: 'register', component: RegisterComponent, canActivate: [Auth3Guard]
    },
    {
        path: 'editWorker', component: WorkerEditComponent, canActivate: [AuthGuard]
    },
    {
        path: 'editWorker/:id', component: WorkerEditByAdminComponent, canActivate: [Auth3Guard]
    },
    {
        path: 'breakdowns', component: BreakdownComponent, canActivate: [AuthGuard]
    },
    {
        path: 'breakdownsList', component: BreakdownListComponent, canActivate: [AuthGuard]
    },
    {
        path: 'breakdownsAdd/:id', component: BreakdownAddComponent, canActivate: [AuthGuard]
    },
    {
        path: 'equipments', component: EquipmentComponent, canActivate: [AuthGuard]
    },
    {
        path: 'equipmentsAdd', component: EquipmentAddComponent, canActivate: [AuthGuard]
    },
    {
        path: 'equipmentsEdit/:id', component: EquipmentEditComponent, canActivate: [AuthGuard]
    },
    {
        path: 'equipmentsBreakdowns/:id', component: EquipmentBreakdownsComponent, canActivate: [AuthGuard]
    },
    {
        path: 'breakdownsEdit/:id', component: BreakdownEditComponent, canActivate: [AuthGuard]
    },
    {
        path: '', component: HomeComponent
    }
    




];