import { Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { CoverLetterGeneratorComponent } from './cover-letter-generator/cover-letter-generator.component';

export const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'cover-letter', component: CoverLetterGeneratorComponent}
  // Add other routes here as needed
];
