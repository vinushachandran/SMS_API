import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Subject } from '../../Model/Subject/subject';
import { baseURL } from '../../../common';

@Injectable({
  providedIn: 'root'
  })
  export class SubjectService {
  private baseUrl=baseURL.apiUrl;
  private apiUrl =`${this.baseUrl}Subject/`;

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
  getAllSubjects(pageNumber:number): Observable<{subjectList:Subject[],totalPages:number}> {
    const status=this.statusSource.value;
    const numberOfRecoards=this.pageSource.value;
    console.log(status);
    return this.http.get<{subjectList:Subject[],totalPages:number}>(`${this.apiUrl}GetAllSubjects?pageNumber=${pageNumber}&numberOfRecoards=${numberOfRecoards}&isActive=${status}`);
  }


  //get a student details by its id
  getSubjectById(id: bigint): Observable<{subjectDetail:Subject}> {
    return this.http.get<{subjectDetail:Subject}>(`${this.apiUrl}GetOneSubject/${id}`);
  }


  //add a new subject
  addSubject(subject: Subject): Observable<any> {
    let params=new HttpParams()
    .set('SubjectCode', subject.subjectCode)
    .set('Name', subject.name)
    .set('IsEnable', subject.isEnable.toString());
    
    return this.http.post<any>(`${this.apiUrl}AddSubject`, null,{params});
    
  }

  //delete a student
  deleteSubject(studentID: bigint): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}DeleteSubjects/${studentID}`);
  }

  //edit a student
  editSubject(subject: Subject,sujectID:string|bigint): Observable<any> {
    let params=new HttpParams()
    .set('SubjectID', BigInt(sujectID).toString())
    .set('SubjectCode', subject.subjectCode)
    .set('Name', subject.name)
    .set('IsEnable', subject.isEnable.toString());
    
    return this.http.put<any>(`${this.apiUrl}UpdateSubject/`, null,{params});
    
  }

  //Toggle enable and disable
  toggleSubjectStatus(id: number, isEnable: boolean) {
    return this.http.put<any>(`${this.apiUrl}ToggleSubjectStatus?id=${id}&isEnable=${isEnable}`, {});
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
  searchSubject():Observable<{data:Subject[],totalPages:number}>{
    const searchTerm=this.termSource.value;
    const searchCatogory=this.catogorySource.value;

    return this.http.get<{data:Subject[],totalPages:number}>((`${this.apiUrl}GetSearchSubjects?Criteria=${searchCatogory}&Term=${searchTerm}`))

  }



}
