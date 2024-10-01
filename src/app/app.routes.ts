import { Routes } from '@angular/router';
import { StudentTableComponent } from './Components/Students/student-table/student-table.component';
import { MainComponent } from './Components/main/main.component';
import { SubjectTableComponent } from './Components/Subjects/subject-table/subject-table.component';
import { TeacherTableComponent } from './Components/Teachers/teacher-table/teacher-table.component';
import { StudentDetailComponent } from './Components/Students/student-detail/student-detail.component';
import { StudentAddComponent } from './Components/Students/student-upsert/student-upsert.component';
import { StudentMenuComponent } from './Components/Students/student-menu/student-menu.component';
import { TeacherAddComponent } from './Components/Teachers/teacher-upsert/teacher-upsert.component';
import { TeacherDetailComponent } from './Components/Teachers/teacher-detail/teacher-detail.component';
import { SubjectDetailComponent } from './Components/Subjects/subject-detail/subject-detail.component';
import { SubjectUpsertComponent } from './Components/Subjects/subject-upsert/subject-upsert.component';


export const routes: Routes = [
    { path: 'index', component: MainComponent },
    { path: '', redirectTo: '/index', pathMatch: 'full' },

    //student module
    { path: 'students', component: StudentTableComponent },    
    { path: 'students/:id', component: StudentDetailComponent },
    { path: 'student-add', component: StudentAddComponent },
    { path: 'student-add/:id', component: StudentAddComponent },
    // Teacher module
    { path: 'teachers', component: TeacherTableComponent },
    { path: 'teacher-add', component: TeacherAddComponent },
    { path: 'teacher-add/:id', component: TeacherAddComponent },
    { path: 'teachers/:id', component: TeacherDetailComponent },

    //Subject module
    { path: 'subjects', component: SubjectTableComponent },
    { path: 'subjects/:id', component: SubjectDetailComponent },
    { path: 'subject-add', component: SubjectUpsertComponent },
    { path: 'subject-add/:id', component: SubjectUpsertComponent },
    


];
