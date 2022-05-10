import { Component, OnInit } from '@angular/core';
import { Tweet } from '../model/tweet';
import { TweetsService } from '../services/tweets.service';

@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.css']
})
export class TweetsComponent implements OnInit {

  tweets: Tweet[];
  constructor(tService: TweetsService) {
    this.tweets = tService.tweets;
   }

  ngOnInit(): void {
  }

  containDemocrat(input: string) : boolean{
    console.log(input.search("Democrats"));
    if(input.search("Democrats") != -1){
      return true;
    }
    return false;
  }

  filterTweets(tweet: Tweet): boolean {
    if(tweet.likes > 20000) {
      return true;
    }
    return false;
  }

}
