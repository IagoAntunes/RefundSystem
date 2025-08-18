import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from './request/login.request';
import { RegisterRequest } from './request/register.request';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';


interface AuthResponse {
    data: {
        token: string;
    };
}
interface DecodedToken {
    exp: number;
    'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string | string[];
}


@Injectable({
    providedIn: 'root'
})
export class AuthService {
    // TODO: MOVE TO ENV
    private apiUrl = 'https://localhost:7102/api/Auth'; 
    private readonly TOKEN_KEY = 'auth_token';

    constructor(private http: HttpClient, private router: Router) { }

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

    isAuthenticated(): boolean {
        const token = this.getToken();
        if (!token) {
            return false;
        }

        try {
            const decodedToken = jwtDecode<DecodedToken>(token);
            const expirationDate = new Date(0);
            expirationDate.setUTCSeconds(decodedToken.exp);
            return expirationDate > new Date();
        } catch (error) {
            return false;
        }
    }

    getUserRoles(): string[] | null {
    const token = this.getToken();
    if (!token) {
        return null;
    }

    try {
        const decodedToken = jwtDecode<DecodedToken>(token);
        const roles = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        if (Array.isArray(roles)) {
        return roles;
        }
        if (typeof roles === 'string') {
        return [roles];
        }
        return null;
    } catch (error) {
        return null;
    }
    }

    redirectUserBasedOnRole(): void {
        const roles = this.getUserRoles();
        if (roles) {
            if (roles.includes('Admin') || roles.includes('Approver')) {
                this.router.navigate(['/dashboard/approvals']);
            } else if (roles.includes('Applicant')) {
                this.router.navigate(['/dashboard/my-refunds']);
            } else {
                this.logout(); // Role desconhecida, desloga por segurança
            }
        } else {
            this.logout(); // Sem roles, desloga
        }
    }

}