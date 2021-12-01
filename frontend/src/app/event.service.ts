import { Injectable } from '@angular/core';
import { WebRequestService } from './web-request.service';
import {Event} from './model/event'
import { Observable, Subject } from "rxjs"
import { WebsocketService } from "./websocket.service";
import { Byte } from '@angular/compiler/src/util';
import 'rxjs/Rx'; 
import 'rxjs/add/operator/map'
import { HttpClient } from '@angular/common/http';
import { map, filter, switchMap } from 'rxjs/operators';
import { Time } from '@angular/common';

const URL = "ws://localhost:44398";

export interface Message {
 
  mat: string;
  heureevent :Time;
  flux : string ;
  autorise : boolean;
  photo : Byte[];
  sync : boolean;

}
@Injectable({
  providedIn: 'root'
})
 
export class EventService {

  public messages: Subject<Message> | any;
  
   constructor( private webReqService: WebRequestService , wsService: WebsocketService) {
    this.messages = <Subject<Message>>wsService.connect(URL).map(
      (response: MessageEvent): Message => {
        let data = JSON.parse(response.data);
        return {
          mat: data.mat,
          heureevent: data.heureevent,
          flux: data.flux,
          autorise: data.autorise,
          photo: data.photo,
          sync: data.sync
        };
      }
    );
  }
  getevent() {
    return this.webReqService.get('getlastevents');
  }
  Sendevent(mat: string , dateevent :Date , heureevent :string, autorise : boolean , flux : string , photo :  Byte[])  {
    // We want to send a web request to create a list
    return this.webReqService.post('event', { mat , dateevent  , heureevent , autorise , flux , photo });
  }


}
