import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Student } from '../../../Model/Student/student';
import { ActivatedRoute, Router} from '@angular/router';
import { StudentService } from '../../../Services/Student/student.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './student-detail.component.html',
  styleUrl: './student-detail.component.css'
})
export class StudentDetailComponent implements OnInit{
  student: Student | null=null;

  constructor(
    private route: ActivatedRoute,
    private studentService: StudentService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) { } 
  
  ngOnInit(): void {
    const idString = this.route.snapshot.paramMap.get('id') || '0'; 
    const studentID = BigInt(idString);

    if (studentID) {
      this.studentService.getStudentById(studentID).subscribe(
        data => {
          console.log('Student data fetched:', data);
          this.student = data.studentDetail;
          this.cdr.detectChanges(); 
        },
        error => {
          console.error('Error fetching student data', error);
        }
      );
    }
  }

  goBack() {
    this.router.navigate(['/students']);
  }
}
