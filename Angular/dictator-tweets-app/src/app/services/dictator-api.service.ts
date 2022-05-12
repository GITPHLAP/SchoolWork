import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Dictator } from '../models/dictator';

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
}
