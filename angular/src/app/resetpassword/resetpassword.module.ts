import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule } from '@angular/forms';
import { ResetpasswordRoutingModule } from './resetpassword-routing.module';
import { ResetpasswordComponent } from './resetpassword.component';


@NgModule({
  declarations: [ResetpasswordComponent],
  imports: [
    FormsModule,
    CommonModule,
    ResetpasswordRoutingModule
  ]
})
export class ResetpasswordModule { }
