import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/model/event';
import { EventService } from 'src/app/event.service';
import { Byte } from '@angular/compiler/src/util';
import { DomSanitizer } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css'],
})
export class EventComponent implements OnInit {
   events: Event[] = [];
   subscription = Subscription;
   intervalId: number = 0;
  constructor( private eventService : EventService,private sanitizer:DomSanitizer) { }
 
  ngOnInit(): void {
    this.eventService.getevent().subscribe((data: any) => {
      for(var i=0;i<this.events.length;i++)
      {
        data[i].photo=this.blobtoimage(this.byteaaraytoblob(this.events[i].photo));
      }
      this.events=data
    
  setInterval(()=> { this.getevents() }, 1000);

  // polling ***** Refresh after 10000 second
  /*const source = interval(10000);
  const text = 'Your Text Here';
  this.subscription = source.subscribe(val => this.getevents());*/
  //this.intervalId = setInterval(this.getevents(), 10000);
  });
}

  getevents (){
    this.eventService.getevent().subscribe((data:any)=> {
      for(var i=0 ; i< this.events.length;i++)
      {
        data[i].photo=this.blobtoimage(this.byteaaraytoblob(this.events[i].photo));
      }
      this.events=data
      console.log("page refreshed successfully")
    })
  }
  blobtoimage(blob:any){
  var reader = new FileReader();
reader.readAsDataURL(blob);
reader.onloadend = function() {
  var base64data = reader.result;
  return (base64data);}
}
   byteaaraytoblob(arr:Byte[] )
   {
    const byteArray = new Uint16Array(arr);
    var b= new Blob([byteArray], {type: 'image/png'});
    var unsafeImageUrl = URL.createObjectURL(b);
   return   unsafeImageUrl;
};
  Sendevent(mat: string , dateevent :Date , heureevent :string, autorise : boolean , flux : string , photo : Byte[] ) {
    this.eventService.Sendevent( mat , dateevent  , heureevent , autorise , flux , photo    ).subscribe(data => {
      console.log(data);
          });
}
}

