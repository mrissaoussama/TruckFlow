import { Injectable } from '@angular/core';
import * as Rx from "rxjs/Rx";
import { webSocket } from 'rxjs/webSocket';
@Injectable({
  providedIn: 'root'
})
export class WebsocketService {

  constructor() { }
  
 // private socket$  = new webSocket(url);
 // public messages$ = this.socket$.asObservable();

  public sendMessage(msg: any) {
   // this.socket$.next(msg);
  }
}
