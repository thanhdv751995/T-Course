import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2OrderModule } from 'ng2-order-pipe';
import { RoleRoutingModule } from './role-routing.module';
import { RoleComponent } from './role.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [RoleComponent],
  imports: [
    NgbModule,
    CommonModule,
    SharedModule,
    FormsModule, Ng2SearchPipeModule,Ng2OrderModule,
    RoleRoutingModule
  ]
})
export class RoleModule { }
