import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        // Quando o usuário acessar a rota raiz '', redirecione para '/auth'
        path: '',
        pathMatch: 'full',
        redirectTo: 'auth'
    },
    {
        // Quando a rota for '/auth', carregue as rotas do arquivo de rotas de autenticação
        // O lazy loading é feito da mesma forma, mas apontando para o arquivo de rotas
        path: 'auth',
        loadChildren: () => import('./features/auth/auth.routes').then(r => r.AUTH_ROUTES)
    }
];
