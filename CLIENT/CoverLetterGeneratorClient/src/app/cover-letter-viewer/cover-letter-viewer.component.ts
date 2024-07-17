import { Component } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';

interface CoverLetter {
  title: string;
  jobTitle: string;
  jobDescription: string;
  content: string;
}

@Component({
  selector: 'app-cover-letter-viewer',
  standalone: true,
  imports: [CommonModule, NgFor],
  templateUrl: './cover-letter-viewer.component.html',
  styleUrls: ['./cover-letter-viewer.component.css']
})
export class CoverLetterViewerComponent {
  coverLetters: CoverLetter[] = [
    { title: 'Cover Letter 1', jobTitle: 'Job 1', jobDescription: 'Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 Description 1 ', content: 'Content 1' },
    { title: 'Cover Letter 2', jobTitle: 'Job 2', jobDescription: 'Description 2', content: 'Content 2' },
    // Add more cover letters here
  ];

  selectedCoverLetter: CoverLetter | null = this.coverLetters[0]; // Initialize with the first cover letter
  showFullJobDescription: boolean = false;

  selectCoverLetter(coverLetter: CoverLetter) {
    this.selectedCoverLetter = coverLetter;
    this.showFullJobDescription = false;
  }

  toggleJobDescription() {
    this.showFullJobDescription = !this.showFullJobDescription;
  }
}
