import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Ng2OrderModule } from 'ng2-order-pipe';
import { NzSelectModule } from 'ng-zorro-antd/select';
@NgModule({
  declarations: [CategoryComponent],
  imports: [
    NgbModule,
    CommonModule,
    FormsModule, Ng2SearchPipeModule,Ng2OrderModule,
    SharedModule,
    NzSelectModule,
    CategoryRoutingModule
  ]
})
export class CategoryModule { }
