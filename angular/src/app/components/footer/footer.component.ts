import { ContactServiceService } from './../../ChildService/Contact/contact-service.service';
import { Component, OnInit } from '@angular/core';
import { ServerHttpService } from '../../Services/server-http.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {
 public contacts:any;
  constructor(
    private contactService:ContactServiceService
  ) { }

  ngOnInit(): void 
  {
    this.contactService.getContacts().subscribe((data)=>{
      this.contacts=data;
    })
  }

}
