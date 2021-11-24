import { Time } from "@angular/common";
import { Byte } from "@angular/compiler/src/util";

export class Event {
  constructor( public  idevent : string ,
    public  mat : string ,
    public dateevent : Date ,
  public  heureevent : any,
  public  flux : string ,
  public  autorise : boolean,
  public photo :any){

  }
  }
  //string mat, DateTime dateevent, DateTime heureevent, string flux, bool autorise, byte[] photo, bool sync
