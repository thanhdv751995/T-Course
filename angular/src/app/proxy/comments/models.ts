import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CommentDto extends EntityDto<string> {
  content?: string;
  url?: string;
  idParent?: string;
  idLesson?: string;
  lessonName?: string;
  idUser?: string;
  userName?: string;
}

export interface CreateCommentDto {
  content: string;
  idParent?: string;
  idLesson: string;
  idUser: string;
}

export interface GetCommentListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface LessonLookupDto extends EntityDto<string> {
  name?: string;
}

export interface UpdateCommentDto {
  content: string;
  idParent?: string;
  idLesson: string;
  idUser: string;
}
