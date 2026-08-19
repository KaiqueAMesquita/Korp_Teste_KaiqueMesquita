import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './invoice-list.component.html',
  styleUrl: './invoice-list.component.scss'
})
export class InvoiceListComponent {

  invoices = [
    {
      id: '1',
      number: 1,
      createdAt: '18/08/2026 20:30',
      status: 'Opened',
      itemCount: 2
    },
    {
      id: '2',
      number: 2,
      createdAt: '18/08/2026 19:45',
      status: 'Closed',
      itemCount: 3
    }
  ];

}