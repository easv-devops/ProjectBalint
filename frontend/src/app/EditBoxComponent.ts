import {Component, Input} from "@angular/core";
import {FormBuilder, Validators} from "@angular/forms";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ModalController, ToastController} from "@ionic/angular";
import {firstValueFrom} from "rxjs";
import {Box, ResponseDto} from "../models";
import {environment} from "../environments/environment";
import {State} from "../state";

@Component({
  template: `
    <ion-list>
      <ion-item>
        <ion-input [formControl]="editBoxForm.controls.name" label="Box Name:"></ion-input>
      </ion-item>
      <ion-item>
        <ion-input [formControl]="editBoxForm.controls.volume" label="Box Volume(ml):"></ion-input>
        <div *ngIf="editBoxForm.controls.volume.invalid">
          Numbers only.
        </div>
      </ion-item>
      <ion-item>
        <ion-input [formControl]="editBoxForm.controls.color" label="Box Color:"></ion-input>
      </ion-item>
      <ion-item>
        <ion-input [formControl]="editBoxForm.controls.description" label="Box Description"></ion-input>
      </ion-item>
      <ion-item>
        <ion-button [disabled]="editBoxForm.invalid" (click)="submit()">Save</ion-button>
      </ion-item>
    </ion-list>
  `
})
export class EditBoxComponent{

  editBoxForm = this.fb.group({
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
      const response = await firstValueFrom(this.http.put<ResponseDto<Box>>(environment.baseURL+'/api/box/7', this.editBoxForm.getRawValue()));
      this.state.boxes = this.state.boxes.filter(box => box.id != 7)
      this.state.boxes.push(response.responseData!);
      const toast = await this.toastController.create({
        message: "The box was successfully created!",
        duration: 4500,
        color: "success"
      })
      toast.present();
      this.modalController.dismiss();
      location.reload();
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
