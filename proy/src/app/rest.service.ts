import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';


const endpoint_web = '173.230.130.7';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(private http: HttpClient) { }

  getCustomServerAddress() : string{
    return localStorage.getItem('customServerAddress');
    }
    
    getUseDefaultServer() {
    const useDefaultServer = localStorage.getItem('useDefaultServer');
    return useDefaultServer== null || useDefaultServer== 'true';
    }
    
    
    private getEndpoint() : string{
    let endpoint : string;
    if(this.getUseDefaultServer()){
    endpoint= endpoint_web;
    }else{
      endpoint = this.getCustomServerAddress();
    }
    return `http://${endpoint}/`;
    }
    //------------SET UP-----------------
    //GET
    getGameList(): Observable<any>{
      const httpOptions = {
        headers : new HttpHeaders({
          'Content-Type': 'application/json',

        })
      };
    return this.http.get(this.getEndpoint() + 'game/', httpOptions);
    }
    
    //POST
    createGame(ownerName:string,game:any): Observable<any>{
      const httpOptions = {
        headers : new HttpHeaders({
          'Content-Type': 'application/json',
          'name':ownerName,
        })
    
        
      };
      //return this.http.post<any>(this.getEndpoint() + 'game/create', JSON.stringify(game), httpOptions);
      return this.http.post<any>(this.getEndpoint() + 'game/create', JSON.stringify(game), httpOptions);
    }
    
    //------------ROUND-----------------
    
    //GET
    getRoundListByID(playerData:any,gameId:string): Observable<any>{
      const httpOptions = {
        headers : new HttpHeaders({
          'Content-Type': 'application/json',
           'name' : playerData.name,
           'password' : playerData.password
        })
      };
    return this.http.get(this.getEndpoint() + 'game/'+gameId, httpOptions);
    }
    
    //PUT
    enterPlayer(gameId:string,namePlayer:string,passwordRound:string): Observable<any>{
      const httpOptions = {
        headers : new HttpHeaders({
          'Content-Type': 'application/json',
          'name':namePlayer,
          'password': passwordRound
          
        })
      };
    
      return this.http.put<any>(this.getEndpoint() + 'game/'+gameId+'/join', httpOptions);
    }
    
    //HEAD
    gameStart(gameId:string,gameOwner:string,gamePassword:string): Observable<any>{
    const httpOptions = {
      headers : new HttpHeaders({
        'Content-Type': 'application/json',
        'name':gameOwner,
        'password': gamePassword
        
      })
    };
    
    return this.http.head(this.getEndpoint() + 'game/'+gameId+'/start', httpOptions);
    }
    
    //POST1
    setGroup(name:string,password:string,gameId:string,groupParticipants:string[]): Observable<any>{
      const httpOptions = {
        headers : new HttpHeaders({
          'Content-Type': 'application/json',
          'name':name,
          'password': password
        })
      };
    
      return this.http.post<any>(this.getEndpoint() + 'game/'+gameId+'/group',JSON.stringify(groupParticipants), httpOptions);
    }
    
    //POST2
    startRound(pshycho:boolean,nameGame:string,roundPassword:string,gameId:string): Observable<any>{
      const httpOptions = {
        headers : new HttpHeaders({
          'Content-Type': 'application/json',
          'name':nameGame,
          'password' : roundPassword
        })
      };
    
      return this.http.post<any>(this.getEndpoint() + 'game/'+gameId+'/go',JSON.stringify(pshycho), httpOptions);
    }
    


}
