import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
  encapsulation: ViewEncapsulation.None
})
export class MainComponent {
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

}
