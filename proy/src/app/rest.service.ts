import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { stringify } from '@angular/compiler/src/util';
import { Player } from './models/Player';


const endpoint_web = 'localhost:44395';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root' 
})
export class RestService {
  private PlayerSubject: BehaviorSubject<any>;
  public Player: Observable<any>;
  public ID;

  constructor(private http: HttpClient) { 

    this.PlayerSubject = new BehaviorSubject<Player>(
      JSON.parse(localStorage.getItem("Player"))
    );
    this.Player = this.PlayerSubject.asObservable();
    

  }

  //------------Server-----------------

  getCustomServerAddress(): string {
    return localStorage.getItem('customServerAddress');
  }

  getUseDefaultServer() {
    const useDefaultServer = localStorage.getItem('useDefaultServer');
    return useDefaultServer == null || useDefaultServer == 'true';
  }


  private getEndpoint(): string {
    let endpoint: string;
    if (this.getUseDefaultServer()) {
      endpoint = endpoint_web;
    } else {
      endpoint = this.getCustomServerAddress();
    }
    return `https://${endpoint}/`;
  }


  setCustomServerAddress(address: string): void {
    address = address
    localStorage.setItem('customServerAddress', address);
    localStorage.setItem('useDefaultServer', 'true');
  }

  //------------SET UP-----------------
  //GET
  getGameList(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',

      })
    };
    return this.http.get(this.getEndpoint() + 'Game/', httpOptions);
  }

  //POST
  createGame(ownerName: string, game: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'name': ownerName
      })
    };

    const body = {
      'name': game.name,
      'password' : game.password

    };

    return this.http.post<any>(this.getEndpoint() + 'Game/create', JSON.stringify(body), httpOptions);
  }

  //------------ROUND-----------------

  //GET
  getRoundListByID(playerData: any, gameId: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'name': playerData.name,
        'password': playerData.password
      })

    };

    return this.http.get(this.getEndpoint() + 'Game/' + gameId, httpOptions);
  }


  //PUT
  enterPlayer(enterPlayerData: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'temporalp': enterPlayerData.temporalp,
        'password': enterPlayerData.password,

      })
    };
    return this.http.put(this.getEndpoint() + 'Game/'+enterPlayerData.gameId+'/join', JSON.stringify(enterPlayerData), httpOptions);
    // put -> post  // 'game/' + enterPlayerData.gameId + '/join'
    //, JSON.stringify(enterPlayerData)
  }

  //HEAD
  gameStart(startGame:any,gameId: any): Observable<any> { 
    const httpOptions = {
      headers: new HttpHeaders({ 
        'Content-Type': 'application/json',
        'name': startGame.name, 
        'password' :  startGame.password

      })
    }; 
    return this.http.head(this.getEndpoint() + 'Game/'+gameId+'/start', httpOptions);
    // 'game/' + gameData.gameId + '/start'
    //JSON.stringify(startGame)
  }

  //POST1
  setGroup(groupPro: any, registerForm: any): Observable<any> {
    console.log(registerForm);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'name': groupPro.name,
        'password': groupPro.password
      })
    };
    const body = {
      'group': registerForm

    };
    return this.http.post<any>(this.getEndpoint() + 'game/' + groupPro.gameId + '/group', JSON.stringify(body), httpOptions);
  }

  //POST2
  startRound(Ispsycho: boolean, go: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'name': go.name,
        'password': go.password
      })
    };
    const body = {
      'psycho': Ispsycho
    };
    return this.http.post<any>(this.getEndpoint() + 'game/' + go.gameId + '/go', JSON.stringify(body), httpOptions);
  }


  getGame(gameId): Observable<any> {
    return this.http.get(this.getEndpoint() + 'Game/' + gameId, httpOptions).pipe(
      map(this.extractData),
    );
  }

  private extractData(res: Response) {
    let body = res;
    return body || {};
  }

//-------------------INICIOS DE SESION JUGADORES----------------------

login(player) {
  console.log(JSON.stringify(player)+ " DESDE REST");
  return this.http
    .post<any>(
      `https://localhost:44395/Player/PostAuthenticate/`,
      JSON.stringify(player),
      httpOptions
    )
    
    .pipe(
      map((userData) => {
        sessionStorage.setItem("gameid", player.gameid);
        let tokenStr = "Bearer " + userData.token;
        sessionStorage.setItem("token", tokenStr);
        sessionStorage.setItem("Id", userData.id);
        this.PlayerSubject.next(sessionStorage.getItem(player.gameid));
        console.log(userData.name + " ANDO BISCANDO NOMBRE");
        this.ID = userData.id;
        console.log(this.ID);
        return userData;
      })
    );
}


logOut() {
  sessionStorage.removeItem("email");
  this.Player = null;
}



isPlayerPsycho(): Observable<any> {
  return this.http
    .get(this.getEndpoint() + "player/" + this.ID)
    .pipe(
      map(this.extractData),
      catchError(this.handleError<any>("isPlayerPsycho"))
    );
}

private handleError<T>(operation = "operation", result?: T) {
  return (error: any): Observable<T> => {
    console.error(error);
    console.log(`${operation} failed: ${error.message}`);
    return of(result as T);
  };
}

}