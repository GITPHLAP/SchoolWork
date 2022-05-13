import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DictatorProfileComponent } from './dictator-profile.component';

describe('DictatorProfileComponent', () => {
  let component: DictatorProfileComponent;
  let fixture: ComponentFixture<DictatorProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DictatorProfileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DictatorProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
