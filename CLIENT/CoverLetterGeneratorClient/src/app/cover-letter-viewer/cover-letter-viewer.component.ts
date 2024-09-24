import { Component, OnInit } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { CoverLetterService } from '../services/cover-letter.service';

interface CoverLetter {
  title: string;
  letter: string;
  jobListing: string;
}

@Component({
  selector: 'app-cover-letter-viewer',
  standalone: true,
  imports: [CommonModule, NgFor],
  templateUrl: './cover-letter-viewer.component.html',
  styleUrls: ['./cover-letter-viewer.component.css']
})
export class CoverLetterViewerComponent implements OnInit {
  coverLetters: CoverLetter[] = [];
  selectedCoverLetter: CoverLetter | null = null;
  showFullJobDescription: boolean = false;

  constructor(private coverLetterService: CoverLetterService) {}

  ngOnInit() {
    this.loadCoverLetters();
  }

  loadCoverLetters() {
    this.coverLetterService.getCoverLetters().subscribe({
      next: (data) => {
        this.coverLetters = data;
        console.log(this.coverLetters)
        this.selectedCoverLetter = this.coverLetters.length > 0 ? this.coverLetters[0] : null;
      },
      error: (err) => {
        console.error('Error fetching cover letters:', err);
      }
    });
  }

  selectCoverLetter(coverLetter: CoverLetter) {
    this.selectedCoverLetter = coverLetter;
    this.showFullJobDescription = false;
  }

  toggleJobDescription() {
    this.showFullJobDescription = !this.showFullJobDescription;
  }
}
