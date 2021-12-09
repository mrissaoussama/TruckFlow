import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/model/event';
import { EventService } from 'src/app/event.service';
import { Byte } from '@angular/compiler/src/util';
import { DomSanitizer } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
import { getGlobalThis } from '@microsoft/signalr/dist/esm/Utils';
@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css'],
})
export class EventComponent implements OnInit {
  events: Event[] = [];
  constructor(private eventService: EventService,private http: HttpClient ) { }
  ngOnInit(): void {
var socket = new WebSocket("ws://localhost:65060");
socket.onclose = function (event) {
 // updateState();
 console.log("close");

 console.log(event);
};
socket.onopen=function (event) {
  // updateState();
  console.log("close");

  console.log(event);
 };
//socket.onerror = updateState;
var self=this;
socket.onmessage = function (event) {
  console.log("onmessage");
  console.log(event.data);
self.getdata(event.data)
};

  }
 getdata(data:any)
  {var dataarray=JSON.parse(data)
    console.log(dataarray)
    for (var i = 0; i < dataarray.length; i++) {
       if (this.events.findIndex(x => x.idevent == dataarray[i].idevent) == -1) {
        this.events.unshift(dataarray[i]);
       this.checkEventsCount(20);
      }
    }}

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