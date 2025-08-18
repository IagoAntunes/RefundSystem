// src/app/features/home/home.routes.ts
import { Routes } from '@angular/router';
import { authGuard } from '../auth/auth.guard';
import { HomeApplicant } from './applicant-home/home-applicant.component';

export const HOME_ROUTES: Routes = [
  {
    // Rota para a home do solicitante (applicant)
    path: 'my-refunds',
    component: HomeApplicant,
    canActivate: [authGuard]
  },
  // TODO: Adicionar a rota para a home do aprovador (approver)
  // {
  //   path: 'approvals',
  //   component: ApproverHomeComponent,
  //   canActivate: [authGuard]
  // },
    {
    path: '',
    redirectTo: 'my-refunds', // Redireciona a rota vazia do dashboard
    pathMatch: 'full'
    }
];