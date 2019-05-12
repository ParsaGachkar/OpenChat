import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { ChatsComponent } from './chats/chats.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', component: ChatsComponent },
  { path: 'Login', component: LoginComponent },
  { path: ':id', component: ChatComponent, pathMatch: 'prefix' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WebAppRoutingModule { }
