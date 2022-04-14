import { localStor } from 'src/app/Services/local-storage.service';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
@Component({
  selector: 'app-root',
  template: `
  
    <app-header *ngIf="this.router.url!='/PageNotFound' && this.router.url!='/admin'&& this.router.url!='/admin/category'&& this.router.url!='/admin/course'&& this.router.url!='/admin/lesson'&& this.router.url!='/admin/contact'&& this.router.url!='/admin/role'&& this.router.url!='/admin/user'"></app-header>
    <router-outlet (activate)="onActivate($event)"></router-outlet>
    <app-mapfooter *ngIf="this.router.url!='/PageNotFound' && this.router.url!='/admin'&& this.router.url!='/admin/category'&& this.router.url!='/admin/course'&& this.router.url!='/admin/lesson'&& this.router.url!='/admin/contact'&& this.router.url!='/admin/role'&& this.router.url!='/admin/user'"></app-mapfooter>
    <app-footer *ngIf="this.router.url!='/PageNotFound' && this.router.url!='/admin'&& this.router.url!='/admin/category'&& this.router.url!='/admin/course'&& this.router.url!='/admin/lesson'&& this.router.url!='/admin/contact'&& this.router.url!='/admin/role'&& this.router.url!='/admin/user'"></app-footer>
  `,
})
export class AppComponent{

     constructor(
        public router: Router,
     )
     {
     }

    onActivate(event) {
        let scrollToTop = window.setInterval(() => {
            let pos = window.pageYOffset;
            if (pos > 0) {
                window.scrollTo(0, pos - 1000); // how far to scroll on each step
            } else {
                window.clearInterval(scrollToTop);
            }
        }, 16);
    }
}