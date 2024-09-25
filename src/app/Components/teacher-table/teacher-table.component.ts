import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Teacher } from '../../Model/Teacher/teacher';
import { TeacherService } from '../../Services/Teacher/teacher.service';

@Component({
  selector: 'app-teacher-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './teacher-table.component.html',
  styleUrl: './teacher-table.component.css'
})
export class TeacherTableComponent implements OnInit {
  teachers: Teacher[] = [];

  constructor(private teacherService: TeacherService) { }

  ngOnInit(): void {
    this.teacherService.getAllTeachers().subscribe(
      data => {
        console.log(data);
        this.teachers = data.teachersList || [];
      },
      error=>{
        console.error('Error fetching student data', error);
      }

  );
  }
}
