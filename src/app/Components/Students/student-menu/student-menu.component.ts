import { Component } from '@angular/core';
import { StudentService } from '../../../Services/Student/student.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-student-menu',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './student-menu.component.html',
  styleUrl: './student-menu.component.css'
})
export class StudentMenuComponent {
  activeStatus:string='';

  constructor(private studentService: StudentService, private router:Router) { }

  ActiveFilter(event:Event){
    const target=event.target as HTMLSelectElement;
    this.activeStatus=target.value;
    this.studentService.activeFilterChange(this.activeStatus);

  }

  navigateToAddStudent() {
    this.router.navigate(['/student-add']); 
  }

}
