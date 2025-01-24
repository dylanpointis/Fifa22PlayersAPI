import { Routes } from '@angular/router';
import { DetailCardComponent } from './Pages/detail-card/detail-card.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
    { path: 'detail-card/:id', component: DetailCardComponent }
];