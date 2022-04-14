import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/admin-manager',
        name: '::Menu:Manager',
        iconClass: 'fas fa-manager',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/myroles',
        name: '::Menu:role',
        parentName: '::Menu:Manager',
        layout: eLayoutType.application,
        requiredPolicy: 'Project.Roles',
      },
      {
        path: '/myusers',
        name: '::Menu:user',
        parentName: '::Menu:Manager',
        layout: eLayoutType.application,
        requiredPolicy: 'Project.Users',
      },
     
      {
        path: '/categories',
        name: '::Menu:category',
        parentName: '::Menu:Manager',
        layout: eLayoutType.application,
        requiredPolicy: 'Project.Categories',
      },
      {
        path: '/courses',
        name: '::Menu:course',
        parentName: '::Menu:Manager',
        layout: eLayoutType.application,
        requiredPolicy: 'Project.Courses',
      },
       {
        path: '/comments',
        name: '::Menu:Comment',
        parentName: '::Menu:Manager',
        layout: eLayoutType.application,
        //requiredPolicy: 'Project.Comments',
      },
      {
        path: '/lessons',
        name: 'Lesson Manager',
        parentName: '::Menu:Manager',
        layout: eLayoutType.application,
        requiredPolicy: 'Project.Lessons',
      },
    ]);
  };
}
