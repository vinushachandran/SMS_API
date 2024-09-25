import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from '../../Model/Subject/subject';
import { baseURL } from '../../../common';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  private baseUrl=baseURL.apiUrl;
  private apiUrl =`${this.baseUrl}Subject/`;

  constructor(private http: HttpClient) { }

  getAllSubjects(): Observable<{subjectList:Subject[]}> {
    return this.http.get<{subjectList:Subject[]}>(`${this.apiUrl}GetAllSubjects`);
  }
}
