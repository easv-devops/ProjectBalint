import {Component, OnInit} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpResponse} from "@angular/common/http";
import {environment} from "../environments/environment";
import {first, firstValueFrom} from "rxjs";
import {Box, ResponseDto} from "../models";
import {State} from "../state";
import {ModalController, ToastController} from "@ionic/angular";
import {CreateBoxComponent} from "./CreateBoxComponent";

@Component({
  template: `
    <h1 id="title">Welcome to the Box Factory!</h1>
    <hr>
    <div class="main-container">
      <div id="content-container">
        <div id="search-container">
          <input type="text" id="search-bar">
          <button id="search-button">Search</button>
        </div>

        <div id="box-container">
          <div class="box" *ngFor="let box of state.boxes">
            <div class="box-content">
              <h2 class="name">{{box.name}}</h2>

              <div class="property-content">
                <div class="box-properties">
                  <p>Id: {{box.id}}</p>
                  <span>Volume: {{box.volume}}</span>
                </div>

                <div class="box-design">
                  <span>Color: </span>
                  <p>{{box.color}}</p>
                </div>
              </div>

              <div class="buttons">
                <button class="menu-buttons" (click)="deleteBox(box.id)">Delete</button>
              </div>
            </div>
          </div>

        </div>
      </div>
    </div>
    <ion-fab>
      <ion-fab-button (click)="openModal()">
        <ion-icon name="add-circle-outline"></ion-icon>
      </ion-fab-button>
    </ion-fab>
  `,
  selector: 'boxFeedComponent'
})


export class BoxFeedComponent implements OnInit {
  constructor(public http: HttpClient, public modalController: ModalController,
              public state: State, public toastController : ToastController) {

  }

  async fetchBoxes() {
    const result = await firstValueFrom<ResponseDto<Box[]>>(this.http.get(environment.baseURL + '/api/boxes'))
    this.state.boxes = result.responseData!;
  }

  ngOnInit(): void {
    this.fetchBoxes()
  }

  async deleteBox(id: number | undefined) {
    try {
      const result = await firstValueFrom(this.http.delete(environment.baseURL + "/api/DeleteBox/" + id))
      this.state.boxes = this.state.boxes.filter(box => box.id != id)
      const toast = await this.toastController.create({
        message: "The box was successfully deleted!",
        duration: 4500,
        color: "success"
      })
      toast.present();
    } catch (error) {
      if (error instanceof HttpErrorResponse) {
        const toast = await this.toastController.create({
          message: error.error.messageToClient,
          duration: 4500,
          color: "danger"

        })
        toast.present();
      }
    }
  }

  async openModal() {
    const modal = await this.modalController.create({
      component: CreateBoxComponent
    });
    modal.present();
  }
}