import {Component, OnInit} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpResponse} from "@angular/common/http";
import {environment} from "../environments/environment";
import {first, firstValueFrom} from "rxjs";
import {Box, ResponseDto} from "../models";
import {State} from "../state";
import {ModalController, ToastController} from "@ionic/angular";
import {CreateBoxComponent} from "./CreateBoxComponent";
import {EditBoxComponent} from "./EditBoxComponent";
import {FormBuilder} from "@angular/forms";

@Component({
  template: `
    <h1 id="title">Welcome to the Box Factory!</h1>
    <hr>
    <div class="main-container">
      <div id="content-container">
        <div id="search-container">
          <input type="text" [formControl]="searchTerm.controls.searchValue" id="search-bar">
          <button id="search-button" (click)="searchForBox()">Search</button>
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
                  <span>Color: {{box.color}}</span>
                </div>
              </div>

              <div class="buttons">
                <button class="menu-buttons" id="delete-button" (click)="deleteBox(box.id)">Delete</button>
                <button class="menu-buttons" id="edit-button" (click)="openEditModal(box)">Edit</button>
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
              public state: State, public toastController: ToastController, public fb: FormBuilder) {

  }
  searchTerm = this.fb.group({
    searchValue: []
  })

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
  async openEditModal(selectedBox: Box) {
    this.state.currentBox = selectedBox;
    const modal = await this.modalController.create({
      component: EditBoxComponent
    });
    modal.present();
  }

  searchForBox() {
    var searchingFor= this.searchTerm.controls.searchValue.value!;
    if ((searchingFor === null || searchingFor === "")){
      this.fetchBoxes();
    }else {
      // @ts-ignore
      this.state.boxes = this.state.boxes.filter(box => box.name?.toUpperCase() == searchingFor.toUpperCase())
      if (this.state.boxes.length == 0)
        {
          this.fetchBoxes().then(()=>{
            // @ts-ignore
            this.state.boxes = this.state.boxes.filter(box => box.id.toString() === searchingFor)
          })
        }
    }
  }
}
