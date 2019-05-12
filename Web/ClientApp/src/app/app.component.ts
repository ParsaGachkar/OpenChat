import { Component } from "@angular/core";
import { NbSidebarService } from "@nebular/theme";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  title = "ClientApp";
  /**
   *
   */
  constructor(private sidebarsvc: NbSidebarService) {}
  toggle() {
    this.sidebarsvc.toggle();
  }
}
