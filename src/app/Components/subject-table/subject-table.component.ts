import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Subject } from '../../Model/Subject/subject';
import { SubjectService } from '../../Services/Subject/subject.service';

@Component({
  selector: 'app-subject-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './subject-table.component.html',
  styleUrl: './subject-table.component.css'
})
export class SubjectTableComponent implements OnInit {
  subjects: Subject[] = [];

  constructor(private subjectService: SubjectService) { }

  ngOnInit(): void {
    this.subjectService.getAllSubjects().subscribe(
      data => {
        this.subjects = data.subjectList || [];
      },
      error=>{
        console.error('Error fetching student data', error);
      }

  );
  }
}