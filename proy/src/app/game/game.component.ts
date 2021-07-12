import { Component, OnInit, Input} from '@angular/core';
import { RestService } from '../rest.service'; 
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  games:any = [];
  
  game : any;

  @Input() playerData = {gameId:'', password:'', name:''};
  @Input() enterPlayerData = {gameId:'', name:'',password:''};

  @Input() go = {gameId:'', name:'',password:''};

  @Input() startGame = {gameId:'', name:'',password:''};
  @Input() groupPro = {gameId:'', name:'',password:''};
  @Input() forPartici = {player1:'',player2:''};
  closeResult: string;
  constructor(public rest:RestService, private route: ActivatedRoute, private router: Router,  private modalService: NgbModal) { }

  ngOnInit() {
    this.getRoundsByID();
  }
  public isCollapsed = true;
  public isCollapsed2 = true;

  getRoundsByID() {
    this.rest.getRoundListByID(this.playerData,this.playerData.gameId).subscribe((data: {}) => {
      console.log(data);
      
      this.game = data;
    });
  } 

  enterPlayer() {
    this.rest.enterPlayer(this.enterPlayerData).subscribe((result) => {
      console.log("si pasa por el enterPlayer");
    }, (err) => {
      console.log(err);
    });
  }

  gameStart() {
    this.rest.gameStart(this.startGame.gameId).subscribe((result) => {
     // this.router.navigate(['']);
      console.log("si pasa por el gameStart");
    }, (err) => {
      console.log(err);
    });
  }



  startRound() {
    this.rest.startRound(false, this.go).subscribe((result) => {
     // this.router.navigate(['']);
      console.log("si pasa por el startRound");
    }, (err) => {
      console.log(err);
    });
  }
  
  

}
