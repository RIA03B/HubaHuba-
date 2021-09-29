import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  baseURL : any;
  constructor(@Inject('BASE_URL') baseUrl: string , private http: HttpClient ) { 
    this.baseURL = baseUrl;
  }
  get_UserIdGuid(): Observable<string> {
    return this.http
      .get<{useridguid}>(this.baseURL + "api/getLoggedInUser" )
      .pipe(
        map(({useridguid})=>{
          return useridguid;
          
        }, catchError(this.handleError)
        )
        )
  }
  handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
