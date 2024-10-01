import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectUpsertComponent } from './subject-upsert.component';

describe('SubjectUpsertComponent', () => {
  let component: SubjectUpsertComponent;
  let fixture: ComponentFixture<SubjectUpsertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubjectUpsertComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubjectUpsertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
