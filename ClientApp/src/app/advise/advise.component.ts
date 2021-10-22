import { HttpClient, HttpEventType, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Message, MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserInfoService } from '../services/user-info.service';
interface Topics {
  index: number,
  name: string
}
@Component({
  selector: 'app-advise',
  templateUrl: './advise.component.html',
  styleUrls: ['./advise.component.css']
})
export class AdviseComponent implements OnInit {
  isExpanded = false;
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  isUserValid : boolean = false;
  userID: string;
  baseURL : any;
  isProfileSetup : boolean = true;
  customMessage : Message[];
  ValidationMessage : Message[];
  loading : any; loaded : any;
  topics: Topics[]; selectedTopic : Topics;
  public progress: number; public message: string; isImageLoading : boolean; 
  constructor(@Inject('BASE_URL') baseUrl: string , private authorizeService: AuthorizeService , private dataService : UserInfoService , private http : HttpClient , private messageService : MessageService , private router : Router) { 
    this.baseURL = baseUrl;
  }
  
  ngOnInit() {
    this.loading = true;
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.load_userID();
    setTimeout(() => {
      if(this.userID){
        this.isUserValid = true;
        this.CheckCustomerProfile();
      } else {
        this.isUserValid = false;
        this.router.navigate(['/home']);
      }
      console.log(this.userID);
      console.log(this.isUserValid);
      
    }, 1000)

    this.loading = false;

    this.topics = [
      {index: 1, name: 'Hair'},
      {index: 2, name: 'Make Up'},
      {index: 3, name: 'Accessories'},
      {index: 4, name: 'Shoes'}
  ];
  }

  clearMessages(name) {
    if(name == "customMessage"){
      this.customMessage = [];
    }
  }
  load_userID() {
    this.dataService.get_UserIdGuid().subscribe(response => {
      this.userID = response;
    }
    );
  }
  CheckCustomerProfile(){
    this.loading = true;  
    this.http.get(this.baseURL + 'api/CheckCustomerProfile/' + this.userID).subscribe(
        (response : any[]) => {
          this.loading = false;
          if(response[0].message == "False"){
            this.isProfileSetup = false;
            this.router.navigate(['/home']);
          } else {
            this.isProfileSetup = true;
          }
          console.log(response[0].message);
        }, (error) => {})
  }
}