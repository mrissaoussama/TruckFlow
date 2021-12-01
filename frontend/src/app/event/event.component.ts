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
  constructor(private eventService: EventService) { }
  ngOnInit(): void {
    setInterval(() => { this.getevents() }, 8000);
  }
  async getevents() {
    console.log("sending")
    this.eventService.getevent().subscribe(async (data: any) => {
      for (var i = 0; i < data.length; i++) {
        if (this.events.findIndex(x => x.idevent == data[i].idevent) == -1) {
          this.events.unshift(data[i]);
          this.checkEventsCount(20);
        }
      }
    });
  }
checkEventsCount(n:number){
  if (this.events.length>n)
  {var elemsToDelete=this.events.length-n
    this.events.splice(this.events.length - elemsToDelete,
      elemsToDelete);
  }
}
}

