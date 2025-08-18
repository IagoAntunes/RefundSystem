import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from './request/login.request';
import { RegisterRequest } from './request/register.request';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    // TODO: MOVE TO ENV
    private apiUrl = 'https://localhost:7102/api/Auth'; 
    private readonly TOKEN_KEY = 'auth_token';

    constructor(private http: HttpClient) { }

    login(credentials: LoginRequest): Observable<any> {
        return this.http.post(`${this.apiUrl}/login`, credentials);
    }

    register(userData: RegisterRequest): Observable<any> {
        return this.http.post(`${this.apiUrl}/register`, userData);
    }

    setToken(token: string): void {
        localStorage.setItem(this.TOKEN_KEY, token);
    }

    getToken(): string | null {
        return localStorage.getItem(this.TOKEN_KEY);
    }

    isLoggedIn(): boolean {
        return this.getToken() !== null;
    }

    logout(): void {
        localStorage.removeItem(this.TOKEN_KEY);
    }
}