import { from } from 'rxjs'
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { WorkersListComponent } from './workers-list/workers-list.component';
import { MarqueeComponent } from './marquee/marquee.component';

export const appRoutes: Routes = [
    {
        path: 'home', component: HomeComponent
    },
    {
        path: 'workers', component: WorkersListComponent
    },
    {
        path: 'marquees', component: MarqueeComponent
    },
];