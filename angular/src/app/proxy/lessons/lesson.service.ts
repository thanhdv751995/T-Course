
import type { CreateLessonDto, GetLessonListDto, LessonDto, UpdateLessonDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CourseLookupDto } from '../rates/models';
import { localStor } from '../../Services/local-storage.service';
import { HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class LessonService {
  apiName = 'Default';

  getListCourseByCategoryIDByID = (ID: string) =>
  this.restService.request<any, ListResultDto<LessonDto>>({
    method: 'GET',
    url: `/api/app/lesson/async-by-course-iD`,
    params: { id: ID },
    headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
  },
  { apiName: this.apiName });
  create = (input: CreateLessonDto) =>
    this.restService.request<any, LessonDto>({
      method: 'POST',
      url: `/api/app/lesson`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/lesson/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, LessonDto>({
      method: 'GET',
      url: `/api/app/lesson/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getById = (id: string) =>
    this.restService.request<any, LessonDto>({
      method: 'GET',
      url: `/api/app/lesson/${id}/by-id`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getCourseLookup = () =>
    this.restService.request<any, ListResultDto<CourseLookupDto>>({
      method: 'GET',
      url: `/api/app/lesson/course-lookup`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getList = (input: GetLessonListDto) =>
    this.restService.request<any, PagedResultDto<LessonDto>>({
      method: 'GET',
      url: `/api/app/lesson`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getListAll = (input: GetLessonListDto) =>
    this.restService.request<any, PagedResultDto<LessonDto>>({
      method: 'GET',
      url: `/api/app/lesson/all`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateLessonDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/lesson/${id}`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
