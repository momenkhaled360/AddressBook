import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { NotificationService } from '../../../../shared/services/notification-service'; // adjust path to match your project

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
})
export class Login {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private notify = inject(NotificationService);

  loading = false;
  serverError = '';

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });

  get email() {
    return this.form.controls.email;
  }

  get password() {
    return this.form.controls.password;
  }

  submit(): void {
    this.serverError = '';

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    this.authService
      .login({
        email: this.form.value.email ?? '',
        password: this.form.value.password ?? '',
      })
      .subscribe({
        next: () => {
          this.loading = false;
          this.notify.success('Logged in successfully');
          this.router.navigate(['/contacts']);
        },
        error: (err) => {
          this.loading = false;
          console.log(err);

          const message = err?.error?.message ?? 'Something went wrong. Please try again.';

          if (message.toLowerCase().includes('email') || message.toLowerCase().includes('password')) {
            this.password.setErrors({ server: message });
            this.password.markAsTouched();
          } else {
            this.serverError = message;
          }
        },
      });
  }
}