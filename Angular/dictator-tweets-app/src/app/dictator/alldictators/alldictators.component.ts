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
  styleUrls: ['./alldictators.component.scss']
})
export class AlldictatorsComponent implements OnInit {
  dictators: Dictator[] = []; 

  constructor(public dictatorHandler: DictatorHandlerService, public tweetsHandler: TweetsHandlerService) { 
  }
  ngOnInit(): void {
    this.dictatorHandler.loadDictators();
    this.dictatorHandler.dictators$.subscribe((data: Dictator[]) => {
      next: 
        this.dictators = data;
    });
  }
  
  onTestClick() {
    let test: Dictator = {
      id: 36,
      firstName: 'Kurt',
      lastName: 'Jensen',
      birthYear: 1,
      deadYear: 2,
      shortDescription: 'beskrivelse',
      mappningText: '',
      tweet$: {} as BehaviorSubject<Tweet>,
      tweets$: {} as BehaviorSubject<Tweet[]>
    };
    console.log("Dictator: ", test);
    this.dictatorHandler.updateDictators(test);

    // let testDic = this.dictators.find(d => d.mappningText === "Twitter Web Client");
    // console.log(testDic);
    // console.log(this.dictators);
  }

}
