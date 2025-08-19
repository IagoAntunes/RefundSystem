// src/app/features/home/refund-detail/refund-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Location } from '@angular/common';

import { RefundService } from '../services/refund.service';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { DropdownComponent, DropdownOption } from '../../../shared/components/dropdown/dropdown.component';
import { CategoryService } from '../services/category.service';
import { map } from 'rxjs';
import { Refund } from '../../../interfaces/refund';

@Component({
    selector: 'app-refund-detail',
    standalone: true,
    imports: [
    CommonModule,
    ReactiveFormsModule,
    InputComponent,
    ButtonComponent,
    DropdownComponent
    ],
    templateUrl: './refund-detail.component.html',
    styleUrls: ['./refund-detail.component.scss']
})
export class RefundDetailComponent implements OnInit {
    detailForm!: FormGroup;
    categories: DropdownOption[] = [];
    refundId: string | null = null;
    imageId: string | null = null;

    refund: Refund | null = null;

    constructor(
        private route: ActivatedRoute,
        private fb: FormBuilder,
        private refundService: RefundService,
        private categoryService: CategoryService,
        private location: Location,
        private router: Router 
    ) {
        const navigation = this.router.getCurrentNavigation();
        this.refund = navigation?.extras?.state?.['refundData'];
    }

    ngOnInit(): void {
        this.detailForm = this.fb.group({
            name: [{ value: '', disabled: true }],
            category: [{ value: '', disabled: true }],
            value: [{ value: null, disabled: true }]
        });

        this.loadCategories();

        if (this.refund) {
            console.log("Dados recebidos via state. Sem requisição extra!");
            this.populateForm(this.refund);
        } else {
            console.log("Dados não encontrados no state. Fazendo requisição à API.");
            this.refundId = this.route.snapshot.paramMap.get('id');
            if (this.refundId) {
                this.loadRefundDetails(this.refundId);
            }
        }
    }

    populateForm(refund: Refund): void {
        this.detailForm.patchValue({
            name: refund.name,
            category: refund.categoryId,
            value: refund.value
        });
        this.imageId = refund.imageId;
    }

    loadRefundDetails(id: string): void {
        this.refundService.getRefundById(id).subscribe(response => {
            if (response.success) {
                const refund = response.data;
                this.imageId = refund.imageId;
                this.detailForm.patchValue({
                    name: refund.name,
                    category: refund.categoryId,
                    value: refund.value
                });
            }
        });
    }

    openReceipt(): void {
        if (!this.imageId) {
            alert('Este reembolso não possui um comprovante anexado.');
            return;
        }

        this.refundService.getReceiptImage(this.imageId).subscribe({
            next: (blob) => {
                const fileURL = URL.createObjectURL(blob);
                window.open(fileURL, '_blank');
            },
            error: (err) => {
                console.error('Erro ao buscar o comprovante:', err);
                alert('Não foi possível carregar o comprovante.');
            }
        });
    }

    goBack(): void {
        this.location.back(); 
    }

    loadCategories(): void {
        this.categoryService.getAllCategories().pipe(
            map(response => response.data.map(category => ({
            value: category.id,
            viewValue: category.name
            })))
        ).subscribe(formattedCategories => {
            this.categories = formattedCategories;
        });
    }

}