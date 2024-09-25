import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation-bar',
  standalone: true,
  imports: [],
  templateUrl: './navigation-bar.component.html',
  styleUrl: './navigation-bar.component.css'
})
export class NavigationBarComponent {
  constructor(private router: Router) { }

  navigateToStudents(): void {
    this.router.navigate(['/students']);
  }

  navigateToTeachers(): void {
    this.router.navigate(['/teachers']);
  }

  navigateToSubjects(): void {
    this.router.navigate(['/subjects']);
  }

  navigateToMain(): void {
    this.router.navigate(['/index']);
  }

}
