import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from './../../shared/shared.module';
import { LessonRoutingModule } from './lesson-routing.module';
import { LessonComponent } from './lesson.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap'
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2OrderModule } from 'ng2-order-pipe';
import { NzSelectModule } from 'ng-zorro-antd/select';


@NgModule({
  declarations: [LessonComponent ],
  imports: [
    NgxPaginationModule,
    Ng2OrderModule,
    ReactiveFormsModule,HttpClientModule,
    NgbModule,
    NzSelectModule,
    FormsModule, Ng2SearchPipeModule,
    CommonModule,
    LessonRoutingModule,SharedModule]
})
export class LessonModule { }
