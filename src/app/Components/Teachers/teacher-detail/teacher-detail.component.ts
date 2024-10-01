import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Teacher } from '../../../Model/Teacher/teacher';
import { ActivatedRoute, Router } from '@angular/router';
import { TeacherService } from '../../../Services/Teacher/teacher.service';

@Component({
  selector: 'app-teacher-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './teacher-detail.component.html',
  styleUrl: './teacher-detail.component.css'
})
export class TeacherDetailComponent implements OnInit{
  teacher: Teacher | null=null;

  constructor(
    private route: ActivatedRoute,
    private teacherService: TeacherService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) { } 

  ngOnInit(): void {
    const idString = this.route.snapshot.paramMap.get('id') || '0'; 
    const teacherID = BigInt(idString);

    if (teacherID) {
      this.teacherService.getTeacherById(teacherID).subscribe(
        data => {
          console.log('Teacher data fetched:', data);
          this.teacher = data.teacherDetail;
          this.cdr.detectChanges(); 
        },
        error => {
          console.error('Error fetching teacher data', error);
        }
      );
    }
  }

  goBack() {
    this.router.navigate(['/teachers']);
  }

}
