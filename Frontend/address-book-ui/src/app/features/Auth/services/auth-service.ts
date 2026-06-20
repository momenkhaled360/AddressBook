import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginRequest } from '../models/login-request';
import { RegisterRequest } from '../models/register-request';
import { AuthResponse } from '../models/auth-response';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:7092/api/auth'; 

  private tokenKey = 'auth_token';

  isLoggedIn = signal<boolean>(this.hasToken());

  constructor(private http: HttpClient) {}

  login(dto: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, dto).pipe(
      tap((res) => this.setSession(res))
    );
  }

  register(dto: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, dto).pipe(
      tap((res) => this.setSession(res))
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.isLoggedIn.set(false);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private setSession(res: AuthResponse): void {
    localStorage.setItem(this.tokenKey, res.token);
    this.isLoggedIn.set(true);
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }
}