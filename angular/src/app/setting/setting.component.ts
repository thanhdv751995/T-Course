import { JoinCourseServiceService } from './../ChildService/JoinCourse/join-course-service.service';
import { Component, OnInit } from '@angular/core';
import { ServerHttpService } from '../Services/server-http.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.scss']
})
export class SettingComponent implements OnInit {
  public userLogin;
  public listCourseUserJoin;
  searchText;
  constructor(
    private joinCourseService:JoinCourseServiceService,
    private serverHttpService:ServerHttpService,
  ) { }

  ngOnInit(): void {
    this.userLogin=this.serverHttpService.currentUserLogin;
    this.getListCourseUserJoin();
  } 
  selectLanguage()
  {
    var value=document.getElementById('inlineFormCustomSelect');
    console.log(value)
  }
  getListCourseUserJoin(){
    if(this.userLogin){
    this.joinCourseService.getListCourseUserJoin(this.userLogin).subscribe(data=>{
      this.listCourseUserJoin=data;
      console.log(data)
    })
    }
  }
  deleteJoinCourse(id){
    this.joinCourseService.deleteJoinCourse(id).subscribe(()=>{
      this.getListCourseUserJoin();
    })
  }
  key = 'creationTime';
  reserve: boolean = false;
  sort(key) {
    this.key = key;
    this.reserve = !this.reserve;
  }
}
