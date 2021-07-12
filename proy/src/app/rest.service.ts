import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { stringify } from '@angular/compiler/src/util';



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

  constructor(private http: HttpClient) { }

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
    return this.http.get(this.getEndpoint() + 'Game/GetGames', httpOptions);
  }

  //POST
  createGame(ownerName: string, game: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'name': ownerName
      })
    };

    return this.http.post<any>(this.getEndpoint() + 'Game/PostGame', JSON.stringify(game), httpOptions);
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
        'name': enterPlayerData.name,
        'password': enterPlayerData.password

      })
    };
    return this.http.post(this.getEndpoint() + 'Player/PostPlayer', JSON.stringify(enterPlayerData), httpOptions);
    // put -> post  // 'game/' + enterPlayerData.gameId + '/join'
  }

  //HEAD
  gameStart(gameId: any): Observable<any> { 
    const httpOptions = {
      headers: new HttpHeaders({ 
        'Content-Type': 'application/json',
        'gameId' : stringify(gameId)

      })
    };

    return this.http.head(this.getEndpoint() + 'Game/start/', httpOptions);
    // 'game/' + gameData.gameId + '/start'
  }

  //POST1
  setGroup(groupPro: any, forPartici: any, registerForm: any): Observable<any> {
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



}