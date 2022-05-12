import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Dictator } from 'src/app/models/dictator';
import { Tweet } from 'src/app/models/tweet';
import { DictatorApiService } from 'src/app/services/dictator-api.service';
import { DictatorHandlerService } from 'src/app/services/dictator-handler.service';
import { TweetsHandlerService } from 'src/app/services/tweets-handler.service';

@Component({
  selector: 'app-alldictators',
  templateUrl: './alldictators.component.html',
  styleUrls: ['./alldictators.component.css']
})
export class AlldictatorsComponent implements OnInit {
  dictators: Dictator[] = []; 

  constructor(public dictatorHandler: DictatorHandlerService, private tweetsHandler: TweetsHandlerService) { 
  }
  ngOnInit(): void {
    this.dictatorHandler.loadDictators();
    this.dictatorHandler.dictators$.subscribe((data: Dictator[]) => {
      next: 
        this.dictators = data;
      // complete: 
      //   this.dictators.forEach(d => {
      //     d.tweets$.next({} as Tweet);
      //   })
    });
  }

  // subToDictatorTweets(dictators: Dictator[]) {
  //   dictators.forEach(d => {
  //     d.tweets$.subscribe();
  //   })
  // }

  onTestClick() {
    // let test: Dictator = {
    //   id: 0,
    //   firstName: 'testign',
    //   lastName: 'B',
    //   birthYear: 1,
    //   deadYear: 2,
    //   shortDescription: 'beskrivelse',
    //   mappingText: ''
    // };
    // this.dictatorHandler.insertDictators(test);

    let testDic = this.dictators.find(d => d.mappningText === "Twitter Web Client");
    console.log(testDic);
    console.log(this.dictators);
  }

}
