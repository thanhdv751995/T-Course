import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateJoinCourseDto {
  idCourse?: string;
  idUser?: string;
  status: boolean;
}

export interface GetJoinCourseListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface JoinCourseDto extends EntityDto<string> {
  idCourse?: string;
  idUser?: string;
  status: boolean;
  coursesName?: string;
  userName?: string;
}

export interface UpdateJoinCourseDto {
  idCourse?: string;
  idUser?: string;
  status: boolean;
}
