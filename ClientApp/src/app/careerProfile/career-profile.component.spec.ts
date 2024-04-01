import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CareerProfileComponent } from './career-profile.component';

describe('CareerProfileComponent', () => {
  let component: CareerProfileComponent;
  let fixture: ComponentFixture<CareerProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CareerProfileComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
