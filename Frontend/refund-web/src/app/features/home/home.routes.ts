// src/app/features/home/home.routes.ts
import { Routes } from '@angular/router';
import { authGuard } from '../auth/auth.guard';
import { HomeApplicant } from './applicant-home/home-applicant.component';
import { ManagerHomeComponent } from './manager-home/manager-home.component';
import { RefundDetailComponent } from './refund-detail/refund-detail.component';

export const HOME_ROUTES: Routes = [
  {
    path: 'my-refunds',
    component: HomeApplicant,
    canActivate: [authGuard]
  },
  {
    path: 'approvals',
    component: ManagerHomeComponent,
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo: 'my-refunds',
    pathMatch: 'full'
  },
  {
    path: 'approvals/:id', 
    component: RefundDetailComponent,
    canActivate: [authGuard]
  },
];