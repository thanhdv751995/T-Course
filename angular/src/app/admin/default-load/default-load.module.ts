
import { DefaultLoadComponent } from './default-load.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
// For MDB Angular Free
  import { ChartsModule, WavesModule } from 'angular-bootstrap-md';
import { Chart } from 'chart.js';
@NgModule({
  declarations: [DefaultLoadComponent],
  imports: [
    ChartsModule,
    WavesModule,
    BrowserModule,
    FormsModule,
    Chart

  ],
    bootstrap:[ DefaultLoadComponent ]
})
export class DefaultLoadModule { }
