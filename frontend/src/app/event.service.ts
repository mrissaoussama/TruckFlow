import { Injectable } from '@angular/core';
import { WebRequestService } from './web-request.service';
import {Event} from './model/event'
import { Byte } from '@angular/compiler/src/util';
@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private webReqService: WebRequestService) { }
  getevent() {
    return this.webReqService.get('getlastevents');
  }
  Sendevent(mat: string , dateevent :Date , heureevent :string, autorise : boolean , flux : string , photo :  Byte[])  {
    // We want to send a web request to create a list
    return this.webReqService.post('event', { mat , dateevent  , heureevent , autorise , flux , photo });
  }
}
