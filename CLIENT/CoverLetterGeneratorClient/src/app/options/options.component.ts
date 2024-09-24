import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserInstructionsService } from '../services/user-instructions.service';

@Component({
  selector: 'app-options',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.css'],
})
export class OptionsComponent implements OnInit {
  skillsExperience: string = '';
  instructions: string = '';

  constructor(private userInstructionsService: UserInstructionsService) {}

  ngOnInit(): void {
    this.userInstructionsService.getUserInstructions().subscribe(
      (response) => {
        this.instructions = response?.instructions || '';
      },
      (error) => {
        console.error('Error loading instructions:', error);
      }
    );
  }

  saveAll() {
    this.userInstructionsService.saveUserInstructions(this.instructions).subscribe(
      () => {
        console.log('Instructions saved successfully');
      },
      (error) => {
        console.error('Error saving instructions:', error);
      }
    );
  }
}
