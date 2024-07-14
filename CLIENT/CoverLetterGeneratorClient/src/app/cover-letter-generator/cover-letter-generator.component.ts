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

  generateCoverLetter() {
    if (this.jobListing && this.experience) {
      this.coverLetter = `Dear Hiring Manager,\n\nI am writing to express my interest in the position you have posted for ${this.jobListing}. With my background in ${this.experience}, I am confident in my ability to contribute effectively to your team.\n\nI have a proven track record in ... (add more details as needed).\n\nThank you for considering my application.\n\nSincerely,\n[Your Name]`;
    } else {
      this.coverLetter = null;
    }
  }
}
