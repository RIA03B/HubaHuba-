import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { OrderServiceComponent } from './order-service.component';

describe('OrderServiceComponent', () => {
  let component: OrderServiceComponent;
  let fixture: ComponentFixture<OrderServiceComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [OrderServiceComponent],
      // Add any additional providers or imports needed for testing
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderServiceComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch orders correctly', () => {
    const mockOrders = [
      { OrderID: 1, OrderType: 'Type A' },
      { OrderID: 2, OrderType: 'Type B' }
    ];

    // Mock the HTTP request
    const req = httpMock.expectOne('api/orders');
    expect(req.request.method).toBe('GET');
    req.flush(mockOrders);

    // After the HTTP request, orders should be populated in the component
    expect(component.orders).toEqual(mockOrders);
  });

  // Add more test cases as needed
});
