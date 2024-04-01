import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CareerDashboardComponent } from './career-dashboard.component';

describe('CareerDashboardComponent', () => {
  let component: CareerDashboardComponent;
  let fixture: ComponentFixture<CareerDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CareerDashboardComponent],
     }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
