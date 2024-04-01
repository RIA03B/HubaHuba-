import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserInfoService } from '../services/user-info.service'; // Import the UserInfoService if needed

@Component({
  selector: 'app-order-service',
  templateUrl: './order-service.component.html',
  styleUrls: ['./order-service.component.css']
})
export class OrderServiceComponent implements OnInit {
  orders: any[] = [];
  userID: string; // Define the userID property

  constructor(private http: HttpClient, private userInfoService: UserInfoService) { } // Inject the UserInfoService if needed

  ngOnInit(): void {
    // Fetch orders when the component initializes
    this.getOrders();

    // Load the userID
    this.loadUserID();
  }

  loadUserID(): void {
    this.userInfoService.get_UserIdGuid().subscribe(response => {
      this.userID = response;
    });
  }

  getOrders(): void {
    // Assuming the API endpoint for fetching orders is 'api/orders/not-taken'
    this.http.get<any[]>('api/orders/not-taken').subscribe({
      next: (orders) => {
        this.orders = orders;
      },
      error: (err) => {
        console.error('Error fetching orders:', err);
      }
    });
  }

  takeOrder(order: any): void {
    // Call your backend API to take the order and assign it to the current user
    this.http.post<any>(`api/assign-order`, { orderId: order.id, userId: this.userID }).subscribe({
      next: (response) => {
        // Remove the taken order from the list
        this.orders = this.orders.filter(o => o.id !== order.id);
      },
      error: (err) => {
        console.error('Error taking order:', err);
      }
    });
  }

  showOrderDetails(order: any): void {
    // Implement the logic to show order details
  }
}
