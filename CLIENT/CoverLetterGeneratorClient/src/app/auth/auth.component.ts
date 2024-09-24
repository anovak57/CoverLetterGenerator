import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service'; // Import the AuthService
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'; 
import { Router } from '@angular/router'; // Import Router to redirect after login/register

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
  isSignUp = false;
  email: string = '';
  password: string = '';
  confirmPassword: string = '';

  constructor(private authService: AuthService, private router: Router) {} 

  togglePanel() {
    this.isSignUp = !this.isSignUp;
  }

  onSubmit() {
    if (this.isSignUp) {
      if (this.password !== this.confirmPassword) {
        alert('Passwords do not match!');
        return;
      }

      this.authService.register({ email: this.email, password: this.password, confirmPassword: this.confirmPassword })
        .subscribe({
          next: (response) => {
            console.log(response);
            const token = response.token;
            this.authService.storeToken(token);
            alert('Registration successful!');
            this.router.navigate(['/cover-letter']);
          },
          error: (error) => {
            console.error(error);
            alert('Registration failed!');
          }
        });

    } else {
      this.authService.login({ email: this.email, password: this.password })
        .subscribe({
          next: (response) => {
            console.log(response);
            const token = response.token;
            this.authService.storeToken(token);
            alert('Login successful!');
            this.router.navigate(['/cover-letter']);
          },
          error: (error) => {
            console.error(error);
            alert('Login failed!');
          }
        });
    }
  }
}
