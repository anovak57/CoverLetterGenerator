import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-options',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './options.component.html',
  styleUrls: ['./options.component.css']
})
export class OptionsComponent {
  skillsExperience: string = '';
  instructions: string = '';

  saveAll() {
    console.log('Skills and Experience saved:', this.skillsExperience);
    console.log('Instructions saved:', this.instructions);
  }
}
