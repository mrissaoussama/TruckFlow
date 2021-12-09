import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/model/event';
import { EventService } from 'src/app/event.service';
import { Byte } from '@angular/compiler/src/util';
import { DomSanitizer } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { SignalRService } from '../signal-r.service';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css'],
})
export class EventComponent implements OnInit {
  events: Event[] = [];
  constructor(public signalRService: SignalRService,private eventService: EventService,private http: HttpClient ) { }
  ngOnInit(): void {
  //  setInterval(() => { this.getevents() }, 8000);

//   var connection = new signalR.HubConnectionBuilder().withUrl("/ws").build();
//   connection.start().then(function () {
//     console.log("connection started");

//   }).catch(function (err) {
//     return console.error(err.toString());
//   });
// connection.on("getlastevents", function (user, message) {
//   console.log(message)
// });
var socket = new WebSocket("https://localhost:44398/getlastevents");
socket.onclose = function (event) {
 // updateState();
 console.log(event);
};
//socket.onerror = updateState;
socket.onmessage = function (event) {
  console.log(event);

};


  }
  private startHttpRequest = () => {
    this.http.get('https://localhost:44398/getlastevents')
      .subscribe((res: any) => {
        console.log(res);
      })
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

