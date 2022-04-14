import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule } from '@angular/forms';
import { ForgotpassRoutingModule } from './forgotpass-routing.module';
import { ForgotpassComponent } from './forgotpass.component';


@NgModule({
  declarations: [ForgotpassComponent],
  imports: [
    FormsModule,
    CommonModule,
    ForgotpassRoutingModule
  ]
})
export class ForgotpassModule { }
