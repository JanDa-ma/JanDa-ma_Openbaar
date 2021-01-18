import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { TestEventComponent } from './TestEvent.component';

describe('TestEventComponent', () => {
  let component: TestEventComponent;
  let fixture: ComponentFixture<TestEventComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      schemas: [NO_ERRORS_SCHEMA],
      declarations: [TestEventComponent]
    });
    fixture = TestBed.createComponent(TestEventComponent);
    component = fixture.componentInstance;
  });

  it('can load instance', () => {
    expect(component).toBeTruthy();
  });
});
