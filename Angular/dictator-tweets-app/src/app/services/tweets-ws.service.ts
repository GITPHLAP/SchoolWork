import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { Tweet } from '../models/tweet';

@Injectable({
  providedIn: 'root'
})
export class TweetsWsService {
  initialized: boolean = false;
  tweets$: BehaviorSubject<Tweet[]> = new BehaviorSubject<Tweet[]>([]);

  constructor() { this.initialize();}

  private initialize() {
    if(!this.initialized){
      const subject: WebSocketSubject<Tweet> = webSocket("ws://localhost:5300/send");
      // Websocket need a message at the beginning else it will disconnect
      subject.next({} as Tweet);
      
      subject.subscribe((msg: Tweet) => {
        next: {
            this.tweets$.getValue().push(msg);
            console.log(msg);
        } // Called whenever there is a message from the server.
        error: {
          (err: any) => console.log(err);
          //this.countriesReady = false;
        }
        complete: () => console.log('complete') // Called when connection is closed (for whatever reason).
      });
      this.initialized = true;
    }

  }

  getTweets(): Observable<Tweet[]>{
    return this.tweets$;
  }
}
