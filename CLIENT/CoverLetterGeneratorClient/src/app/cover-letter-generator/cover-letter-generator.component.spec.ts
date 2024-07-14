import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoverLetterGeneratorComponent } from './cover-letter-generator.component';

describe('CoverLetterGeneratorComponent', () => {
  let component: CoverLetterGeneratorComponent;
  let fixture: ComponentFixture<CoverLetterGeneratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CoverLetterGeneratorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CoverLetterGeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
