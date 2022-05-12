import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlldictatorsComponent } from './alldictators.component';

describe('AlldictatorsComponent', () => {
  let component: AlldictatorsComponent;
  let fixture: ComponentFixture<AlldictatorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlldictatorsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AlldictatorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
