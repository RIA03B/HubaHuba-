import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';


@Component({
  selector: 'app-career-dashboard',
  templateUrl: './career-dashboard.component.html',
  styleUrls: ['./career-dashboard.component.css']
})
export class CareerDashboardComponent {
  userData: any; // Placeholder for user data, replace with actual user data
  registrationForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.registrationForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      affiliation: [''],
      experience: [''],
      designSpecialization: [''],
      portfolio: [''],
      fashionEvents: [''],
      designInspirations: [''],
      specialRequirements: [''],
      heardAbout: [''],
      designAesthetic: [''],
      targetAudience: [''],
      designTechniques: [''],
      awardsRecognition: [''],
      challengesObstacles: [''],
      inspirationMotivation: [''],
      goalsMilestones: ['']
    }, { validator: this.passwordMatchValidator });
  }


  onSubmit() {
    if (this.registrationForm.valid) {
      // Handle form submission here (e.g., send data to backend)
      console.log('Form submitted successfully!');
      console.log(this.registrationForm.value);
    } else {
      // Mark all form fields as touched to display validation errors
      this.registrationForm.markAllAsTouched();
    }
  }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password');
    const confirmPassword = formGroup.get('confirmPassword');

    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
    } else {
      confirmPassword.setErrors(null);
    }
  }
}

