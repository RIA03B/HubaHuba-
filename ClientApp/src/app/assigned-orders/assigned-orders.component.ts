import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-assigned-orders',
  templateUrl: './assigned-orders.component.html',
  styleUrls: ['./assigned-orders.component.css']
})
export class AssignedOrdersComponent implements OnInit {
  assignedOrders: any[] = []; // Placeholder for assigned orders

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    // Fetch assigned orders from the server
    this.fetchAssignedOrders();
  }

  fetchAssignedOrders() {
    // Replace the URL with the actual API endpoint to fetch assigned orders
    const apiUrl = 'api/assignedOrders'; // Example URL
    this.http.get<any[]>(apiUrl).subscribe(
      (response) => {
        // Assign the fetched orders to the component property
        this.assignedOrders = response;
      },
      (error) => {
        console.error('Error fetching assigned orders:', error);
      }
    );
  }
}

