import { Component } from '@angular/core';
import { Dictator } from './models/dictator';
import { DictatorHandlerService } from './services/dictator-handler.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'dictator-tweets-app';

  dictators: Dictator[] = []; 
  constructor(private dictatorHandler: DictatorHandlerService) {
    this.dictatorHandler.dictators$.subscribe((data: Dictator[]) => {
      next: 
        this.dictators = data;
    });
  }

}
