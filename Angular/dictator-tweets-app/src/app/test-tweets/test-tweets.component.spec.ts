import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTweetsComponent } from './test-tweets.component';

describe('TestTweetsComponent', () => {
  let component: TestTweetsComponent;
  let fixture: ComponentFixture<TestTweetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestTweetsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestTweetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
