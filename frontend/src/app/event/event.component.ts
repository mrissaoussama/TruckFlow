import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/model/event';
import { EventService } from 'src/app/event.service';
import { Byte } from '@angular/compiler/src/util';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css'] 
})
export class EventComponent implements OnInit {
   events: Event[];
  constructor( private eventService : EventService) { }

  ngOnInit(): void {
    this.eventService.getevent().subscribe((events: Event[]) => {
      this.events = events;

    })
  }
  Sendevent(mat: string , dateevent :Date , heureevent :string, autorise : boolean , flux : string , photo : Byte[] ) {
    this.eventService.Sendevent( mat , dateevent  , heureevent , autorise , flux , photo    ).subscribe((event: Event) => {
      console.log(event); 
          });
}
}
