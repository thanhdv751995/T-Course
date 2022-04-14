import { Ng2OrderModule } from 'ng2-order-pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [UserComponent],
  imports: [
    NgbModule,
    CommonModule,
    FormsModule, Ng2SearchPipeModule,Ng2OrderModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
