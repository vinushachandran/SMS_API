import { Component } from '@angular/core';
import { TeacherService } from '../../../Services/Teacher/teacher.service'; 
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-teacher-menu', 
  standalone: true,
  imports: [RouterModule],
  templateUrl: './teacher-menu.component.html', 
  styleUrl: './teacher-menu.component.css'      
})
export class TeacherMenuComponent {
  activeStatus: string = '';
  activePageSize: number = 5;

  constructor(private teacherService: TeacherService, private router: Router) { } 

  // For the active filter
  ActiveFilter(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.activeStatus = target.value;
    this.teacherService.activeFilterChange(this.activeStatus); 
  }

  // For the page size filter
  pageFilter(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.activePageSize = +target.value;
    this.teacherService.pageFilterChage(this.activePageSize);
  }

  // Navigate to the add teacher form
  navigateToAddTeacher() { 
    this.router.navigate(['/teacher-add']);  
  }
}
