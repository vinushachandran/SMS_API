import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Student } from '../../Model/Student/student';
import { baseURL } from '../../../common';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private baseUrl=baseURL.apiUrl;
  private apiUrl =`${this.baseUrl}Student/`;

  constructor(private http: HttpClient) { }

  private statusSource=new BehaviorSubject<string>('');
  currentStatus=this.statusSource.asObservable();

  activeFilterChange(status:string){
    this.statusSource.next(status);
    this.getAllStudents().subscribe();
    
  }

  getAllStudents(): Observable<{allStudentsList:Student[]}> {
    const status=this.statusSource.value;
    console.log(status);
    return this.http.get<{allStudentsList:Student[]}>(`${this.apiUrl}GetAllStudents?isActive=${status}`);
  }

  getStudentById(id: bigint): Observable<{studentDetail:Student}> {
    return this.http.get<{studentDetail:Student}>(`${this.apiUrl}GetOneStudent/${id}`);
  }

  addStudent(student: Student): Observable<any> {
    let params=new HttpParams()
    .set('StudentRegNo', student.studentRegNo)
    .set('FirstName', student.firstName)
    .set('MiddleName', student.middleName || '')
    .set('LastName', student.lastName)
    .set('DisplayName', student.displayName)
    .set('Email', student.email)
    .set('Gender', student.gender)
    .set('Address', student.address)
    .set('ContactNo', student.contactNo)
    .set('IsEnable', student.isEnable.toString());

    let dobString = '';
    if (student.dob) {
      const dob = new Date(student.dob); 
      dobString = isNaN(dob.getTime()) ? '' : dob.toISOString();
    }

    params = params.set('DOB', dobString);
    
    return this.http.post<any>(`${this.apiUrl}AddStudent`, null,{params});
    
  }

  deleteStudent(studentID: bigint): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}DeleteStudent/${studentID}`);
  }

  editStudent(student: Student,studentID:string|bigint): Observable<any> {
    let params=new HttpParams()
    .set('StudentID', BigInt(studentID).toString())
    .set('StudentRegNo', student.studentRegNo)
    .set('FirstName', student.firstName)
    .set('MiddleName', student.middleName || '')
    .set('LastName', student.lastName)
    .set('DisplayName', student.displayName)
    .set('Email', student.email)
    .set('Gender', student.gender)
    .set('Address', student.address)
    .set('ContactNo', student.contactNo)
    .set('IsEnable', student.isEnable.toString());

    let dobString = '';
    if (student.dob) {
      const dob = new Date(student.dob); 
      dobString = isNaN(dob.getTime()) ? '' : dob.toISOString();
    }

    params = params.set('DOB', dobString);
    
    return this.http.put<any>(`${this.apiUrl}UpdateStudent/`, null,{params});
    
  }
  
  
}
