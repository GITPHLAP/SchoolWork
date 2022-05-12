import { Component, OnInit, Input } from '@angular/core';
import { Tweet } from '../models/tweet';

@Component({
  selector: 'app-test-tweets',
  templateUrl: './test-tweets.component.html',
  styleUrls: ['./test-tweets.component.css']
})
export class TestTweetsComponent implements OnInit {

  constructor() { 

  }

  ngOnInit(): void {
  }

}
