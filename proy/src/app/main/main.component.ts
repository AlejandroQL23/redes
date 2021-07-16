import { Component, OnInit, Input } from '@angular/core';
import { RestService } from '../rest.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalDismissReasons, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators, FormArray, FormBuilder } from '@angular/forms';
import { first } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { PathLocationStrategy } from '@angular/common';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  title = 'appBootstrap';
  totalArray: number;
  games: any = [];
  telArray: any = [];
  game: any;
  player: FormGroup;
  loadingPlayer = false;
  submittedPlayer = false;
  returnUrlSupporter: string;
//----------------------------
  @Input() playerData = { gameId: '', owner: '', password: '' };
  @Input() enterPlayerData = { gameId: '', temporalp: '', password: ''};//////
  @Input() go = { gameId: '', name: '', password: '' };
  @Input() startGame = { gameId: '', name: '', password: '' };
  @Input() groupPro = { gameId: '', name: '', password: '' };
  @Input() forPartici = { player1: '', player2: '' };
  @Input() gameData = { owner: '', password: '', name: '' };    
  @Input() playerIsPsycho:any = {id: '', psycho:''};
  @Input() ID = this.rest.ID;

  closeResult: string;
  IsHidden = false;
  IsHidden2 = true;



  onSelect() {
    this.IsHidden = !this.IsHidden;
    this.IsHidden2 = !this.IsHidden2;

  }

  constructor(private formBuilder: FormBuilder,public rest: RestService,
    private route: ActivatedRoute,private router: Router,private modalService: NgbModal) {

    this.player = this.formBuilder.group({
      gameid: ['', [Validators.required]],
      name:  ['', [Validators.required]]
  })
     }
//---------------------------------------------------------------
  ngOnInit() {
    this.getGames();
    this.getRoundsByID();
  }

//---------------------------------------------------------------
  get telefonos() {
    return this.registerForm.get('telefonos') as FormArray;
  }

  registerForm = this.formBuilder.group({
    telefonos: this.formBuilder.array([])
  });

//---------------------------------------------------------------

  agregarTelefono() {
    const telefonoFormGroup = this.formBuilder.group({
      telefono: ''
    });
    this.telefonos.push(telefonoFormGroup);

  }
//---------------------------------------------------------------
  removerTelefono(indice: number) {
    this.telefonos.removeAt(indice);
  }
//---------------------------------------------------------------
  submit() {

    if (!this.registerForm.valid) {
      alert('Alguna regla de validación no se está cumpliendo');
      return;
    }

    this.llena();
  }
//---------------------------------------------------------------
  llena() {
    for (var _i = 0; _i < (this.registerForm.get('telefonos').value).length; _i++) {
      this.telArray[_i] = ((this.registerForm.get(['telefonos', _i])).get(['telefono'])).value
    }
  }


  // enterServer() {
  //  if (!this.useDefaultServer){
  //    this.rest.setCustomServerAddress(this.form.get('serverAddres').value);
  //  }
  //  this.rest.setUseDefaultServer(this.useDefaultServer);
  // }

//---------------------------------------------------------------
  open(content, id) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
    this.getGame(id, this.playerData);
    this.getGame(id, this.enterPlayerData);
    this.getGame2(id, this.startGame);
    this.getGame(id, this.groupPro);
    this.getGame(id, this.go);
  }


  filtrando : any = [] ;
  cargaInfoPsycho(){
    console.log(this.ID + " sancho pamza");
    this.getPlayerById( this.playerIsPsycho); ///this.ID no trae el ID

    // this.filtrando = this.getPlayerById( this.playerIsPsycho.psycho);
    //  if ( this.filtrando == false ){
    //      console.log("No es psycho no habilito mascarilla")
    //  }else{
    //    console.log("Soy psycho  habilito mascarilla")
    //  }
  }
//---------------------------------------------------------------

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
//---------------------------------------------------------------
  getGames() {
    this.games = [];
    this.rest.getGameList().subscribe((data: {}) => {
      console.log(data);
      this.games = data;
    });
  }
//---------------------------------------------------------------
  getGame(id, game) {
    this.rest.getGame(id).subscribe((data: { gameId, name, owner, password, players, psychos, status, rounds: { id, leader, group } }) => {
      game.gameId = data.gameId;
      game.name = data.name;
      game.owner = data.owner;
      game.password = data.password;
    });
  }
