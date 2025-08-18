import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-file-upload',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './file-upload.component.html',
    styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent {
    @Input() label: string = '';
    @Input() controlName: string = '';
    @Input() formGroup!: FormGroup;
    @Input() placeholder: string = 'Selecione um arquivo';

    fileName: string | null = null;

    onFileSelected(event: Event): void {
        const element = event.target as HTMLInputElement;
        const file = element.files ? element.files[0] : null;

        if (file) {
            this.fileName = file.name;
            this.formGroup.patchValue({ [this.controlName]: file });
            this.formGroup.get(this.controlName)?.updateValueAndValidity();
        }
    }
}