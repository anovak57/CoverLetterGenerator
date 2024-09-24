import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-cover-letter-generator',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cover-letter-generator.component.html',
  styleUrls: ['./cover-letter-generator.component.css']
})
export class CoverLetterGeneratorComponent {
  jobListing: string = '';
  experience: string = '';
  coverLetter: string | null = null;
  isModalOpen: boolean = false;
  coverLetterTitle: string = '';

  constructor(private http: HttpClient, private authService: AuthService) {}

  generateCoverLetter() {
    if (this.jobListing && this.experience) {
      const requestBody = { jobListing: this.jobListing, experience: this.experience };

      this.http.post<any>('http://localhost:5272/api/coverletter/generate', requestBody)
        .subscribe({
          next: (response) => {
            console.log(response);
            this.coverLetter = response.coverLetter;
          },
          error: (error) => {
            console.error('Error generating cover letter:', error);
          }
        });
    } else {
      alert('Please provide job listing and experience.');
    }
  }

  saveCoverLetter() {
    if (this.coverLetterTitle && this.coverLetter) {
      const coverLetterDto = {
        Title: this.coverLetterTitle,
        Letter: this.coverLetter,
        JobListing: this.jobListing
      };

      this.http.post('http://localhost:5272/api/coverletter/save', coverLetterDto)
        .subscribe({
          next: () => {
            this.resetForm();
            this.closeModal();
            alert('Cover letter saved successfully.');
          },
          error: (error) => {
            console.error('Error saving cover letter:', error);
            alert('Failed to save cover letter.');
          }
        });
    } else {
      alert('Please enter a title for the cover letter.');
    }
  }

  resetForm() {
    this.jobListing = '';
    this.experience = '';
    this.coverLetter = null;
    this.coverLetterTitle = '';
    this.isModalOpen = false;
  }

  async copyToClipboard() {
    if (this.coverLetter) {
      try {
        await navigator.clipboard.writeText(this.coverLetter);
        alert('Cover letter copied to clipboard.');
      } catch (err) {
        console.error('Failed to copy:', err);
        alert('Failed to copy the text to clipboard.');
      }
    }
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }
}
