

import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';

// For MDB Angular Free
import { ChartsModule, WavesModule } from 'angular-bootstrap-md'
@NgModule({
  declarations: [HomeComponent],
  imports: [SharedModule, HomeRoutingModule,ChartsModule,WavesModule],
})
export class HomeModule {}
