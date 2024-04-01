import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { UserInfoService } from '../services/user-info.service';

@Component({
  selector: 'app-career-profile',
  templateUrl: './career-profile.component.html',
  styleUrls: ['./career-profile.component.css']
})
export class CareerProfileComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  isUserValid: boolean = false;
  userID: string;
  baseURL: string;

  careerProfile = {
    UserName: '',
    Email: '',
    Affiliation: '',
    Experience: '',
    DesignSpecialization: '',
    Portfolio: '',
    FashionEvents: '',
    DesignInspirations: '',
    SpecialRequirements: '',
    HeardAbout: '',
    DesignAesthetic: '',
    TargetAudience: '',
    DesignTechniques: '',
    AwardsRecognition: '',
    ChallengesObstacles: '',
    InspirationMotivation: '',
    GoalsMilestones: ''
  };

  loading: boolean = false;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private authorizeService: AuthorizeService,
    private dataService: UserInfoService,
    private http: HttpClient,
    private messageService: MessageService,
    private router: Router
  ) {
    this.baseURL = baseUrl;
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.loadUserID();
  }

  loadUserID() {
    this.dataService.get_UserIdGuid().subscribe(response => {
      this.userID = response;
      this.isUserValid = !!this.userID;
      if (!this.isUserValid) {
        this.router.navigate(['/home']);
      }
    });
  }

  saveCareerProfile() {
    this.loading = true;
    this.http.post(`${this.baseURL}api/UpdateCareerProfile`, this.careerProfile).subscribe(
      () => {
        this.loading = false;
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Career profile updated successfully.' });
        // Directly navigate to the career profile route
        this.router.navigate(['/career-profile']); // Adjust the navigation path here
      },
      error => {
        this.loading = false;
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update career profile.' });
      }
    );
  }
}
