import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Dictator } from '../models/dictator';
import { Tweet } from '../models/tweet';
import { DictatorApiService } from './dictator-api.service';
import { TweetsHandlerService } from './tweets-handler.service';

@Injectable({
  providedIn: 'root'
})
export class DictatorHandlerService {

  dictators$: BehaviorSubject<Dictator[]> = new BehaviorSubject<Dictator[]>([]);

  constructor(private dictatorApi: DictatorApiService) {

  }
  loadDictators(): void {
    let dictators;
    this.dictatorApi.getDictators().subscribe((data: Dictator[]) => {
      next:
        data.forEach(d => {
          // // Initilaize tweet subject 
          // d.tweet$ = new BehaviorSubject<Tweet>({} as Tweet);

          // Initilaize tweets subject 
          d.tweets$ = new BehaviorSubject<Tweet[]>([] as Tweet[]);
          d.tweets$.getValue().splice(0,length)
          // Subscribe to tweet subject to add the new tweet to array
          let newTweet;
          d.tweet$?.subscribe((data: Tweet) => {
            next:
              newTweet = data;
            
            complete:
              if(newTweet != {} as Tweet){
                d.tweets$.getValue().push(newTweet);
                d.tweets$.next(d.tweets$.getValue())
              }
          });
        });
        dictators = data;
      
      complete:
        this.dictators$.next(dictators);

    });
  }

  insertDictators(newDictator: Dictator): void {
    this.dictatorApi.postDictator(newDictator).subscribe((data: Dictator) => {      
      complete:
        // this.dictators$.next(dictator);
        this.loadDictators();
    });
    
  }

  updateDictators(newDictator: Dictator): void {
    this.dictatorApi.putDictator(newDictator).subscribe(() => {      
      complete:
        // this.dictators$.next(dictator);
        this.loadDictators();
    });
    
  }

  getDictator(id: number): Dictator {
    return this.dictators$.getValue().find(d => d.id === id)!;
  }

  deleteDictator(dictator: Dictator): void {
    this.dictatorApi.deleteDictator(dictator).subscribe(() => {      
      complete:
        this.loadDictators();
    });
    
  }
}
