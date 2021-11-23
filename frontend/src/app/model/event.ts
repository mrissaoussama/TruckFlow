import { Byte } from "@angular/compiler/src/util";

export class Event {
    id : string ;
    mat : string ;
    dateevent : Date ;
    heureevent : Date;
    flux : string ;
    autorise : boolean;
    photo : Byte[];

  }
  //string mat, DateTime dateevent, DateTime heureevent, string flux, bool autorise, byte[] photo, bool sync