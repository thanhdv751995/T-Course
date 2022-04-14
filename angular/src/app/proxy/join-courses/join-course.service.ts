import type { CreateJoinCourseDto, GetJoinCourseListDto, JoinCourseDto, UpdateJoinCourseDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { UserLookupDto } from '../khoa-hocs/models';
import type { CourseLookupDto } from '../rates/models';

@Injectable({
  providedIn: 'root',
})
export class JoinCourseService {
  apiName = 'Default';

  create = (input: CreateJoinCourseDto) =>
    this.restService.request<any, JoinCourseDto>({
      method: 'POST',
      url: `/api/app/join-course`,
      body: input,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, JoinCourseDto>({
      method: 'GET',
      url: `/api/app/join-course/${id}`,
    },
    { apiName: this.apiName });

  getCourseLookup = () =>
    this.restService.request<any, ListResultDto<CourseLookupDto>>({
      method: 'GET',
      url: `/api/app/join-course/course-lookup`,
    },
    { apiName: this.apiName });

  getList = (input: GetJoinCourseListDto) =>
    this.restService.request<any, PagedResultDto<JoinCourseDto>>({
      method: 'GET',
      url: `/api/app/join-course`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  getUserLookup = () =>
    this.restService.request<any, ListResultDto<UserLookupDto>>({
      method: 'GET',
      url: `/api/app/join-course/user-lookup`,
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateJoinCourseDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/join-course/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
