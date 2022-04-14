import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateLessonDto {
  name: string;
  description: string;
  idCourse?: string;
  url?: string;
}

export interface GetLessonListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface LessonDto extends EntityDto<string> {
  name?: string;
  description?: string;
  benefit?: string;
  coursesName?: string;
  idCourse?: string;
  url?: string;
  fileName?: string;
  creationTime?: string;
}

export interface UpdateLessonDto {
  name: string;
  description: string;
  idCourse?: string;
  url?: string;
}
