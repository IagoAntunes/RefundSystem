import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

export interface DropdownOption {
    value: string | number;
    viewValue: string;
}

@Component({
    selector: 'app-dropdown',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent {
    @Input() label: string = '';
    @Input() controlName: string = '';
    @Input() formGroup!: FormGroup;
    @Input() options: DropdownOption[] = [];
    @Input() placeholder: string = 'Selecione uma opção';
}