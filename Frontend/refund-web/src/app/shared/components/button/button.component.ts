import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-button',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './button.component.html',
    styleUrls: ['./button.component.scss']
    })
    export class ButtonComponent {
    @Input() type: 'button' | 'submit' | 'reset' = 'button';
    @Input() label: string = '';
    @Input() disabled: boolean = false;
    @Input() variant: 'primary' | 'secondary' | 'link' = 'primary';

    @Output() buttonClick = new EventEmitter<MouseEvent>();

    onClick(event: MouseEvent) {
        if (!this.disabled) {
            this.buttonClick.emit(event);
        }
    }
}