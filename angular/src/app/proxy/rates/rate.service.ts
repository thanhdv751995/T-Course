import type { CourseLookupDto, CreateRateDto, GetRateListDto, RateDto, UpdateRateDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { UserLookupDto } from '../khoa-hocs/models';
import { HttpHeaders } from '@angular/common/http';
import { localStor } from 'src/app/Services/local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class RateService {
  apiName = 'Default';

  create = (input: CreateRateDto) =>
    this.restService.request<any, RateDto>({
      method: 'POST',
      url: `/api/app/rate`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/rate/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, RateDto>({
      method: 'GET',
      url: `/api/app/rate/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getCourseLookup = () =>
    this.restService.request<any, ListResultDto<CourseLookupDto>>({
      method: 'GET',
      url: `/api/app/rate/course-lookup`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getList = (input: GetRateListDto) =>
    this.restService.request<any, PagedResultDto<RateDto>>({
      method: 'GET',
      url: `/api/app/rate`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getListRateByIDCourseByID = (ID: string) =>
    this.restService.request<any, ListResultDto<RateDto>>({
      method: 'GET',
      url: `/api/app/rate/rate-by-iDCourse`,
      params: { id: ID },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getUserLookup = () =>
    this.restService.request<any, ListResultDto<UserLookupDto>>({
      method: 'GET',
      url: `/api/app/rate/user-lookup`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateRateDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/rate/${id}`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
