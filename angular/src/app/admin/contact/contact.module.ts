import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from './../../shared/shared.module';
import { ContactRoutingModule } from './contact-routing.module';
import { ContactComponent } from './contact.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap'
import { Ng2OrderModule } from 'ng2-order-pipe';
import { Ng2SearchPipeModule } from 'ng2-search-filter';


@NgModule({
  declarations: [ContactComponent],
  imports: [
    NgbModule,
    Ng2SearchPipeModule,Ng2OrderModule,
    SharedModule,
    ContactRoutingModule
  ]
})
export class ContactModule { }
