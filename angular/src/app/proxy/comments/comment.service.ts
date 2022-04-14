import type { CommentDto, CreateCommentDto, GetCommentListDto, LessonLookupDto, UpdateCommentDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { UserLookupDto } from '../khoa-hocs/models';
import { HttpHeaders } from '@angular/common/http';
import { localStor } from 'src/app/Services/local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  apiName = 'Default';

  create = (input: CreateCommentDto) =>
    this.restService.request<any, CommentDto>({
      method: 'POST',
      url: `/api/app/comment`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/comment/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, CommentDto>({
      method: 'GET',
      url: `/api/app/comment/${id}`,
    },
    { apiName: this.apiName });

  getLessonLookup = () =>
    this.restService.request<any, ListResultDto<LessonLookupDto>>({
      method: 'GET',
      url: `/api/app/comment/lesson-lookup`,
    },
    { apiName: this.apiName });

  getList = (input: GetCommentListDto) =>
    this.restService.request<any, PagedResultDto<CommentDto>>({
      method: 'GET',
      url: `/api/app/comment`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  getUserLookup = () =>
    this.restService.request<any, ListResultDto<UserLookupDto>>({
      method: 'GET',
      url: `/api/app/comment/user-lookup`,
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateCommentDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/comment/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
