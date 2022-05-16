import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewDictatorComponent } from './new-dictator.component';

describe('NewDictatorComponent', () => {
  let component: NewDictatorComponent;
  let fixture: ComponentFixture<NewDictatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewDictatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewDictatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
