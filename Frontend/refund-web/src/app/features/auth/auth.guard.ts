import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

export const authGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    const isLoginPage = state.url.includes('/login');

    if (authService.isAuthenticated()) {
        // Se o usuário está logado e tenta acessar a página de login,
        // redireciona para o dashboard correto.
        if (isLoginPage) {
            authService.redirectUserBasedOnRole();
            return false;
        }
        // Se está logado e acessando outra página, permite.
        return true;
    } else {
        // Se não está logado e está tentando acessar a página de login, permite.
        if (isLoginPage) {
            return true;
        }
        // Se não está logado e tenta acessar qualquer outra página, redireciona para o login.
        router.navigate(['/login']);
        return false;
    }
};