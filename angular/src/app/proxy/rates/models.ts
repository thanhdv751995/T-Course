import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CourseLookupDto extends EntityDto<string> {
  name?: string;
}

export interface CreateRateDto {
  ratePoint: number;
  content: string;
  idCourse?: string;
  idUser?: string;
}

export interface GetRateListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface RateDto extends EntityDto<string> {
  ratePoint: number;
  content?: string;
  idCourse?: string;
  courseName?: string;
  userName?: string;
  idUser?: string;
  creationTime?: string;
}

export interface UpdateRateDto {
  ratePoint: number;
  content?: string;
  idCourse?: string;
  idUser?: string;
}
