import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentService } from '../../../Services/Student/student.service';
import { Student } from '../../../Model/Student/student';
import { Router, RouterModule } from '@angular/router';
import { StudentMenuComponent } from "../student-menu/student-menu.component";
import { Clipboard } from '@angular/cdk/clipboard';
import * as bootstrap from 'bootstrap';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-student-table',
  standalone: true,
  imports: [CommonModule, RouterModule, StudentMenuComponent,FormsModule],
  templateUrl: './student-table.component.html',
  styleUrls: ['./student-table.component.css']
})
export class StudentTableComponent implements OnInit {
  students: Student[] = [];
  paginatedStudents: Student[] = [];
  activeStatus:string='';
  currentPage:number=1;
  totalPage: number = 1;
  searchTerm: string = '';
  seachCatogory:string='all';
  noDataFound:boolean=false;


  constructor(private studentService: StudentService, private clipboard: Clipboard,private router:Router) { }



  ngOnInit(): void {
     //change load student by active filter
     this.studentService.currentStatus.subscribe(
      ()=>{  
        this.currentPage=1;     
        this.loadStudents(this.currentPage);
      }
     )
     //change load student by page filter
     this.studentService.currentPageSizeStatus.subscribe(
      ()=>{
        this.currentPage=1;        
        this.loadStudents(this.currentPage);
      }
     )
  }

  //load the student table
  loadStudents(pageNumber:number){
    this.studentService.getAllStudents(pageNumber).subscribe(
      data => {
        this.students = data.allStudentsList || [];
        this.totalPage=data.totalPages;
        this.noDataFound=false;
      }

  );
  }
  //change the search catogory
  optionChange(optionEvent:Event,searchTerm:HTMLInputElement){
    searchTerm.value='';//change the search term value as empty when change the category
    const catogory=optionEvent.target as HTMLSelectElement;
    this.seachCatogory=catogory.value;
    this.studentService.searchCatogory(this.seachCatogory);
    this.loadStudents(1);
    

  }
  //Changing the  term of the search
  termChange(typeEvent:Event) {
    const term=typeEvent.target as HTMLInputElement;
    this.searchTerm=term.value;
    if(this.searchTerm){
      this.studentService.searchTerm(this.searchTerm);
      if(this.searchTerm.length>1){
        this.currentPage=1;
        this.searchStudent()
      }
      else{
        this.currentPage=1;
        this.loadStudents(this.currentPage);
      }
    }
  }
  //Search student
  searchStudent(){
    this.studentService.searchStudent().subscribe(
      data=>{
        this.students=data.data;
        this.totalPage=data.totalPages;
        this.noDataFound=false;
      },
      error=>{
        this.students=[];
        this.noDataFound=true;
      }
    )
  }

  //Go to next page
  goToNextPage(activePage:number): void{
    this.currentPage=activePage+1;
    this.loadStudents(this.currentPage);
    
  }

  //Go to the previous page
  goToPreviousPage(activePage:number):void{
    this.currentPage=activePage-1;
    this.loadStudents(this.currentPage);
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
            this.loadStudents(this.currentPage);
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


//toggle the active state
toggleStudentStatus(student: any) {
  const action = student.isEnable ? 'disable' : 'enable';
  const confirmationMessage = `Are you sure you want to ${action} ${student.displayName}?`;

  Swal.fire({
    title: 'Confirmation',
    text: confirmationMessage,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Yes',
    cancelButtonText: 'No'
  }).then((result) => {
    if (result.isConfirmed) {
      // Call the API to toggle status
      this.studentService.toggleStudentStatus(student.studentID, !student.isEnable).subscribe(response => {
        // Handle successful response
        if (response.success) {
          student.isEnable = !student.isEnable; 
          Swal.fire({
            title: 'Success!',
            text: response.message,
            icon: 'success'
          });
        } else {
          Swal.fire({
            title: 'Error!',
            text: response.message,
            icon: 'error'
          });
        }
      }, error => {
        Swal.fire({
          title: 'Error!',
          text: 'An error occurred while toggling status.',
          icon: 'error'
        });
      });
    }
  });
}



}
