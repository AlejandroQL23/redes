import { Component, OnInit, Input} from '@angular/core';
import { RestService } from '../rest.service'; 
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDismissReasons, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

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

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }


  getGames() {
    this.games = [];
    this.rest.getGameList().subscribe((data: {}) => {
      console.log(data);
      this.games = data;
    });
  }

createGame() {
  /*this.rest.createGame(this.gameData.name, this.gameData).subscribe((result) => {
    console.log("si pasa por el post");
  }, (err) => {
    console.log(err);
  });*/
  this.router.navigate(['/Game']);
}

getRoundsByID() {
  this.rest.getRoundListByID(this.playerData,this.playerData.gameId).subscribe((data: {}) => {
    console.log(data);
    
    this.game = data;
  });
}  

refresh(): void { window.location.reload(); }


}