import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

@Component({
    selector: 'app-input',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './input.component.html',
    styleUrls: ['./input.component.scss'],
})
export class InputComponent {
    @Input() label: string = '';
    @Input() type: string = 'text';
    @Input() placeholder: string = '';
    @Input() controlName: string = '';
    @Input() formGroup!: FormGroup;
}
