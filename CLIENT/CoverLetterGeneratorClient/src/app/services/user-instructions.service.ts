import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserInstructionsService {
  private baseUrl = 'http://localhost:5272/api/coverletter';

  constructor(private http: HttpClient) {}

  getUserInstructions(): Observable<any> {
    return this.http.get(`${this.baseUrl}/instructions`);
  }

  saveUserInstructions(instructions: string): Observable<any> {
    return this.http.put(`${this.baseUrl}/instructions/save`, { instructions });
  }
}
