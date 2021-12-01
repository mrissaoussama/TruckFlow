import { Component } from '@angular/core';
import { EventService } from './event.service';
import { WebsocketService } from './websocket.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [WebsocketService, EventService]
})
export class AppComponent {
    title = 'frontend';
  constructor(private eventService: EventService){
    eventService.messages.subscribe((msg: any) => {
      console.log("Response from websocket: " + msg);
    });
  }
  private message = {   

    mat: "Matricule",
    heureevent: "Heure evenement",
    autorise: "Autorisation ",
    photo: "photo",
    sync:" sync",
  };
  sendMsg() {
    console.log("new message from client to websocket: ", this.message);
    this.eventService.messages.next(this.message);
    this.message.mat = "";
  }
}
