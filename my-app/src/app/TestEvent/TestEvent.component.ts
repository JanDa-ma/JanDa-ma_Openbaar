import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'TestEvent',
  templateUrl: './TestEvent.component.html',
  styleUrls: ['./TestEvent.component.scss'],
})
export class TestEventComponent implements OnInit {
  tekst: string = 'blabla';
  constructor() {}

  ngOnInit() {
    alert('init');
  }
  geefBericht() {
    console.log('test');
    this.tekst = 'Tekst 1';
  }
}
