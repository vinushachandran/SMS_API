import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SubjectService } from '../../../Services/Subject/subject.service';

@Component({
  selector: 'app-subject-menu',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './subject-menu.component.html',
  styleUrl: './subject-menu.component.css'
})
export class SubjectMenuComponent {
  activeStatus:string='';
  activePageSize:number=5;

  constructor(private subjectService: SubjectService, private router:Router) { }

  ngOnInit(){
    this.activePageSize=this.subjectService.pageSource.value;
    
  }
  //For the active filter
  ActiveFilter(event:Event){
    const target=event.target as HTMLSelectElement;
    this.activeStatus=target.value;
    this.subjectService.activeFilterChange(this.activeStatus);

  }
//For the feature filter
  pageFilter(event:Event){
    const target=event.target as HTMLSelectElement;
    this.activePageSize=+target.value;
    this.subjectService.pageFilterChange(this.activePageSize);

  }

  //go to the add student form
  navigateToAddSubject() {
    this.router.navigate(['/subject-add']); 
  }

}
