import { Routes } from '@angular/router';
import { StudentTableComponent } from './Components/Students/student-table/student-table.component';
import { MainComponent } from './Components/main/main.component';
import { SubjectTableComponent } from './Components/subject-table/subject-table.component';
import { TeacherTableComponent } from './Components/teacher-table/teacher-table.component';
import { StudentDetailComponent } from './Components/Students/student-detail/student-detail.component';
import { StudentAddComponent } from './Components/Students/student-add/student-add.component';
import { StudentMenuComponent } from './Components/Students/student-menu/student-menu.component';


export const routes: Routes = [
    { path: 'index', component: MainComponent },
    { path: '', redirectTo: '/index', pathMatch: 'full' },
    { path: 'students', component: StudentTableComponent },
    { path: 'subjects', component: SubjectTableComponent },
    { path: 'teachers', component: TeacherTableComponent },
    { path: 'students/:id', component: StudentDetailComponent },
    // { path: '', component: StudentMenuComponent },
    { path: 'student-add', component: StudentAddComponent },
    { path: 'student-add/:id', component: StudentAddComponent }
];
