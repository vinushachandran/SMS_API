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

  // Active filter 
  public statusSource=new BehaviorSubject<string>('');
  currentStatus=this.statusSource.asObservable();

  activeFilterChange(status:string){
    this.statusSource.next(status);    
  }

  //pagination filter
  public pageSource=new BehaviorSubject<number>(5);
  currentPageSizeStatus=this.pageSource.asObservable();

  pageFilterChange(NumOfRecords:number){
    this.pageSource.next(NumOfRecords);    
  }

  //get all the student list 
  getAllStudents(pageNumber:number): Observable<{allStudentsList:Student[],totalPages:number}> {
    const status=this.statusSource.value;
    const numberOfRecoards=this.pageSource.value;
    console.log(status);
    return this.http.get<{allStudentsList:Student[],totalPages:number}>(`${this.apiUrl}GetAllStudents?pageNumber=${pageNumber}&numberOfRecoards=${numberOfRecoards}&isActive=${status}`);
  }

  //get a student details by its id
  getStudentById(id: bigint): Observable<{studentDetail:Student}> {
    return this.http.get<{studentDetail:Student}>(`${this.apiUrl}GetOneStudent/${id}`);
  }

  //add a new student
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

  //delete a student
  deleteStudent(studentID: bigint): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}DeleteStudent/${studentID}`);
  }

  //edit a student
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

  //Toggle enable and disable
  toggleStudentStatus(id: number, isEnable: boolean) {
    return this.http.put<any>(`${this.apiUrl}TogleStudentStatus?id=${id}&isEnable=${isEnable}`, {});
  }

  //search catogory option
  private catogorySource=new BehaviorSubject<string>('');
  currentCatogory=this.catogorySource.asObservable();

  searchCatogory(catogory:string){
    this.catogorySource.next(catogory);
  }

  // search term
  private termSource=new BehaviorSubject<string>('');//It allows you to hold a current value and emit new values over time.
  currentTerm=this.termSource.asObservable();
  
  searchTerm(term:string){
    this.termSource.next(term);// This method allows you to update the value of the BehaviorSubject
  }
  
  //search
  searchStudent():Observable<{data:Student[],totalPages:number}>{
    const searchTerm=this.termSource.value;
    const searchCatogory=this.catogorySource.value;

    return this.http.get<{data:Student[],totalPages:number}>((`${this.apiUrl}GetSerachStudents?Criteria=${searchCatogory}&Term=${searchTerm}`))

  }


  
  
  
}
