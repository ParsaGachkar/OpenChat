import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  GotoWebApp() {
    this.router.navigate(['/WebApp']);
  }

  GotoSourceCode(){
    window.location.href = 'https://github.com/ParsaGachkar/OpenChat/'
  }
  GotoReleases(){
    window.location.href = 'https://github.com/ParsaGachkar/OpenChat/releases'
  }
}
