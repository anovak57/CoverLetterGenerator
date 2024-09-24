import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequestDto } from '../models/login-request.dto';
import { RegisterRequestDto } from '../models/register-request.dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:5272/api/account';

  constructor(private http: HttpClient) { }

  register(registerDto: RegisterRequestDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, registerDto);
  }

  login(loginDto: LoginRequestDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, loginDto);
  }

  logout(): void {
    localStorage.removeItem('jwtToken');
  }

  storeToken(token: string): void {
    localStorage.setItem('jwtToken', token);
  }

  getToken(): string | null {
    return localStorage.getItem('jwtToken');
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token;
  }
}
