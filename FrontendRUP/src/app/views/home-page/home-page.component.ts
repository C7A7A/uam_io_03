import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  constructor(private authService: AuthService) { }

  isLoggedIn: boolean;

  ngOnInit(): void {
    this.authService.isLoggedInObservable.subscribe((val) => {
      this.isLoggedIn = val;
    });
  }

}
