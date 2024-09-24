import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface CoverLetter {
  title: string;
  jobListing: string;
  letter: string;
}

@Injectable({
  providedIn: 'root'
})
export class CoverLetterService {
  private apiUrl = 'http://localhost:5272/api/coverletter';

  constructor(private http: HttpClient) { }

  getCoverLetters(): Observable<CoverLetter[]> {
    return this.http.get<CoverLetter[]>(this.apiUrl);
  }

  getCoverLetterById(id: number): Observable<CoverLetter> {
    return this.http.get<CoverLetter>(`${this.apiUrl}/${id}`);
  }
}
