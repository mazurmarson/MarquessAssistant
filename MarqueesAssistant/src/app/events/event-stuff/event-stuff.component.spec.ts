/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EventStuffComponent } from './event-stuff.component';

describe('EventStuffComponent', () => {
  let component: EventStuffComponent;
  let fixture: ComponentFixture<EventStuffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventStuffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventStuffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
