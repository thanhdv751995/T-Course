import { TestBed } from '@angular/core/testing';

import { JoinCourseServiceService } from './join-course-service.service';

describe('JoinCourseServiceService', () => {
  let service: JoinCourseServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JoinCourseServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
