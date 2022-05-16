import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { Dictator } from 'src/app/models/dictator';
import { Tweet } from 'src/app/models/tweet';
import { DictatorHandlerService } from 'src/app/services/dictator-handler.service';

@Component({
  selector: 'app-new-dictator',
  templateUrl: './new-dictator.component.html',
  styleUrls: ['./new-dictator.component.scss']
})
export class NewDictatorComponent implements OnInit {

  dictator: Dictator = {} as Dictator;
  constructor(private dicHandler: DictatorHandlerService) { }

  dictatorForm:FormGroup = {} as FormGroup;
  ngOnInit(): void {
    this.dictatorForm = new FormGroup({
      firstName: new FormControl(),
      lastName: new FormControl(),
      birthYear: new FormControl(),
      deadYear: new FormControl(),
      shortDescription: new FormControl()
    });
  }

  onAddDictator(){
    let newDictator: Dictator = {
      id: 0,
      firstName: this.dictatorForm.get('firstName')?.value,
      lastName: this.dictatorForm.get('lastName')?.value,
      birthYear: this.dictatorForm.get('birthYear')?.value,
      deadYear: this.dictatorForm.get('deadYear')?.value,
      shortDescription: this.dictatorForm.get('shortDescription')?.value,
      mappningText: '',
      tweet$: {} as BehaviorSubject<Tweet>,
      tweets$: {} as BehaviorSubject<Tweet[]>
    };

    // console.log(newDictator);
    this.dicHandler.insertDictators(newDictator);
    this.dictatorForm.reset();
  }

}
