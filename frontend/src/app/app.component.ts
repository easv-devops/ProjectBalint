import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <ion-app>
     <boxFeedComponent></boxFeedComponent>
    </ion-app>
  `,
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor() {}
}
