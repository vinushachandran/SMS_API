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
  activePageSize:number=5;

  constructor(private studentService: StudentService, private router:Router) { }

  ngOnInit(){
    this.activePageSize=this.studentService.pageSource.value;
    
  }
  //For the active filter
  ActiveFilter(event:Event){
    const target=event.target as HTMLSelectElement;
    this.activeStatus=target.value;
    this.studentService.activeFilterChange(this.activeStatus);

  }
//For the feature filter
  pageFilter(event:Event){
    const target=event.target as HTMLSelectElement;
    this.activePageSize=+target.value;
    this.studentService.pageFilterChange(this.activePageSize);

  }

  //go to the add student form
  navigateToAddStudent() {
    this.router.navigate(['/student-add']); 
  }

}
