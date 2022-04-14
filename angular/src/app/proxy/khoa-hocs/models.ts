import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CategoryLookupDto extends EntityDto<string> {
  name?: string;
}

export interface CourseDto extends EntityDto<string> {
  name?: string;
  description?: string;
  categoryName?: string;
  benefit?: string;
  nameUser?: string;
  idCategory?: string;
  idUser?: string;
  url?: string;
  fileName?: string;
  lessonName?: string;
  lessonURL?: string;
  lessonDescription?: string;
  lessonID?: string;
  creationTime?: string;
  rateCount: number;
  rateTotalPoint: number;
  rateAverage: number;
}

export interface CreateCourseDto {
  name: string;
  description: string;
  benefit?: string;
  idCategory?: string;
  idUser?: string;
  url?: string;
}

export interface GetCourseListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateCourseDto {
  name: string;
  description: string;
  benefit?: string;
  idCategory?: string;
  idUser?: string;
  url?: string;
}

export interface UserLookupDto extends EntityDto<string> {
  name?: string;
}
