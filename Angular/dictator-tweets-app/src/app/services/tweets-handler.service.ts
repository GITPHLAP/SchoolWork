import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Dictator } from '../models/dictator';
import { Tweet } from '../models/tweet';
import { DictatorHandlerService } from './dictator-handler.service';
import { TweetsWsService } from './tweets-ws.service';

@Injectable({
  providedIn: 'root'
})
export class TweetsHandlerService {

  constructor(private tweetWs: TweetsWsService) {}

  getTweetsForDictator(mappingText : string): Observable<Tweet[]>{
    let tweets$ = new BehaviorSubject<Tweet[]>([]);

    this.tweetWs.getTweets().subscribe((tweets: Tweet[]) => {
      // console.log(tweets);
      for(let tweet of tweets) {
        if(tweet.Client == mappingText){

          tweets$.getValue().push(tweet);
        }
      }
    });

    // console.log(tweets$);
    return tweets$;
  }


  // Use NUll to hide in html
  getLatestTweetForDictator(mappingText : string): Observable<Tweet|null>{
    let tweet$ = new BehaviorSubject<Tweet | null>(null );
    try {

      this.getTweetsForDictator(mappingText).subscribe((tweets: Tweet[]) => {
        if(tweets.length > 0) {
          tweet$.next(tweets[tweets.length-1]);
        }
        // console.log(tweets[tweets.length-1]);
      });
    } catch (error) {
      console.log(error);
    }
    return tweet$;

  }

}
