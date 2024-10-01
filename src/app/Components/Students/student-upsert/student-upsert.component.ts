import { Component } from '@angular/core';
import { Student } from '../../../Model/Student/student';
import { StudentService } from '../../../Services/Student/student.service';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-student-add',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule,RouterOutlet],
  templateUrl: './student-upsert.component.html',
  styleUrl: './student-upsert.component.css'
})
export class StudentAddComponent {

  studentForm!: FormGroup ;
  errorMessage: string | null = null;
  exsistingStudent:Student | null=null;

  constructor(
    private studentService: StudentService, 
    private router: Router,
    private fb:FormBuilder,
    private route: ActivatedRoute) { }

  
  //Inital loading
  ngOnInit():void{
    this.formValidation();
    const idString = this.route.snapshot.paramMap.get('id') || '0'; 
    const studentID = BigInt(idString);
    // when student id is selected it have to go to edit
    if (studentID) {
      this.studentService.getStudentById(studentID).subscribe(
        data => {
          console.log('Student data fetched:', data);
          this.exsistingStudent = data.studentDetail; 
          this.editStudent(this.exsistingStudent);

        },
        error => {
          console.error('Error fetching student data', error);
        }
      );
    }

  }

  //UI form validation
  formValidation(){
    this.studentForm = this.fb.group({
      studentRegNo: ['', Validators.required],
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      displayName: ['',Validators.required],
      email: ['', [Validators.required, Validators.email]],
      gender: ['male',Validators.required],
      dob: [null, Validators.required],
      address: ['',Validators.required],
      contactNo: ['', Validators.required],
      isEnable:[false]

    });
  }

  
  // Submit the add or edit form
  onSubmit() {
    if (this.studentForm.valid) {
      const newstudent: Student=this.studentForm.value;
      console.log(newstudent);
      if(this.exsistingStudent){
        this.studentService.editStudent(newstudent,this.exsistingStudent.studentID).subscribe(
          (response:any)=> {
            Swal.fire({
              title: 'Success!',
              text: response.message,
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              this.router.navigate(['/students']);
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
        this.studentService.addStudent(newstudent).subscribe(
          (response:any)=> {
            Swal.fire({
              title: 'Success!',
              text: response.message,
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              this.router.navigate(['/students']);
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
      this.studentForm.markAllAsTouched();
    }
  }

  // edit student
  editStudent(studentData:Student){
    let formattedDob = studentData.dob
      ? new Date(studentData.dob).toISOString().split('T')[0]
      : '';

      this.studentForm.patchValue({
        studentRegNo: studentData?.studentRegNo,
        firstName: studentData?.firstName,
        middleName: studentData?.middleName,
        lastName: studentData?.lastName,
        displayName: studentData?.displayName,
        email: studentData?.email,
        gender: studentData?.gender,
        dob: formattedDob,
        address: studentData?.address,
        contactNo: studentData?.contactNo,
        isEnable: studentData?.isEnable
      });
    

  }

  // Go to back to the student table
  onBack() {
    this.router.navigate(['/students']);
  }
 

  

}
