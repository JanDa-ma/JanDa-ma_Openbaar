/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TestEvent2Component } from './TestEvent2.component';

describe('TestEvent2Component', () => {
  let component: TestEvent2Component;
  let fixture: ComponentFixture<TestEvent2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestEvent2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestEvent2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
