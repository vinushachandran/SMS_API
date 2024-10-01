import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Teacher } from '../../Model/Teacher/teacher';
import { baseURL } from '../../../common';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  private baseUrl=baseURL.apiUrl;
  private apiUrl =`${this.baseUrl}Teacher/`;

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

  pageFilterChage(NumOfRecords:number){
    this.pageSource.next(NumOfRecords);
  }



  getAllTeachers(pageNumber:number): Observable<{teachersList:Teacher[],totalPages:number}> {
    const status=this.statusSource.value;
    const numberOfRecoards=this.pageSource.value;
    return this.http.get<{teachersList:Teacher[],totalPages:number}>(`${this.apiUrl}GetAllTeachers?pageNumber=${pageNumber}&numberOfRecoards=${numberOfRecoards}&isActive=${status}`);
  }

  //get a teacher details by its id
  getTeacherById(id: bigint): Observable<{teacherDetail:Teacher}> {
    return this.http.get<{teacherDetail:Teacher}>(`${this.apiUrl}GetOneTeachers/${id}`);
  }

  //edit a teacher
  editTeacher(teacher: Teacher,teacherID:string|bigint): Observable<any> {
    let params=new HttpParams()
    .set('TeacherID', BigInt(teacherID).toString())
    .set('TeacherRegNo', teacher.teacherRegNo)
    .set('FirstName', teacher.firstName)
    .set('MiddleName', teacher.middleName || '')
    .set('LastName', teacher.lastName)
    .set('DisplayName', teacher.displayName)
    .set('Email', teacher.email)
    .set('Gender', teacher.gender)
    .set('Address', teacher.address)
    .set('ContactNo', teacher.contactNo)
    .set('IsEnable', teacher.isEnable.toString());

    let dobString = '';
    if (teacher.dob) {
      const dob = new Date(teacher.dob); 
      dobString = isNaN(dob.getTime()) ? '' : dob.toISOString();
    }

    params = params.set('DOB', dobString);
    
    return this.http.put<any>(`${this.apiUrl}UpdateTeacher/`, null,{params});
    
  }

  //add a new student
  addTeacher(teacher: Teacher): Observable<any> {
    let params=new HttpParams()
    .set('TeacherRegNo', teacher.teacherRegNo)
    .set('FirstName', teacher.firstName)
    .set('MiddleName', teacher.middleName || '')
    .set('LastName', teacher.lastName)
    .set('DisplayName', teacher.displayName)
    .set('Email', teacher.email)
    .set('Gender', teacher.gender)
    .set('Address', teacher.address)
    .set('ContactNo', teacher.contactNo)
    .set('IsEnable', teacher.isEnable.toString());

    let dobString = '';
    if (teacher.dob) {
      const dob = new Date(teacher.dob); 
      dobString = isNaN(dob.getTime()) ? '' : dob.toISOString();
    }

    params = params.set('DOB', dobString);
    
    return this.http.post<any>(`${this.apiUrl}AddTeacher`, null,{params});
  }

  //delete a student
  deleteTeacher(teacherID: bigint): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}DeleteTeachers/${teacherID}`);
  }

  //Toggle enable and disable
  toggleTeacherStatus(id: number, isEnable: boolean) {
    return this.http.put<any>(`${this.apiUrl}ToggleTeacherStatus?id=${id}&isEnable=${isEnable}`, {});
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
  searchTeacher():Observable<{data:Teacher[],totalPages:number}>{
    const searchTerm=this.termSource.value;
    const searchCatogory=this.catogorySource.value;

    return this.http.get<{data:Teacher[],totalPages:number}>((`${this.apiUrl}GetSearchTeachers?Criteria=${searchCatogory}&Term=${searchTerm}`))

  }
   
    
}

