import {Component, OnInit } from "@angular/core";
import {Router} from "@angular/router";
import {State} from "../../state";
import {TokenService} from "../services/token.service";
import {JwtService} from "../services/jwt.service";

@Component({
  template: `
    <div id="page-container">
      <div id="top-bar">

        <div id="user-container">
          <div id="user-background" (click)="editUser()">
            <img src="../assets/images/hive.png" height="50" width="50">
          </div>
          <div id="user-controls">
            <p id="username" (click)="editUser()">{{username}}</p>
            <p id="log-out" (click)="logout()">Log <span id="out">out</span></p>
          </div>

        </div>
        <div id="warning-triangle">
          !
        </div>
      </div>

      <div id="field-container">

        <div class="field">

          <div class="field-top">
            Field Name
            <button class="field-edit-button">...</button>
          </div>
          <div class="field-content">
            <div class="hive">
              <div class="hive-top">
                <p class="hive-name">Hive name</p>
              </div>
              <div class="hive-middle">
                <p>ID: 1</p>
              </div>
              <div class="hive-bottom">
                <button class="manage-hive-button">Manage</button>
              </div>
            </div>

          </div>

        </div>

      </div>

    </div>
  `,
  styleUrls: ['../css/beesFeed.component.scss'],
  selector: 'bees-feed'
})

export class BeesFeedComponent implements OnInit {
  username: any;
  constructor(private router: Router, private jwtService: JwtService,
              public state: State, public tokenService: TokenService) {
  }
  ngOnInit(): void {
    console.log("BeesFeedComponent initialized");

    const token = this.tokenService.getToken();

    if (token) {
      const decodedToken = this.jwtService.decodeToken(token);
      this.username = decodedToken ? decodedToken['sub'] : null;
    } else {
      this.router.navigate(['login-component']);
    }
  }

  logout() {
    this.tokenService.removeToken();
    this.router.navigate(['login-component']);
  }

  editUser() {
    this.router.navigate(['user-component']);
  }
}
