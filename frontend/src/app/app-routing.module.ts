import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {BeesFeedComponent} from "./components/beesFeed.component";
import {LoginComponent} from "./components/login.component";
import {UserComponent} from "./components/user.component";


const routes: Routes = [
  { path: '', redirectTo: '/login-component', pathMatch: 'full' },
  { path: 'login-component', component: LoginComponent },
  { path: 'bees-feed', component: BeesFeedComponent },
  { path: 'user-component', component: UserComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
