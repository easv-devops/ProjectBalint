import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import {BoxFeedComponent} from "./BoxFeedComponent";
import {HttpClientModule} from "@angular/common/http";
import {CreateBoxComponent} from "./CreateBoxComponent";
import {ReactiveFormsModule} from "@angular/forms";
import {EditBoxComponent} from "./EditBoxComponent";

@NgModule({
  declarations: [AppComponent, BoxFeedComponent, CreateBoxComponent, EditBoxComponent],
  imports: [BrowserModule, IonicModule.forRoot(), AppRoutingModule, HttpClientModule, ReactiveFormsModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent],
})
export class AppModule {}
