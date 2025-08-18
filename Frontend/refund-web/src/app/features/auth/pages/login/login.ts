import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputComponent } from '../../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../../shared/components/button/button.component';
@Component({
  selector: 'app-login',
  standalone: true, 
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputComponent,
    ButtonComponent
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {
  isLoginMode = true;

  // Forms
  loginForm!: FormGroup;
  registerForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  // ngOnInit é o lugar perfeito para inicializar os formulários
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      // [valor inicial, [validadores]]
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });

    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
  }

  toggleMode() {
    this.isLoginMode = !this.isLoginMode;
  }

  onSubmit(): void {
    if (this.isLoginMode) {
      if (this.loginForm.valid) {
        console.log("Dados de Login:", this.loginForm.value);
        // Aqui você chamará seu serviço de autenticação
      }
    } else {
      if (this.registerForm.valid) {
        // Adicionar validação de senhas iguais aqui
        console.log("Dados de Cadastro:", this.registerForm.value);
        // Aqui você chamará seu serviço de registro
      }
    }
  }

}
