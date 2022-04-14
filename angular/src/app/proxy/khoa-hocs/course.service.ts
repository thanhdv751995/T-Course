import type { CategoryLookupDto, CourseDto, CreateCourseDto, GetCourseListDto, UpdateCourseDto, UserLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AttachmentLookupDto } from '../courses/models';
import { localStor } from 'src/app/Services/local-storage.service';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  apiName = 'Default';

  create = (input: CreateCourseDto) =>
    this.restService.request<any, CourseDto>({
      method: 'POST',
      url: `/api/app/course`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/course/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, CourseDto>({
      method: 'GET',
      url: `/api/app/course/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });
    getID = (id: string) =>
    this.restService.request<any, CourseDto>({
      method: 'GET',
      url: `/api/app/course/${id}/by-id`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });
  getAttachmentLookup = () =>
    this.restService.request<any, ListResultDto<AttachmentLookupDto>>({
      method: 'GET',
      url: `/api/app/course/attachment-lookup`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getCategoryLookup = () =>
    this.restService.request<any, ListResultDto<CategoryLookupDto>>({
      method: 'GET',
      url: `/api/app/course/category-lookup`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getList = (input: GetCourseListDto) =>
    this.restService.request<any, PagedResultDto<CourseDto>>({
      method: 'GET',
      url: `/api/app/course`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount },
      // headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });
    getListAll = (input: GetCourseListDto) =>
    this.restService.request<any, PagedResultDto<CourseDto>>({
      method: 'GET',
      url: `/api/app/course/all`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getListCourse = (input: GetCourseListDto) =>
    this.restService.request<any, PagedResultDto<CourseDto>>({
      method: 'GET',
      url: `/api/app/course/course`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getListCourseByCategoryIDByID = (ID: string) =>
    this.restService.request<any, ListResultDto<CourseDto>>({
      method: 'GET',
      url: `/api/app/course/course-by-category-iD`,
      params: { id: ID },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getListLessonByIDCourseByID = (ID: string) =>
    this.restService.request<any, ListResultDto<CourseDto>>({
      method: 'GET',
      url: `/api/app/course/lesson-by-iDCourse`,
      params: { id: ID },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getUserLookup = () =>
    this.restService.request<any, ListResultDto<UserLookupDto>>({
      method: 'GET',
      url: `/api/app/course/user-lookup`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateCourseDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/course/${id}`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
