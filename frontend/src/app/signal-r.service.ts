import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: Event[]=[];
  private hubConnection!: signalR.HubConnection;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:44398')
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  public addTransferChartDataListener = () => {
    this.hubConnection.on('eventdata', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}
