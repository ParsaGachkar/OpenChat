
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NbThemeModule, NbLayoutModule, NbSidebarModule, NbCardModule, NbLayoutDirection, NbButtonModule } from "@nebular/theme";
import { NbEvaIconsModule } from "@nebular/eva-icons";
import { ReactiveFormsModule } from '@angular/forms';
import { PersianPipesModule } from 'angular2-persian-pipes';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: "default" }, [], [
      { name: 'mobile', width: 800 },
      { name: 'desktop', width: 1200 }
    ], NbLayoutDirection.RTL),
    NbLayoutModule,
    NbEvaIconsModule,
    NbSidebarModule.forRoot(),
    NbCardModule,
    NbButtonModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
