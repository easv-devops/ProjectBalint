import {Component, OnInit } from "@angular/core";
import {Router} from "@angular/router";
import {State} from "../../state";
import {TokenService} from "../services/token.service";
import {JwtService} from "../services/jwt.service";

@Component({
  template: `
    <div id="page-container">


    </div>
  `,
  styleUrls: ['../css/accounts.component.scss'],
  selector: 'accounts'
})

export class AccountsComponent implements OnInit {
  username: any;
  rank: any;
  constructor(private router: Router, private jwtService: JwtService,
              public state: State, public tokenService: TokenService) {
  }
  ngOnInit(): void {
    console.log("accounts initialized");

    const token = this.tokenService.getToken();

    if (token) {
      const decodedToken = this.jwtService.decodeToken(token);
      this.username = decodedToken ? decodedToken['sub'] : null;
      this.rank = decodedToken ? decodedToken['rank'] : null;
    } else {
      this.router.navigate(['login-component']);
    }
  }

  back() {
    this.router.navigate(['bees-component']);
  }
}
