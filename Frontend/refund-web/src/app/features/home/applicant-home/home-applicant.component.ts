import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { map } from 'rxjs';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { DropdownComponent, DropdownOption } from '../../../shared/components/dropdown/dropdown.component';
import { FileUploadComponent } from '../../../shared/components/file-upload/file-upload.component';
import { AuthService } from '../../auth/auth.service';
import { CategoryService } from '../services/category.service';
import { RefundService } from '../services/refund.service';
import { Category } from '../../../interfaces/category';



@Component({
    selector: 'app-home',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        InputComponent,
        ButtonComponent,
        DropdownComponent,
        FileUploadComponent
    ],
    templateUrl: './home-applicant.component.html',
    styleUrls: ['./home-applicant.component.scss']
})
export class HomeApplicant {
    refundForm!: FormGroup;
    categories: DropdownOption[] = [];
    constructor(
        private authService: AuthService,
        private refundService: RefundService,
        private router: Router,
        private fb: FormBuilder,
        private categoryService: CategoryService
    ) {}

    ngOnInit(): void {
        this.refundForm = this.fb.group({
            title: ['', Validators.required],
            category: ['', Validators.required],
            amount: [null, [Validators.required, Validators.min(0.01)]],
            attachment: [null]
        });
        this.loadCategories();
    }

    loadCategories(): void {
        this.categoryService.getAllCategories().pipe(
            map(response => response.data.map((category: Category) => ({
                value: category.id,
                viewValue: category.name
            })))
            ).subscribe({
            next: (formattedCategories) => {
                this.categories = formattedCategories;
            },
            error: (err) => console.error('Erro ao carregar categorias:', err)
        });
    }

    onSubmit(): void {
        if (!this.refundForm.valid) {
            console.error('Formulário inválido!');
            return;
        }

        const formData = new FormData();
        formData.append('Name', this.refundForm.value.title);
        formData.append('CategoryId', this.refundForm.value.category);
        formData.append('Value', this.refundForm.value.amount.toString());
        formData.append('File', this.refundForm.value.attachment);
        formData.append('Status', '0');

        this.refundService.createRefund(formData).subscribe({
            next: (response) => {
                if (response.success) {
                    console.log('Reembolso criado com sucesso!', response.data);
                    alert('Reembolso solicitado com sucesso!');
                    this.refundForm.reset(); 
                } else {
                    console.error('Falha ao criar reembolso:', response);
                    alert('Ocorreu um erro ao solicitar o reembolso.');
                }
            },
            error: (err) => {
                console.error('Erro na requisição:', err);
                alert('Ocorreu um erro de comunicação com o servidor.');
            }
        });
    }

    logout(): void {
        this.authService.logout();
        this.router.navigate(['/auth']);
    }
}