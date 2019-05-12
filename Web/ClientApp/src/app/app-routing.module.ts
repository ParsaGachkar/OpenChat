
import { NgModule } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: './main/main.module#MainModule', pathMatch: 'prefix' },
  { path: 'WebApp', loadChildren: './web-app/web-app.module#WebAppModule', pathMatch: 'prefix' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
