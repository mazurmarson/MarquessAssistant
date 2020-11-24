/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MarqueeStuffComponent } from './marquee-stuff.component';

describe('MarqueeStuffComponent', () => {
  let component: MarqueeStuffComponent;
  let fixture: ComponentFixture<MarqueeStuffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarqueeStuffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarqueeStuffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
