import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Subject } from '../../../Model/Subject/subject';
import { SubjectService } from '../../../Services/Subject/subject.service';
import { SubjectMenuComponent } from "../subject-menu/subject-menu.component";
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-subject-table',
  standalone: true,
  imports: [CommonModule, SubjectMenuComponent,RouterModule,FormsModule],
  templateUrl: './subject-table.component.html',
  styleUrl: './subject-table.component.css'
})
export class SubjectTableComponent implements OnInit {
  subjects: Subject[] = [];
  paginatedStudents: Subject[] = [];
  activeStatus:string='';
  currentPage:number=1;
  totalPage: number = 1;
  searchTerm: string = '';
  seachCatogory:string='all';
  noDataFound:boolean=false;



  constructor(private subjectService: SubjectService, private router:Router) { }

  ngOnInit(): void {
    //change load student by active filter
    this.subjectService.currentStatus.subscribe(
      ()=>{  
        this.currentPage=1;     
        this.loadSubjects(this.currentPage);
      }
     )
     //change load student by page filter
     this.subjectService.currentPageSizeStatus.subscribe(
      ()=>{
        this.currentPage=1;        
        this.loadSubjects(this.currentPage);
      }
     )
  }

  //load the subject table
  loadSubjects(pageNumber:number){
    this.subjectService.getAllSubjects(pageNumber).subscribe(
      data => {
        this.subjects = data.subjectList || [];
        this.totalPage=data.totalPages;
        this.noDataFound=false;
      }

  );
  }

  optionChange(optionEvent:Event,searchTerm:HTMLInputElement){
    searchTerm.value='';//change the search term value as empty when change the category
    const catogory=optionEvent.target as HTMLSelectElement;
    this.seachCatogory=catogory.value;
    this.subjectService.searchCatogory(this.seachCatogory);
    this.loadSubjects(1);
    }

  termChange(typeEvent:Event) {
    const term=typeEvent.target as HTMLInputElement;
    this.searchTerm=term.value;
    if(this.searchTerm){
      this.subjectService.searchTerm(this.searchTerm);
      if(this.searchTerm.length>1){
        this.currentPage=1;
        this.searchSubjects()
      }
      else{
        this.currentPage=1;
        this.loadSubjects(this.currentPage);
      }
    }
  }

  //Search student
  searchSubjects(){
    this.subjectService.searchSubject().subscribe(
      data=>{
        this.subjects=data.data;
        this.totalPage=data.totalPages;
        this.noDataFound=false;
      },
      error=>{
        this.subjects=[];
        this.noDataFound=true;
      }
    )
  }

  goToPreviousPage(activePage:number): void{
    this.currentPage=activePage-1;
    this.loadSubjects(this.currentPage);
    }
  goToNextPage(activePage:number):void{
    this.currentPage=activePage+1;
    this.loadSubjects(this.currentPage);
  }

  deleteSubject(subjectID: string | bigint): void {
    const subjectIDBigInt = typeof subjectID === 'string' ? BigInt(subjectID) : subjectID;
  
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
        this.subjectService.deleteSubject(subjectIDBigInt).subscribe(
          (response: any) => {
            // Show success message
            Swal.fire({
              text: response.message,
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              // Reload the student list after deletion
              this.loadSubjects(this.currentPage);
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
    navigateToAddSubject(subjectID: string | bigint){
      this.router.navigate(['/subject-add/',subjectID]); 
    }
    toggleSubjectStatus(subject:any){
      const action = subject.isEnable ? 'disable' : 'enable';
      const confirmationMessage=`Are  you sure want to ${action} ${subject.displayName}`    
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
          this.subjectService.toggleSubjectStatus(subject.subjectID, !subject.isEnable).subscribe(
            (response: any) => {
              // Show success message
              Swal.fire({
                text: response.message,
                icon: 'success',
                confirmButtonText: 'OK'
              }).then(() => {
                // Reload the student list after deletion
                this.loadSubjects(this.currentPage);
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

    
   
    

  
}