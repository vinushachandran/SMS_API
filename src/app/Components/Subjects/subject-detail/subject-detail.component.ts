import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SubjectService } from '../../../Services/Subject/subject.service';
import { Subject } from '../../../Model/Subject/subject';

@Component({
  selector: 'app-subject-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './subject-detail.component.html',
  styleUrl: './subject-detail.component.css'
})
export class SubjectDetailComponent implements OnInit {
  subjects: Subject | null=null;

  constructor(
    private route: ActivatedRoute,
    private subjectService: SubjectService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) { } 
  
  ngOnInit(): void {
    const idString = this.route.snapshot.paramMap.get('id') || '0'; 
    const subjectID = BigInt(idString);

    if (subjectID) {
      this.subjectService.getSubjectById(subjectID).subscribe(
        data => {
          console.log('Subject data fetched:', data);
          this.subjects = data.subjectDetail;
          this.cdr.detectChanges(); 
        },
        error => {
          console.error('Error fetching subject data', error);
        }
      );
    }
  }

  goBack() {
    this.router.navigate(['/subjects']);
  }

}
