import { Injectable } from '@angular/core';
import { Dictator } from '../models/dictator';

@Injectable({
  providedIn: 'root'
})
export class DictatorsService {

  constructor() { }

  private dictators: Array<Dictator> = [];
  public getDictators(): Array<Dictator> {
    return this.dictators;
  }
  public deleteDictator(dictator: Dictator): void {
    this.dictators.splice(this.dictators.indexOf(dictator),1);
  }
  public addDictator(newDictator: Dictator): void {
    this.dictators.push(newDictator);
  }
}
