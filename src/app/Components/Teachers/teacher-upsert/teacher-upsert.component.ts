import { Component } from '@angular/core';
import { Teacher } from '../../../Model/Teacher/teacher';
import { TeacherService } from '../../../Services/Teacher/teacher.service';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-teacher-add',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, RouterOutlet],
  templateUrl: './teacher-upsert.component.html',
  styleUrl: './teacher-upsert.component.css'
})
export class TeacherAddComponent {

  teacherForm!: FormGroup;
  errorMessage: string | null = null;
  exsistingTeacher: Teacher | null = null;

  constructor(
    private teacherService: TeacherService, 
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute) { }

  // Initial loading
  ngOnInit(): void {
    this.formValidation();
    const idString = this.route.snapshot.paramMap.get('id') || '0'; 
    const teacherID = BigInt(idString);

    // When teacher id is selected it goes to edit
    if (teacherID) {
      this.teacherService.getTeacherById(teacherID).subscribe(
        data => {
          console.log('Teacher data fetched:', data);
          this.exsistingTeacher = data.teacherDetail; 
          this.editTeacher(this.exsistingTeacher);
        },
        error => {
          console.error('Error fetching teacher data', error);
        }
      );
    }
  }

  // UI form validation
  formValidation() {
    this.teacherForm = this.fb.group({
      teacherRegNo: ['', Validators.required],
      firstName: ['', Validators.required],
      middleName: [''],
      lastName: ['', Validators.required],
      displayName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      gender: ['male', Validators.required],
      dob: [null, Validators.required],
      address: ['', Validators.required],
      contactNo: ['', Validators.required],
      isEnable: [false]
    });
  }

  // Submit the add or edit form
  onSubmit() {
    if (this.teacherForm.valid) {
      const newTeacher: Teacher = this.teacherForm.value;
     
      if (this.exsistingTeacher) {
        this.teacherService.editTeacher(newTeacher, this.exsistingTeacher.teacherID).subscribe(
          (response: any) => {
            Swal.fire({
              title: 'Success!',
              text: response.message,
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              this.router.navigate(['/teachers']);
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
      } else {
        console.log(newTeacher);
        this.teacherService.addTeacher(newTeacher).subscribe(
          (response: any) => {
            Swal.fire({
              title: 'Success!',
              text: response.message,
              icon: 'success',
              confirmButtonText: 'OK'
            }).then(() => {
              this.router.navigate(['/teachers']);
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
    } else {
      this.teacherForm.markAllAsTouched();
    }
  }

  // Edit teacher
  editTeacher(teacherData: Teacher) {
    let formattedDob = teacherData.dob
      ? new Date(teacherData.dob).toISOString().split('T')[0]
      : '';

    this.teacherForm.patchValue({
      teacherRegNo: teacherData?.teacherRegNo,
      firstName: teacherData?.firstName,
      middleName: teacherData?.middleName,
      lastName: teacherData?.lastName,
      displayName: teacherData?.displayName,
      email: teacherData?.email,
      gender: teacherData?.gender,
      dob: formattedDob,
      address: teacherData?.address,
      contactNo: teacherData?.contactNo,
      isEnable: teacherData?.isEnable
    });
  }

  // Go back to the teacher table
  onBack() {
    this.router.navigate(['/teachers']);
  }
}
