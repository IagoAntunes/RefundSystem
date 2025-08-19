// src/app/features/home/manager-home/manager-home.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { RefundService } from '../services/refund.service';
import { Refund } from '../../../interfaces/refund';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CategoryService } from '../services/category.service';

@Component({
    selector: 'app-manager-home',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        InputComponent,      
        ButtonComponent
    ],
    templateUrl: './manager-home.component.html',
    styleUrls: ['./manager-home.component.scss']
})
export class ManagerHomeComponent implements OnInit {
    searchForm!: FormGroup;
    allRefunds: Refund[] = []; 
    paginatedRefunds: Refund[] = []; 

    constructor(
        private authService: AuthService,
        private refundService: RefundService,
        private router: Router,
        private fb: FormBuilder,
        private categoryService: CategoryService, 
        private sanitizer: DomSanitizer 
    ) {}


    currentPage = 1;
    itemsPerPage = 6;
    totalPages = 1;

    ngOnInit(): void {
        this.searchForm = this.fb.group({
            query: ['']
        });
        this.loadRefunds(); 
    }

    viewDetails(refund: Refund): void { 
        this.router.navigate(['/dashboard/approvals', refund.id], {
            state: { refundData: refund } 
        });
    }

    loadRefunds(): void {
        this.refundService.getRefunds().subscribe({
            next: (response) => {
                if (response.success) {
                    this.allRefunds = response.data;
                    this.totalPages = Math.ceil(this.allRefunds.length / this.itemsPerPage);
                    this.updatePaginatedRefunds();
                    console.log('Reembolsos carregados:', this.allRefunds);
                } else {
                    console.error('A API indicou uma falha ao carregar os reembolsos.');
                }
            },
            error: (err) => console.error('Erro ao carregar reembolsos:', err)
        });
    }

    updatePaginatedRefunds(): void {
        const startIndex = (this.currentPage - 1) * this.itemsPerPage;
        const endIndex = startIndex + this.itemsPerPage;
        this.paginatedRefunds = this.allRefunds.slice(startIndex, endIndex);
    }

    previousPage(): void {
        if (this.currentPage > 1) {
            this.currentPage--;
            this.updatePaginatedRefunds();
        }
    }

    nextPage(): void {
        if (this.currentPage < this.totalPages) {
            this.currentPage++;
            this.updatePaginatedRefunds();
        }
    }

    getRefundStatus(status: number): string {
        switch (status) {
            case 0: return 'Pendente';
            case 1: return 'Aprovado';
            case 2: return 'Recusado';
            default: return 'Desconhecido';
        }
    }

    getCategoryIcon(categoryName: any | null): SafeHtml {
        let iconSvg = `<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"></circle><line x1="12" y1="16" x2="12" y2="12"></line><line x1="12" y1="8" x2="12.01" y2="8"></line></svg>`; // Ícone padrão
        
        if (categoryName.name?.toLowerCase() === 'alimentação') {
            iconSvg = `<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M2 21h20M12 21a9 9 0 0 0 9-9H3a9 9 0 0 0 9 9Z"></path><path d="M12 2c-2.5 0-5 2.5-5 5v1h10V7c0-2.5-2.5-5-5-5Z"></path></svg>`;
        } else if (categoryName.name?.toLowerCase() === 'transporte') {
            iconSvg = `<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M19 17h2l.64-2.56a2 2 0 0 0-1.94-2.44H5.3a2 2 0 0 0-1.94 2.44L4 17h2"></path><path d="M14 17V9"></path><path d="M10 17V9"></path><path d="M5 17v-3.34a2 2 0 0 1 1.17-1.83l5-2.5a2 2 0 0 1 1.66 0l5 2.5A2 2 0 0 1 19 13.66V17"></path><path d="M5 17a2 2 0 0 0 2 2h10a2 2 0 0 0 2-2"></path></svg>`;
        }
        return this.sanitizer.bypassSecurityTrustHtml(iconSvg);
    }



    onSearch(): void {
        console.log('Pesquisando por:', this.searchForm.value.query);
    }

    logout(): void {
        this.authService.logout();
        this.router.navigate(['/auth/login']);
    }
}