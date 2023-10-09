import {Component} from "@angular/core";
import {FormBuilder, Validators} from "@angular/forms";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Box, ResponseDto} from "../models";
import {State} from "../state";
import {firstValueFrom} from "rxjs";
import {ModalController, ToastController} from "@ionic/angular";

@Component({
  template: `
    <ion-list>
      <ion-item>
        <ion-input [formControl]="createNewBoxForm.controls.name" label="Box Name:"></ion-input>
      </ion-item>
      <ion-item>
        <ion-input [formControl]="createNewBoxForm.controls.volume" label="Box Volume(ml):"></ion-input>
          <div *ngIf="createNewBoxForm.controls.volume.invalid">
            Numbers only.
          </div>
      </ion-item>
      <ion-item>
        <ion-input [formControl]="createNewBoxForm.controls.color" label="Box Color:"></ion-input>
      </ion-item>
      <ion-item>
        <ion-input [formControl]="createNewBoxForm.controls.description" label="Box Description"></ion-input>
      </ion-item>
      <ion-item>
        <ion-button [disabled]="createNewBoxForm.invalid" (click)="submit()">Add</ion-button>
      </ion-item>
    </ion-list>
  `
})
export class CreateBoxComponent{

  createNewBoxForm = this.fb.group({
    name: ['', Validators.required],
    volume: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    color: ['', Validators.required],
    description: ['', Validators.required]
  })

  constructor(public fb: FormBuilder, public http: HttpClient, public state: State,
              public toastController: ToastController, public modalController: ModalController) {

  }


  async submit() {
    try{
      const response = await firstValueFrom(this.http.post<ResponseDto<Box>>(environment.baseURL+'/api/box', this.createNewBoxForm.getRawValue()));
      this.state.boxes.push(response.responseData!);
      const toast = await this.toastController.create({
        message: "The box was successfully created!",
        duration: 4500,
        color: "success"
      })
      toast.present();
      this.modalController.dismiss();
    }catch (error){
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
}
