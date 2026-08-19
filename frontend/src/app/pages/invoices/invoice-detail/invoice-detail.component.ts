import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-invoice-detail',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './invoice-detail.component.html',
  styleUrl: './invoice-detail.component.scss'
})
export class InvoiceDetailComponent {

  printing = false;

  invoice = {
    id: '1',
    number: 1,
    createdAt: '18/08/2026 20:30',
    status: 'Opened',
    items: [
      {
        productCode: '34567',
        productDescription: 'Produto A',
        quantity: 2
      },
      {
        productCode: '223232',
        productDescription: 'Produto B',
        quantity: 2
      }
    ]
  };

  print(): void {

    this.printing = true;

    // depois:
    // invoiceService.print()

    setTimeout(() => {
      this.printing = false;
    }, 1500);
  }
}