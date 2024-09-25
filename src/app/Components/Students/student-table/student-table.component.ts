import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { StudentService } from '../../../Services/Student/student.service';
import { Student } from '../../../Model/Student/student';
import { Router, RouterModule } from '@angular/router';
import { StudentMenuComponent } from "../student-menu/student-menu.component";
import { Clipboard } from '@angular/cdk/clipboard';
import * as bootstrap from 'bootstrap';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-student-table',
  standalone: true,
  imports: [CommonModule, RouterModule, StudentMenuComponent],
  templateUrl: './student-table.component.html',
  styleUrls: ['./student-table.component.css']
})
export class StudentTableComponent implements OnInit {
  students: Student[] = [];
  paginatedStudents: Student[] = [];
  activeStatus:string='';
  currentPage:number=1;
  itemPerPage:number=5;
  totalPages: number = 10;


  constructor(private studentService: StudentService, private clipboard: Clipboard,private router:Router) { }



  ngOnInit(): void {
     this.studentService.currentStatus.subscribe(
      (status)=>{
        this.activeStatus=status;
        this.loadStudents();
      }

     )
  }

  loadStudents(){
    this.studentService.getAllStudents().subscribe(
      data => {
        this.students = data.allStudentsList || [];
        this.updatePaginatedStudents();
      }

  );
  }

  updatePaginatedStudents(): void {
    this.totalPages = Math.ceil(this.students.length / this.itemPerPage);
    const startIndex = (this.currentPage - 1) * this.itemPerPage;
    const endIndex = startIndex + this.itemPerPage;
    this.paginatedStudents = this.students.slice(startIndex, endIndex);
  }

  goToNextPage(): void{
    if(this.currentPage*this.itemPerPage<this.students.length){
      this.currentPage++;
      this.updatePaginatedStudents();
    }
  }

  goToPreviousPage():void{
    if(this.currentPage>1){
      this.currentPage--;
      this.updatePaginatedStudents();
    }
  }

  // Copy the email and contact number and show the tooltip copied

  copyText(text: string, event: MouseEvent): void {
    this.clipboard.copy(text);

    const target = event.target as HTMLElement;
    const tooltip = bootstrap.Tooltip.getInstance(target) || new bootstrap.Tooltip(target);
    tooltip.setContent({ '.tooltip-inner': 'Copied!' });
    tooltip.show();

    setTimeout(() => {
      tooltip.setContent({ '.tooltip-inner': text });
      tooltip.hide();
    }, 1000); 
  }

  ngAfterViewInit(): void {
    const tooltipTriggerList = Array.from(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.forEach(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
  }

// Delete a student
deleteStudent(studentID: string | bigint): void {
  const studentIDBigInt = typeof studentID === 'string' ? BigInt(studentID) : studentID;

  Swal.fire({
    title: 'Are you sure?',
    text: 'You won’t be able to revert this!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, delete it!'
  }).then((result) => {
    if (result.isConfirmed) {
      this.studentService.deleteStudent(studentIDBigInt).subscribe(
        (response: any) => {
          // Show success message
          Swal.fire({
            text: response.message,
            icon: 'success',
            confirmButtonText: 'OK'
          }).then(() => {
            // Reload the student list after deletion
            this.loadStudents();
          });
        },
        (error) => {
          if (error.status == 500) {
            Swal.fire({
              text: error.error.totalMessages,
              icon: 'warning',
              confirmButtonText: 'OK'
            });
          } else {
            Swal.fire({
              text: error.message || 'An error occurred',
              icon: 'error',
              confirmButtonText: 'OK'
            });
          }
        }
      );
    }
  });
}

navigateToAddStudent(studentID: string | bigint){
  this.router.navigate(['/student-add/',studentID]); 
}


}
