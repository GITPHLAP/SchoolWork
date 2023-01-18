import { Component, Input } from '@angular/core';
import { Tweet } from '../models/tweet';

@Component({
  selector: 'app-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.scss']
})
export class TweetComponent {
  private _tweet : Tweet | null = {} as Tweet;
 
  @Input()
  set tweet(tweet: Tweet | null) {
    if (tweet != null){
      this._tweet = tweet;
    }
    else{
      this._tweet = {} as Tweet;
    }
  }
  get tweet() {
    return this._tweet;
  }

}
