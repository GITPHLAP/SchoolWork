import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Dictator } from '../models/dictator';
import { JsonDictator } from '../models/json-dictator';

@Injectable({
  providedIn: 'root'
})
export class DictatorApiService {

  constructor(private http: HttpClient) { }

  getDictators(): Observable<Dictator[]> {
    return this.http.get<Dictator[]>('https://localhost:5001/api/Dictator');
  }

  postDictator(newdictator: Dictator):Observable<Dictator> {
    return this.http.post<Dictator>('https://localhost:5001/api/Dictator/insert', newdictator);
  }

  putDictator(dic: Dictator):Observable<void> {
    // TODO: Find a good solution for this... The problem is that it cant stringify to json because og the behaviour subject
    let dictator: JsonDictator = new JsonDictator(dic.id, dic.firstName, dic. lastName, dic.birthYear, dic.deadYear, dic.shortDescription, dic.mappningText);
    return this.http.put<void>('https://localhost:5001/api/Dictator/update', dictator);
  }

  deleteDictator(dic: Dictator):Observable<void> {
    // TODO: Find a good solution for this... The problem is that it cant stringify to json because og the behaviour subject
    let dictator: JsonDictator = new JsonDictator(dic.id, dic.firstName, dic. lastName, dic.birthYear, dic.deadYear, dic.shortDescription, dic.mappningText);
    return this.http.delete<void>('https://localhost:5001/api/Dictator', {body: dictator});
  }
}
