import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { Dictator } from 'src/app/models/dictator';
import { DictatorHandlerService } from 'src/app/services/dictator-handler.service';

@Component({
  selector: 'app-dictator-profile',
  templateUrl: './dictator-profile.component.html',
  styleUrls: ['./dictator-profile.component.scss']
})
export class DictatorProfileComponent implements OnInit {

  editMode: boolean = false;
  dictator$: BehaviorSubject<Dictator> = new BehaviorSubject<Dictator>({} as Dictator);
  constructor(private route: ActivatedRoute, private dicHandler: DictatorHandlerService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      // If web page start on this page it cant fetch the data from dictator handler 
      // Then redirect to home page
      if(!this.dicHandler.getDictator(Number(params.get('id')))) {
        this.router.navigate(['']);
      }
      this.dictator$.next(this.dicHandler.getDictator(Number(params.get('id'))));
    })
  }

  onEditClick() {
    this.editMode = true;
  }

  onSaveClick() {
    let test: Dictator = this.dictator$.getValue();
    console.log("Dictator: ", test);
    this.dicHandler.updateDictators(test);
    this.editMode = false;

  }

  onDeleteClick() {

    let test: Dictator = this.dictator$.getValue();
    this.dicHandler.deleteDictator(test);
    this.editMode = false;
    this.router.navigate(['']);
  }

}
