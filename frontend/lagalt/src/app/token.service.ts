import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  
  constructor(private decoder: JwtHelperService) {

  }

  public getToken(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      const isTokenExpired = this.decoder.isTokenExpired(token);
      if (!isTokenExpired) {
        return token;
      }
    }
    return null
  } 
}
