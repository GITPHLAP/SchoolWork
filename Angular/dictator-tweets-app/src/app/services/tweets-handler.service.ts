import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Dictator } from '../models/dictator';
import { Tweet } from '../models/tweet';
import { DictatorHandlerService } from './dictator-handler.service';
import { TweetsWsService } from './tweets-ws.service';

@Injectable({
  providedIn: 'root'
})
export class TweetsHandlerService {
  // TODO: tweets is doubled in AllTweet

  constructor(private tweetWs: TweetsWsService,  private dicHandler: DictatorHandlerService) { 
    this.tweetWs.tweet$.subscribe((msg: Tweet) => {
      let dictator: Dictator | undefined;
      next:
        this.dicHandler.dictators$.forEach(value => {
          dictator = value.find(d => d.mappningText === msg.Client);
          console.log("The dictator: " + dictator + "    tweet: "+ msg.Tweet);

          
          if(dictator != undefined){
            if(dictator.tweet$ == undefined) {
              // Initilaize tweet subject 
              dictator.tweet$ = new BehaviorSubject<Tweet>(msg);

              dictator.tweets$ = new BehaviorSubject<Tweet[]>(Array<Tweet>(msg));
            } else {
              dictator.tweet$.next(msg);

              // Subscibe to tweet. When new tweet then it will automatically add to tweets list
              let newTweet;
              dictator.tweet$.subscribe((data: Tweet) => {
                next:
                  newTweet = data;
                
                complete:
                  if(newTweet != {} as Tweet){
                    dictator?.tweets$.getValue().push(newTweet);
                  }
              });

              console.log("Dictators last tweet: ");
              console.log(dictator.tweet$?.getValue());
            }
          }
        })
    });
  }
}
