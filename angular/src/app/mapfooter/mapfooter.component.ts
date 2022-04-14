import { ContactServiceService } from './../ChildService/Contact/contact-service.service';
import { Component, OnInit } from '@angular/core';
import { ServerHttpService } from '../Services/server-http.service';

@Component({
  selector: 'app-mapfooter',
  templateUrl: './mapfooter.component.html',
  styleUrls: ['./mapfooter.component.scss']
})
export class MapfooterComponent implements OnInit {
   public contacts:any;

  constructor(
    private contactService: ContactServiceService
  ) { }

  ngOnInit(): void {
    this.contactService.getContacts().subscribe((data)=>{
      this.contacts=data;

    })
  }

}
