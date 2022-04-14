import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})


export class ServerHttpService {
  public dangerousVideoUrl:any;
  public videoUrl:any;
  public countForTrending=5;
  public totalPageNumberOfCourse;
  public lookupCategoryLinkApi='/api/app/course/category-lookup';
  public getCategory='/api/app/category';
 public currentUserLogin;
 public currentUserRole;
 public roleDefaultAdmin='admin';
  constructor(
    public sanitizer: DomSanitizer,
    private router:Router,
    ) { this.getCurrentUser();
    this.getCurrentUserRole();}
    convertVideoToSafe(url)
    {
      this.dangerousVideoUrl = 'https://www.youtube.com/embed/' + url;
        this.videoUrl =
        this.sanitizer.bypassSecurityTrustResourceUrl(this.dangerousVideoUrl);
    }
    reloadRouter()
    {
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }
    Pagination(coursesMaxResultCount,listPageNumberTotal,numberPagination:number)
    {
      this.totalPageNumberOfCourse=Math.ceil(coursesMaxResultCount.totalCount/numberPagination);
          for(let i=1;i<=this.totalPageNumberOfCourse;i++)
          {
            listPageNumberTotal.push(i);
          }
    }
    redirectTo(uri:string){
      this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
      this.router.navigate([uri]));
      }
    getCurrentUser()
    {
      if(localStorage.getItem('CurrentUser')){
        this.currentUserLogin =(JSON.parse(localStorage.getItem('CurrentUser'))).sub;
      }
    }
    getCurrentUserRole()
    {
      if(localStorage.getItem('CurrentUser')){
        this.currentUserRole =(JSON.parse(localStorage.getItem('CurrentUser'))).role;
      }
    }
}
