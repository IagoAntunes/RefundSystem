import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'auth',
        pathMatch: 'full'
    },
    {
        path: 'auth',
        loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
    },
    {
        path: 'dashboard', // Rota para a área logada
        loadChildren: () => import('./features/home/home.routes').then(m => m.HOME_ROUTES)
    }
];
