import { Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { CoverLetterGeneratorComponent } from './cover-letter-generator/cover-letter-generator.component';
import { CoverLetterViewerComponent } from './cover-letter-viewer/cover-letter-viewer.component';
import { OptionsComponent } from './options/options.component';

export const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'cover-letter', component: CoverLetterGeneratorComponent},
  { path: 'user/cover-letters', component: CoverLetterViewerComponent},
  { path: 'user/options', component: OptionsComponent }
];
