import { CommonServiceService } from 'src/app/Services/common-service.service';
import { CoreModule } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import { IdentityConfigModule } from '@abp/ng.identity/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { TenantManagementConfigModule } from '@abp/ng.tenant-management/config';
import { ThemeBasicModule } from '@abp/ng.theme.basic';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule,LOCALE_ID  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxsModule } from '@ngxs/store';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { CoursedetailComponent } from './coursedetail/coursedetail.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactusComponent } from './contactus/contactus.component';
import { MapfooterComponent } from './mapfooter/mapfooter.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SafePipe } from './app.safeurl';
import { LearnLessonComponent } from './learn-lesson/learn-lesson.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedService } from './admin/shared.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProfileComponent } from './profile/profile.component';
import { SettingComponent } from './setting/setting.component';
import { ToastrModule } from 'ngx-toastr';
import {NgxSpinnerModule} from 'ngx-spinner'
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { IconsProviderModule } from './icons-provider.module';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzFormModule } from 'ng-zorro-antd/form';
import { storeLocaleData } from '@abp/ng.core/locale';
import(
/* webpackChunkName: "_locale-your-locale-js"*/
/* webpackMode: "eager" */
'@angular/common/locales/vi'
).then(m => storeLocaleData(m.default, 'vi'));
registerLocaleData(en);



@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
    timeOut: 5000,
    positionClass: 'toast-top-right',
    preventDuplicates: true,
  }),
  NgxSpinnerModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(
        {
          cultureNameLocaleFileMap: { "DotnetCultureName": "AngularLocaleFileName",'vi': 'vi' },
        },
        ),
    }),
    ThemeSharedModule.forRoot(),
    IdentityConfigModule.forRoot(),
    TenantManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    NgxsModule.forRoot(),
    ThemeBasicModule.forRoot(),
    NgbModule,
    IconsProviderModule,
    NzLayoutModule,
    NzMenuModule,
    NzSelectModule
  ],
  declarations: [MapfooterComponent, AppComponent, HeaderComponent, FooterComponent , CoursedetailComponent, AboutusComponent, ContactusComponent, PagenotfoundComponent, SafePipe, LearnLessonComponent, ProfileComponent, SettingComponent],
  providers: [APP_ROUTE_PROVIDER, SharedService, { provide: LOCALE_ID, useValue: 'en-US'}, { provide: NZ_I18N, useValue: en_US },],
  bootstrap: [AppComponent],
})
export class AppModule { }
