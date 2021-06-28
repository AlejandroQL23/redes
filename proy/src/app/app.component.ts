import { Component, OnInit, Input} from '@angular/core';
import { RestService } from './rest.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'appBootstrap';

  @Input() gameData = { name:'', password:'', server:''};

  newGame() {
 
      console.log(this.gameData);
   
  }
  
  public isCollapsed = true;
  public isCollapsed2 = true;
}
