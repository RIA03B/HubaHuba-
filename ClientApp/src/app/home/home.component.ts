import { HttpClient, HttpEventType, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Message, MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserInfoService } from '../services/user-info.service';
export interface Image{}
export interface headers{}
export interface CustomerProfile{
  UID: string; FirstName: string; LastName: string; PhoneNumber: string; HairColor: string; SkinColor: string; EyeColor: string; FaceShapeID: string; Height: string; Weight: string;
  Insecurities: string; Securities: string; FavTV_Movies: string; JobDesc: string; AcomplishedFeeling: string; VibeWanted: string;
}
interface CustomerProfiles extends Array<CustomerProfile>{}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isExpanded = false;
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  isUserValid : boolean = false;
  userID: string;
  baseURL : any;
  isProfileSetup : boolean = true;
  submitted: boolean = false;

  FirstName : any; LastName : any; PhoneNumber : any;

  haircolors: any[]; selectedHairColor: any;
  skincolors: any[]; selectedSkinColor: any;
  eyecolors: any[]; selectedEyeColor: any;
  faceshapes : any[]; selectedFaceShape : any;

  Height: any; Weight : any;

  Insecurities : any; Securities : any; FavouriteTvMovie : any; JobDesc : any; AcomplishedFeeling : any; VibeWanted : any;
  selectedPhoto: any; fileMessage : Message[]; filePhotoName : any;
  ValidationMessage : string;
  loading : any; loaded : any;
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
      }
      console.log(this.userID);
      console.log(this.isUserValid);
      
    }, 1000)

    this.haircolors = [
      {name: 'Color 1', src: 'assets/Images/HairColors/Color1.PNG'},
      {name: 'Color 2', src: 'assets/Images/HairColors/Color2.PNG'},
      {name: 'Color 3', src: 'assets/Images/HairColors/Color3.PNG'},
      {name: 'Color 4', src: 'assets/Images/HairColors/Color4.PNG'},
      {name: 'Color 5', src: 'assets/Images/HairColors/Color5.PNG'},
      {name: 'Color 6', src: 'assets/Images/HairColors/Color6.PNG'},
      {name: 'Color 7', src: 'assets/Images/HairColors/Color7.PNG'},
      {name: 'Color 8', src: 'assets/Images/HairColors/Color8.PNG'},
      {name: 'Color 9', src: 'assets/Images/HairColors/Color9.PNG'},
      {name: 'Color 10', src: 'assets/Images/HairColors/Color10.PNG'},
      {name: 'Color 11', src: 'assets/Images/HairColors/Color11.PNG'},
      {name: 'Color 12', src: 'assets/Images/HairColors/Color12.PNG'},
      {name: 'Color 13', src: 'assets/Images/HairColors/Color13.PNG'},
      {name: 'Color 14', src: 'assets/Images/HairColors/Color14.PNG'},
      {name: 'Color 15', src: 'assets/Images/HairColors/Color15.PNG'},
      {name: 'Color 16', src: 'assets/Images/HairColors/Color16.PNG'}
    ];

    this.skincolors = [
      {name: 'Color 1', src: 'assets/Images/SkinColors/Skin1.PNG'},
      {name: 'Color 2', src: 'assets/Images/SkinColors/Skin2.PNG'},
      {name: 'Color 3', src: 'assets/Images/SkinColors/Skin3.PNG'},
      {name: 'Color 4', src: 'assets/Images/SkinColors/Skin4.PNG'},
      {name: 'Color 5', src: 'assets/Images/SkinColors/Skin5.PNG'},
      {name: 'Color 6', src: 'assets/Images/SkinColors/Skin6.PNG'},
      {name: 'Color 7', src: 'assets/Images/SkinColors/Skin7.PNG'},
      {name: 'Color 8', src: 'assets/Images/SkinColors/Skin8.PNG'}
    ];

    this.eyecolors = [
      {name: 'Color 1', src: 'assets/Images/EyeColors/Eye1.jpg'},
      {name: 'Color 2', src: 'assets/Images/EyeColors/Eye2.jpg'},
      {name: 'Color 3', src: 'assets/Images/EyeColors/Eye3.jpg'},
      {name: 'Color 4', src: 'assets/Images/EyeColors/Eye4.jpg'},
      {name: 'Color 5', src: 'assets/Images/EyeColors/Eye5.jpg'},
      {name: 'Color 6', src: 'assets/Images/EyeColors/Eye6.jpg'},
      {name: 'Color 7', src: 'assets/Images/EyeColors/Eye7.jpg'},
      {name: 'Color 8', src: 'assets/Images/EyeColors/Eye8.jpg'},
      {name: 'Color 9', src: 'assets/Images/EyeColors/Eye9.jpg'},
      {name: 'Color 10', src: 'assets/Images/EyeColors/Eye10.jpg'},
      {name: 'Color 11', src: 'assets/Images/EyeColors/Eye11.jpg'},
      {name: 'Color 12', src: 'assets/Images/EyeColors/Eye12.jpg'},
      {name: 'Color 13', src: 'assets/Images/EyeColors/Eye13.jpg'},
      {name: 'Color 14', src: 'assets/Images/EyeColors/Eye14.jpg'},
      {name: 'Color 15', src: 'assets/Images/EyeColors/Eye15.jpg'},
      {name: 'Color 16', src: 'assets/Images/EyeColors/Eye16.jpg'},
      {name: 'Color 17', src: 'assets/Images/EyeColors/Eye17.jpg'},
      {name: 'Color 18', src: 'assets/Images/EyeColors/Eye18.jpg'},
      {name: 'Color 19', src: 'assets/Images/EyeColors/Eye19.jpg'},
      {name: 'Color 20', src: 'assets/Images/EyeColors/Eye20.jpg'},
      {name: 'Color 21', src: 'assets/Images/EyeColors/Eye21.jpg'},
      {name: 'Color 22', src: 'assets/Images/EyeColors/Eye22.jpg'},
      {name: 'Color 23', src: 'assets/Images/EyeColors/Eye23.jpg'},
      {name: 'Color 24', src: 'assets/Images/EyeColors/Eye24.jpg'},
      {name: 'Color 25', src: 'assets/Images/EyeColors/Eye25.jpg'}
    ];

    this.faceshapes = [
      {name: 'Shape 1', src: 'assets/Images/FaceShapes/Face1.jpg'},
      {name: 'Shape 2', src: 'assets/Images/FaceShapes/Face2.jpg'},
      {name: 'Shape 3', src: 'assets/Images/FaceShapes/Face3.jpg'},
      {name: 'Shape 4', src: 'assets/Images/FaceShapes/Face4.jpg'},
      {name: 'Shape 5', src: 'assets/Images/FaceShapes/Face5.jpg'},
      {name: 'Shape 6', src: 'assets/Images/FaceShapes/Face6.jpg'},
      {name: 'Shape 7', src: 'assets/Images/FaceShapes/Face7.jpg'},
      {name: 'Shape 8', src: 'assets/Images/FaceShapes/Face8.jpg'}
    ];
    this.loading = false;
  }

  fileChange(files:any[]) {
    console.log(files[0]);
    if (files && files.length > 0) {
     var index = files[0].name.lastIndexOf(".") + 1;
     var fileName = files[0].name.substring(index).toLowerCase();
     if (fileName=="jpg" || fileName=="jpeg" || fileName=="png"){
        this.filePhotoName = files[0].name;
        this.clearMessages("fileMessage");
      } else{
        this.filePhotoName = undefined;
        this.fileMessage = [
          {severity:'custom', summary:'Error', detail:'File selected must be an image (jpg , jpeg , png).', icon: 'pi-file'}
        ];
        setTimeout(()=> this.clearMessages("fileMessage"),10000);
      }   
    }
  }

  clearMessages(name) {
    if(name == "fileMessage"){
      this.fileMessage = [];
    }
  }

  SubmitClick(files) {
    this.loading = true;  
    var message = [];
    //Personal Information
    if(this.FirstName == undefined || this.FirstName == null || this.FirstName.length <= 0){
       message.push("First Name");
    }
    if (this.LastName == undefined || this.LastName == null || this.LastName.length <= 0){
      message.push("Last Name");
    }
    if (this.PhoneNumber == undefined || this.PhoneNumber == null || this.PhoneNumber.length <= 0){
      message.push("Phone Number");
    }
    //Body Characteristics
    if (this.selectedHairColor == undefined || this.selectedHairColor == null || this.selectedHairColor.length <= 0){
      message.push("Hair Color");
    }
    if (this.selectedSkinColor == undefined || this.selectedSkinColor == null || this.selectedSkinColor.length <= 0){
      message.push("Skin Color");
    }
    if (this.selectedEyeColor == undefined || this.selectedEyeColor == null || this.selectedEyeColor.length <= 0){
      message.push("Eye Color");
    }
    if (this.selectedFaceShape == undefined || this.selectedFaceShape == null || this.selectedFaceShape.length <= 0){
      message.push("Face Shape");
    }
    if (this.Height == undefined || this.Height == null || this.Height.length <= 0){
      message.push("Height");
    }
    if (this.Weight == undefined || this.Weight == null || this.Weight.length <= 0){
      message.push("Weight");
    }
    //Personality Characteristics
    if (this.Insecurities == undefined || this.Insecurities == null || this.Insecurities.length <= 0){
      message.push("Insecurities");
    }
    if (this.Securities == undefined || this.Securities == null || this.Securities.length <= 0){
      message.push("Securities");
    }
    if (this.FavouriteTvMovie == undefined || this.FavouriteTvMovie == null || this.FavouriteTvMovie.length <= 0){
      message.push("Favourite TV-Shows or Movies");
    }
    if (this.JobDesc == undefined || this.JobDesc == null || this.JobDesc.length <= 0){
      message.push("Job Description");
    }
    if (this.AcomplishedFeeling == undefined || this.AcomplishedFeeling == null || this.AcomplishedFeeling.length <= 0){
      message.push("Accomplished Feeling");
    }
    if (this.VibeWanted == undefined || this.VibeWanted == null || this.VibeWanted.length <= 0){
      message.push("Vibe Wanted");
    }
    if (this.filePhotoName == undefined || this.filePhotoName == null || this.filePhotoName.length <= 0){
      message.push("Profile Photo");
    }
    var endMessage = "";
    if(message.length > 1){
      endMessage = " are not valid.";
    } else if (message.length <= 1) {
      endMessage = " is not valid.";
    }
    if(message.length > 0){
      this.messageService.add({ key: 'validation', severity: 'error', summary: message.join(", ") + endMessage, detail: '' });
      setTimeout(()=>this.messageService.clear("validation"),10000);
      this.loading = false;  
      return;
    } else {
      var result: CustomerProfiles = [
        {  UID: this.userID.toString(), FirstName: this.FirstName.toString(),LastName: this.LastName.toString(),PhoneNumber: this.PhoneNumber.toString(),HairColor: this.selectedHairColor.name.toString(),SkinColor: this.selectedSkinColor.name.toString(),
          EyeColor: this.selectedEyeColor.name.toString(),FaceShapeID: this.selectedFaceShape.name.toString(),Height: this.Height.toString(),Weight: this.Weight.toString(),Insecurities: this.Insecurities.toString(),
          Securities: this.Securities.toString(),FavTV_Movies: this.FavouriteTvMovie.toString(),JobDesc: this.JobDesc.toString(),AcomplishedFeeling: this.AcomplishedFeeling.toString(),VibeWanted: this.VibeWanted.toString(), }
        ];
        const httpOptions = {
          headers: new HttpHeaders({'Content-Type': 'application/json'})
        }    
       this.http.post(this.baseURL + 'api/InsertCustomerProfile' , result[0] , httpOptions).subscribe(
        (response2 : headers[]) => {
          console.log(response2);
        }, (error) => {}
        
      )
      setTimeout(()=>this.UploadCustomerPhoto(files),1000);
    }
    
  }

  load_userID() {
    this.dataService.get_UserIdGuid().subscribe(response => {
      this.userID = response;
    }
    );
  }

  UploadCustomerPhoto(files){
    const formData = new FormData();
    for (let file of files)
      formData.append(file.name, file);

    const uploadReq = new HttpRequest('POST', this.baseURL + 'api/uploadPhoto/' + this.userID , formData, {
      reportProgress: true,
    });
    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
    });
    this.loading = false;  
    this.messageService.add({ key: 'myKey1', severity: 'success', summary: 'Sucesfully updated your profile data.', detail: '' });
    setTimeout(()=>this.messageService.clear("myKey1"),30000);
    this.isProfileSetup = true;
  }

  CheckCustomerProfile(){
    this.loading = true;  
    this.http.get(this.baseURL + 'api/CheckCustomerProfile/' + this.userID).subscribe(
        (response : any[]) => {
          this.loading = false;  
          if(response[0].message == "False"){
            this.isProfileSetup = false;
          } else {
            this.isProfileSetup = true;
          }
        }, (error) => {})
  }


  ExploreEvents(){
    this.router.navigate(['./events']);
  }
  ExploreGarments(){
    this.router.navigate(['./garments']);
  }
  ExploreWardrobe(){
    this.router.navigate(['./wardrobe']);
  }
  ExploreAdvise(){
    this.router.navigate(['./advise']);
  }

}
