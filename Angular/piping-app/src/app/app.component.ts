import { Component } from '@angular/core';
import {Md5} from 'ts-md5/dist/md5';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'piping-app';

text = "";
fromMorse = false;
  
}
