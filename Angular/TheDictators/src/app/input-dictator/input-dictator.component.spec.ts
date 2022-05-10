import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputDictatorComponent } from './input-dictator.component';

describe('InputDictatorComponent', () => {
  let component: InputDictatorComponent;
  let fixture: ComponentFixture<InputDictatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InputDictatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InputDictatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
