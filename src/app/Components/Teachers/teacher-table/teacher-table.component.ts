import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Teacher } from '../../../Model/Teacher/teacher';
import { TeacherService } from '../../../Services/Teacher/teacher.service';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TeacherMenuComponent } from "../teacher-menu/teacher-menu.component";
import bootstrap from 'bootstrap';
import { Clipboard } from '@angular/cdk/clipboard';
import Swal from 'sweetalert2';


@Component({
selector: 'app-teacher-table',
standalone: true,
imports: [CommonModule, RouterModule, FormsModule, TeacherMenuComponent],
templateUrl: './teacher-table.component.html',
styleUrl: './teacher-table.component.css'
})
export class TeacherTableComponent implements OnInit {
teachers: Teacher[] = [];
paginatedTeacher: Teacher[] = [];
activeStatus:string='';
currentPage:number=1;
totalPage: number = 1;
searchTerm: string = '';
seachCatogory:string='all';
noDataFound:boolean=false;

constructor(private teacherService: TeacherService, private clipboard: Clipboard,private router:Router) { }

ngOnInit(): void {
  this.teacherService.currentStatus.subscribe(
    ()=>{  
      this.currentPage=1;     
      this.loadTeachers(this.currentPage);
    }
    )
    //change load student by page filter
    this.teacherService.currentPageSizeStatus.subscribe(
    ()=>{
      this.currentPage=1;        
      this.loadTeachers(this.currentPage);
    } 

);
}

loadTeachers(pageNumber:number){
  this.teacherService.getAllTeachers(pageNumber).subscribe(
    data=>{
      this.teachers=data.teachersList||[];
      this.totalPage=data.totalPages;
      this.noDataFound=false;
    }
  )
}

//Go to the next page
goToNextPage(activePage:number): void{
  this.currentPage=activePage+1;
  this.loadTeachers(this.currentPage);
}

//Go to the previous page
goToPreviousPage(activePage:number):void{
  this.currentPage=activePage-1;
  this.loadTeachers(this.currentPage);
}

copyText(text: string, event: MouseEvent): void {
  
}

optionChange(optionEvent:Event,searchTerm:HTMLInputElement){
  searchTerm.value='';//change the search term value as empty when change the category
    const catogory=optionEvent.target as HTMLSelectElement;
    this.seachCatogory=catogory.value;
    this.teacherService.searchCatogory(this.seachCatogory);
    this.loadTeachers(1);
}

termChange(typeEvent:Event) {
  const term=typeEvent.target as HTMLInputElement;
    this.searchTerm=term.value;
    if(this.searchTerm){
      this.teacherService.searchTerm(this.searchTerm);
      if(this.searchTerm.length>1){
        this.currentPage=1;
        this.searchTeacher()
      }
      else{
        this.currentPage=1;
        this.loadTeachers(this.currentPage);
      }
    }
}

//Search teacher
searchTeacher(){
  this.teacherService.searchTeacher().subscribe(
    data=>{
      this.teachers=data.data;
      this.totalPage=data.totalPages;
      this.noDataFound=false;
    },
    error=>{
      this.teachers=[];
      this.noDataFound=true;
    }
  )
}



//toggle the active state
toggleTeacherStatus(teacher:any){
  const action = teacher.isEnable ? 'disable' : 'enable';
  const confirmationMessage = `Are you sure you want to ${action} ${teacher.displayName}?`;

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
      this.teacherService.toggleTeacherStatus(teacher.teacherID, !teacher.isEnable).subscribe(
        (response: any) => {
          // Show success message
          Swal.fire({
            text: response.message,
            icon: 'success',
            confirmButtonText: 'OK'
          }).then(() => {
            // Reload the student list after deletion
            this.loadTeachers(this.currentPage);
          });
        },
        (error)=>{
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

navigateToAddTeacher(teacherID: string | bigint){
  this.router.navigate(['/teacher-add',teacherID]);
}

deleteTeacher(teacherID: string | bigint): void {
  const teacherIDBigInt = typeof teacherID === 'string' ? BigInt(teacherID) : teacherID;
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
      this.teacherService.deleteTeacher(teacherIDBigInt).subscribe(
        (response: any) => {
          // Show success message
          Swal.fire({
            text: response.message,
            icon: 'success',
            confirmButtonText: 'OK'
          }).then(() => {
            // Reload the student list after deletion
            this.loadTeachers(this.currentPage);
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


}
