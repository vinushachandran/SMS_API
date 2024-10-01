import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subject } from '../../../Model/Subject/subject';
import { SubjectService } from '../../../Services/Subject/subject.service';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-subject-upsert',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule,RouterOutlet],
  templateUrl: './subject-upsert.component.html',
  styleUrl: './subject-upsert.component.css'
})
export class SubjectUpsertComponent {
  subjectForm!: FormGroup ;
  errorMessage: string | null = null;
  exsistingSubject:Subject | null=null;


  constructor(
    private subjectService: SubjectService, 
    private router: Router,
    private fb:FormBuilder,
    private route: ActivatedRoute) { }

  //Inital loading
  ngOnInit():void{
    this.formValidation();
    const idString = this.route.snapshot.paramMap.get('id') || '0'; 
    const subjectID = BigInt(idString);
    // when subject id is selected it have to go to edit
    if (subjectID) {
      this.subjectService.getSubjectById(subjectID).subscribe(
        data => {
          console.log('Subject data fetched:', data);
          this.exsistingSubject = data.subjectDetail; 
          this.editSubject(this.exsistingSubject);

        },
        error => {
          console.error('Error fetching subject data', error);
        }
      );
    }

  }

   //UI form validation
   formValidation(){
    this.subjectForm = this.fb.group({
      subjectCode: ['', Validators.required],
      subjectName: ['', Validators.required],
      isEnable:[false]

    });
  }

  
  // Submit the add or edit form
  onSubmit() {
    if (this.subjectForm.valid) {
      const newsubject: Subject=this.subjectForm.value;
      console.log(newsubject);
      if(this.exsistingSubject){
        this.subjectService.editSubject(newsubject,this.exsistingSubject.subjectID).subscribe(
          (response:any)=> {
            Swal.fire({
              title: 'Success!',
              text: response.message,
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              this.router.navigate(['/subjects']);
            });
          },
          (error) => {
            Swal.fire({
              title: 'Warning!',
              text: error.error.message,
              icon: 'warning',
              confirmButtonText: 'OK'
            });
          }          
        );
      }
      else{
        this.subjectService.addSubject(newsubject).subscribe(
          (response:any)=> {
            Swal.fire({
              title: 'Success!',
              text: response.message,
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              this.router.navigate(['/subjects']);
            });
          },
          (error) => {
            Swal.fire({
              title: 'Warning!',
              text: error.error.message,
              icon: 'warning',
              confirmButtonText: 'OK'
            });
          }          
        );
      }      
    }
    else{
      this.subjectForm.markAllAsTouched();
    }
  }

  // edit student
  editSubject(subjectData:Subject){
      this.subjectForm.patchValue({
        subjectCode: subjectData?.subjectCode,
        subjectName: subjectData?.name,
        isEnable: subjectData?.isEnable
      });
    

  }

  // Go to back to the student table
  onBack() {
    this.router.navigate(['/subjects']);
  }

}
