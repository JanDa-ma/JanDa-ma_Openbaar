import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TestEventComponent } from './TestEvent/TestEvent.component';
import { TestEvent2Component } from './TestEvent2/TestEvent2.component';

@NgModule({
  declarations: [AppComponent, TestEventComponent, TestEvent2Component],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
