import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputComponent } from '../../../../shared/components/input/input.component';
import { ButtonComponent } from '../../../../shared/components/button/button.component';
import { AuthService } from '../../auth.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../../request/login.request';
import { RegisterRequest } from '../../request/register.request';
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

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

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
        const request: LoginRequest = {
          email: this.loginForm.value.email,
          password: this.loginForm.value.password
        };
        this.authService.login(request).subscribe({
          next: (response) => {
            console.log('Login bem-sucedido!', response);
            this.authService.setToken(response.data.token);
            this.router.navigate(['/dashboard']); 
          },
          error: (err) => console.error('Erro no login:', err)
        });
      }
    } else {
      if (this.registerForm.valid) {
        const request: RegisterRequest = {
          email: this.registerForm.value.email,
          username: this.registerForm.value.name, 
          password: this.registerForm.value.password,
          roles: ['APPLICANT'] 
        };
        this.authService.register(request).subscribe({
            next: (response) => {
                console.log('Cadastro bem-sucedido!', response);
                this.isLoginMode = true;
            },
            error: (err) => console.error('Erro no cadastro:', err)
        });
      }
    }
  }

}
