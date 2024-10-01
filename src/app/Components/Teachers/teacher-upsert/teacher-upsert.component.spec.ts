import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherUpsertComponent } from './teacher-upsert.component';

describe('TeacherUpsertComponent', () => {
  let component: TeacherUpsertComponent;
  let fixture: ComponentFixture<TeacherUpsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TeacherUpsertComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeacherUpsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
