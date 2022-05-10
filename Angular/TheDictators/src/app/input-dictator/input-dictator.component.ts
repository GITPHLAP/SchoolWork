import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Dictator } from '../models/dictator';
import { DictatorsService } from '../services/dictators.service';

@Component({
  selector: 'app-input-dictator',
  templateUrl: './input-dictator.component.html',
  styleUrls: ['./input-dictator.component.css']
})
export class InputDictatorComponent implements OnInit {

  constructor(private formbuilder: FormBuilder, public service: DictatorsService) { }

  dictatorForm = this.formbuilder.group({
    firstName: '',
    lastName: '',
    birthYear: '',
    deadYear: '',
    shortDescription: ''
  });

  onSubmit() {
    let newDictator: Dictator = this.dictatorForm.value;
    this.service.addDictator(newDictator);
  }

  deleteDictator(dictator: Dictator) {
    this.service.deleteDictator(dictator);
  }

  ngOnInit(): void {

  }

}
