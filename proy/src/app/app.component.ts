import { Component, OnInit, Input} from '@angular/core';
import { RestService } from './rest.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit {
  title = 'appBootstrap';
  games:any = [];
  game : any;
  @Input() gameData = {ownerGame:'', password:'', name:''};
  @Input() playerData = {gameId:'', password:'', name:''};
  closeResult: string;

  constructor(public rest:RestService, private route: ActivatedRoute, private router: Router,  private modalService: NgbModal) { }



  ngOnInit() {
    this.getGames();
    this.getRoundsByID();
  }

  getGames() {
    this.games = [];
    this.rest.getGameList().subscribe((data: {}) => {
      console.log(data);
      this.games = data;
    });
  }
//-----------------------------------------
createGame() {
  this.rest.createGame(this.gameData.name, this.gameData).subscribe((result) => {
   // this.router.navigate(['']);
    console.log("si pasa por el post");
  }, (err) => {
    console.log(err);
  });
}
//-----------------------------------------

getRoundsByID() {
  this.rest.getRoundListByID(this.playerData,this.playerData.gameId).subscribe((data: {}) => {
    console.log(data);
    this.game = data;
  });
}


  newGame() {
 
      console.log(this.gameData);
   
  }
  
  public isCollapsed = true;
  public isCollapsed2 = true;
  refresh(): void { window.location.reload(); }


//------------------------------------------------









}