//---------------------------------------------------------------
  getGame2(id, game) {
    this.rest.getGame(id).subscribe((data: { gameId, name, owner, password, players, psychos, status, rounds: { id, leader, group } }) => {
      game.gameId = data.gameId;
      game.name = data.owner;
      game.password = data.password;
    });
  }
//---------------------------------------------------------------
  createGame(name, pass) {
    this.rest.createGame(this.gameData.owner, this.gameData).subscribe((result) => {
      console.log("si pasa por el post");
    }, (err) => {
      console.log(err);
    });
    this.loading(name, pass);
  }
//---------------------------------------------------------------
  getRoundsByID() {
    this.rest.getRoundListByID(this.playerData, this.playerData.gameId).subscribe((data: {}) => {
      console.log(data);
      this.game = data;
    });
  }
  //---------------------------------------------------------------
    
  getPlayerById(player) {
    this.rest.isPlayerPsycho().subscribe((data: {id, psycho}) => {
      player.id = data.id;
      player.psycho = data.psycho;
    });
  }

//---------------------------------------------------------------
  loading(name, pass) {
    let timerInterval
    Swal.fire({
      title: 'Credenciales!',
      html: 'Nombre del servidor: ' + name + '<br> Contraseña: ' + pass,
      timer: 3000,
      timerProgressBar: true,
      didOpen: () => {
        Swal.showLoading()
        timerInterval = setInterval(() => {
          const content = Swal.getHtmlContainer()
          if (content) {
            const b = content.querySelector('b')
            // if (b) {
            //   b.textContent = Swal.getTimerLeft()
            // }
          }
        }, 100)
      },
      willClose: () => {
        clearInterval(timerInterval)
        this.refresh();
      }
    }).then((result) => {
      /* Read more about handling dismissals below */
      if (result.dismiss === Swal.DismissReason.timer) {
        console.log('I was closed by the timer')
      }
    })
  }
//---------------------------------------------------------------
  refresh(): void { window.location.reload(); }

  public isCollapsed = true;
  public isCollapsed2 = true;
//---------------------------------------------------------------
  enterPlayer() {
    this.rest.enterPlayer(this.enterPlayerData).subscribe((result) => {
      console.log("si pasa por el enterPlayer");
    }, (err) => {
      console.log(err);
    });
  }
//---------------------------------------------------------------
  gameStart() {
    this.rest.gameStart(this.startGame,this.startGame.gameId).subscribe((result) => {
      // this.router.navigate(['']);
      console.log("si pasa por el gameStart");
    }, (err) => {
      console.log(err);
    });
  }
//---------------------------------------------------------------
  setGroup() {
    this.rest.setGroup(this.groupPro, this.telArray).subscribe((result) => {
      // this.router.navigate(['']);
      console.log("si pasa por el setGroup");
    }, (err) => {
      console.log(err);
    });
    this.totalArray = this.telArray.length -1;
    console.log(this.totalArray);
    this.telArray = [];
  }

//---------------------------------------------------------------
  startRound() {
    if(this.totalArray == 0){
      this.rest.startRound(false, this.go).subscribe((result) => {
        // this.router.navigate(['']);
        console.log("si pasa por el startRound");
      }, (err) => {
        console.log(err);
      });
    }else{
      this.totalArray = this.totalArray - 1;
    }
  }
//---------------------------------------------------------------
  startRound2() {
    if(this.totalArray == 0){
      this.rest.startRound(true, this.go).subscribe((result) => {
        // this.router.navigate(['']);
        console.log("si pasa por el startRound");
      }, (err) => {
        console.log(err);
      });
    }else{
        this.totalArray = this.totalArray - 1;
    }
  }
//---------------------------------------------------------------

onSubmitPlayer() {  

  if (!this.player.valid) {
    return;
  }

  setTimeout (() => {

  this.submittedPlayer = true;

  if (this.player.invalid) {
      return;
  }

  this.loadingPlayer = true;
  
  this.rest.login(this.player.value)
      .pipe(first())
      .subscribe(
          data => {
            console.log(this.player.value)
            
          },
          error => {
              this.loadingPlayer = false;
          });

        }, 3000);

}

clear() {
  this.player.reset();
}

}