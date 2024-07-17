import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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

  generateCoverLetter() {
    if (this.jobListing && this.experience) {
      this.coverLetter = `Dear Hiring Manager,\n\nI am writing to express my interest in the position you have posted for ${this.jobListing}. With my background in ${this.experience}, I am confident in my ability to contribute effectively to your team.\n\nI have a proven track record in ... (add more details as needed).\n\nThank you for considering my application.\n\nSincerely,\n[Your Name]`;
    } else {
      this.coverLetter = null;
    }
  }

  async copyToClipboard() {
    if (this.coverLetter) {
      try {
        await navigator.clipboard.writeText(this.coverLetter);
      } catch (err) {
        console.error('Failed to copy: ', err);
        alert('Failed to copy the text to clipboard');
      }
    }
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  saveCoverLetter() {
    if (this.coverLetterTitle && this.coverLetter) {
      // Implement save functionality here
      this.closeModal();
    } else {
      alert('Please enter a title for the cover letter');
    }
  }
}
