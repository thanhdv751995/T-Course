import { DefaultLoadComponent } from './default-load/default-load.component';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { CategoryComponent } from './category/category.component';
import { CourseComponent } from './course/course.component';
import { LessonComponent } from './lesson/lesson.component';
import { UserComponent } from './user/user.component';
import { ContactComponent } from './contact/contact.component';
import { RoleComponent } from './role/role.component';

const routes: Routes = [{
  path: '', component: AdminComponent, children: [
    
    {path: 'contact', component: ContactComponent},
    {
    path: 'user', component: UserComponent
  },
  {
    path:'Load',component:DefaultLoadComponent
  },
  {
    path: 'category', component: CategoryComponent
  },
  {
    path: 'course', component: CourseComponent
  },
  {
    path: 'role', component: RoleComponent
  },
  {
    path: 'lesson', component: LessonComponent
  }]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
