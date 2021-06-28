import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';


import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainComponent } from './main/main.component';
import { GameComponent } from './game/game.component';

const appRoutes: Routes = [
  {
    path: 'Main',
    component: MainComponent,
    data: { title: 'Principal' }
  },
  {
    path: 'Game',
    component: GameComponent,
    data: { title: 'Juego' }
  },
  { path: '',
  redirectTo: '/Main',
  pathMatch: 'full'
}

];
@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    GameComponent
    
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
