import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectMenuComponent } from './subject-menu.component';

describe('SubjectMenuComponent', () => {
  let component: SubjectMenuComponent;
  let fixture: ComponentFixture<SubjectMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubjectMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubjectMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
