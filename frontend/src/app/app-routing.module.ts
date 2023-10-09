import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {BoxFeedComponent} from "./BoxFeedComponent";

const routes: Routes = [

  {
    path: '',
    redirectTo: 'boxes',
    pathMatch: 'full'
  },
  {
    path: 'boxes',
    component: BoxFeedComponent

  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
