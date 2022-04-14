
import { NgModule } from '@angular/core';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { SharedModule } from './../../shared/shared.module';
import { CourseRoutingModule } from './course-routing.module';
import { CourseComponent } from './course.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap'
import { Ng2OrderModule } from 'ng2-order-pipe';
import { NzSelectModule } from 'ng-zorro-antd/select';
@NgModule({
  declarations: [CourseComponent],
  imports: [
    NgbModule,
   Ng2SearchPipeModule,Ng2OrderModule,
    SharedModule,
    NzSelectModule,
    CourseRoutingModule
  ]
})
export class CourseModule { }
