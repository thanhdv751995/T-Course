import { AuthGuard } from './Services/auth.guard';
import { ProfileComponent } from './profile/profile.component';
import { SettingComponent } from './setting/setting.component';
import { CourseComponent } from './course/course.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactusComponent } from './contactus/contactus.component';
import { CoursedetailComponent } from './coursedetail/coursedetail.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { LearnLessonComponent } from './learn-lesson/learn-lesson.component';

import { AdminComponent } from './admin/admin.component';
import { Role } from '@proxy/user/models';

import { ForgotpassComponent } from './forgotpass/forgotpass.component';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';


const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule), 
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'aboutus',component:AboutusComponent
  },

  {
    path: 'contactus', component: ContactusComponent
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'courses', loadChildren: () => import('./course/course.module').then(m => m.CourseModule) },
  {
    path:'courses/:id',component:CoursedetailComponent
  } ,
  {
    path:'courses/type/:nameType',component:CourseComponent
  }, 
  {
    path:'learn/:idCourse/:idLesson',component:LearnLessonComponent
  } ,
  {
    path:'courses/category/:idCategory',component:CourseComponent
  } ,
  {
    path:'resetpassword/:userid/:token',component:ResetpasswordComponent
  } ,
  {
    path: 'PageNotFound', component: PagenotfoundComponent, data: { header: false}
  },
    {
    path: 'Setting', component: SettingComponent, data: { header: false}
  },
    {
    path: 'Profile', component: ProfileComponent, data: { header: false}
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] }
},
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },
  { path: 'admin/user', loadChildren: () => import('./admin/user/user.module').then(m => m.UserModule) },
  { path: 'admin/category', loadChildren: () => import('./admin/category/category.module').then(m => m.CategoryModule) },
  { path: 'admin/course', loadChildren: () => import('./admin/course/course.module').then(m => m.CourseModule) },
  { path: 'admin/lesson', loadChildren: () => import('./admin/lesson/lesson.module').then(m => m.LessonModule) },
  { path: 'admin/contact', loadChildren: () => import('./admin/contact/contact.module').then(m => m.ContactModule) },
  { path: 'admin/role', loadChildren: () => import('./admin/role/role.module').then(m => m.RoleModule) },
  { path: 'forgotpass', loadChildren: () => import('./forgotpass/forgotpass.module').then(m => m.ForgotpassModule) },
  { path: 'resetpassword', loadChildren: () => import('./resetpassword/resetpassword.module').then(m => m.ResetpasswordModule) },
  { path: '**', redirectTo: '/PageNotFound', data: { header: false } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
