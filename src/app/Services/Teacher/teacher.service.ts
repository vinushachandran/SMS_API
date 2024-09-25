import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Teacher } from '../../Model/Teacher/teacher';
import { baseURL } from '../../../common';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  private baseUrl=baseURL.apiUrl;
  private apiUrl =`${this.baseUrl}Teacher/`;

  constructor(private http: HttpClient) { }

  getAllTeachers(): Observable<{teachersList:Teacher[]}> {
    return this.http.get<{teachersList:Teacher[]}>(`${this.apiUrl}GetAllTeachers`);
  }
}
