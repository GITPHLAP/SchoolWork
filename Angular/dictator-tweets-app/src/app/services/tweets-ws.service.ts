import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { Tweet } from '../models/tweet';

@Injectable({
  providedIn: 'root'
})
export class TweetsWsService {
  subject$: WebSocketSubject<Tweet> = webSocket("ws://localhost:5300/send");
  tweet$: BehaviorSubject<Tweet> = new BehaviorSubject<Tweet>({} as Tweet);
  constructor() {
    // Websocket need a message at the beginning else it will disconnect
    this.subject$.next({} as Tweet);
    
    this.subject$.asObservable().subscribe((msg: Tweet) => {
      next: {
          this.tweet$.next(msg);
          console.log(msg);
      } // Called whenever there is a message from the server.
      error: {
        (err: any) => console.log(err);
        //this.countriesReady = false;
      }
      complete: () => console.log('complete') // Called when connection is closed (for whatever reason).
    });
   }
}
