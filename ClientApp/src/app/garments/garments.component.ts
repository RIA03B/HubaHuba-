import { HttpClient, HttpEventType, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Message, MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserInfoService } from '../services/user-info.service';
interface ImageFile {
  Name : any;
  Index : any;
}
@Component({
  selector: 'app-garments',
  templateUrl: './garments.component.html',
  styleUrls: ['./garments.component.css']
})
export class GarmentsComponent implements OnInit {
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
  imagePath : any; URLS = new Array(); FilesStorage : ImageFile[]; TestFile : ImageFile;
  deletedialog : boolean = false;  GMTHeaders : any[]; GMTData = new Array(); selectedGMT : any;
  public progress: number; public message: string; isImageLoading : boolean;  NStyles : any;
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

    this.GMTHeaders =[
      'Index',
      'Image',
      'Name'
    ];
    console.log(this.GMTHeaders);
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

  onFileChanged(event) {
    const files = event.target.files;
    var name = files[0].name;
    if (files.length === 0)
        return;
    const mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.messageService.add({ key: 'validation', severity: 'error', summary: 'Only Image Files Allowed.', detail: '' });
      setTimeout(()=>this.messageService.clear("validation"),20000);
      return;
    }
    const reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.URLS.push(reader.result); 
      this.GMTData.push(
        {Index: this.URLS.length, Image : reader.result, Name: name , Length: this.URLS.length - 1}
      );
    };
  }
  onRowEditCancel(data , index){
    console.log(data);
    console.log(index);
    this.deletedialog = true;
  }
  CloseDeletImageDialog(){
    this.deletedialog = false;
  }
  ConfirmDeleteImage(){
    this.deletedialog = false;
  }
  SubmitOrder(){
    if(this.NStyles == undefined || this.NStyles == null || this.GMTData == undefined || this.GMTData == null || this.GMTData.length <= 0){
      this.messageService.add({ key: 'validation', severity: 'error', summary: 'Please insert at least 1 image and select a number of styles.', detail: '' });
      setTimeout(()=>this.messageService.clear("validation"),20000);
      return;
    }
  }
}
