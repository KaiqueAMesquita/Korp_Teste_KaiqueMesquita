import { Component, inject, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';

import { Invoice } from '../../../shared/models/invoice';
import { InvoiceStatus } from '../../../shared/enums/invoice-status';

import { InvoiceService } from '../../../core/services/invoice.service';
import { ModalService } from '../../../core/services/modal.service';

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [
    RouterLink,
    DatePipe
  ],
  templateUrl: './invoice-list.component.html',
  styleUrl: './invoice-list.component.scss'
})
export class InvoiceListComponent implements OnInit {

  private invoiceService = inject(InvoiceService);
  private modalService = inject(ModalService);

  invoices: Invoice[] = [];

  loading = false;
  printingId: string | null = null;

  InvoiceStatus = InvoiceStatus;

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices(): void {
    this.loading = true;

    this.invoiceService
      .getAll()
      .pipe(
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe({
        next: invoices => {
          this.invoices = invoices;
        },
        error: () => {}
      });
  }

  print(invoice: Invoice): void {
    if (invoice.status !== InvoiceStatus.Opened) {
      return;
    }

    this.printingId = invoice.id;

    this.invoiceService
      .print(invoice.id)
      .pipe(
        finalize(() => {
          this.printingId = null;
        })
      )
      .subscribe({
        next: () => {
          this.modalService.open(
            'Impressão concluída',
            'A nota fiscal foi impressa e finalizada com sucesso.'
          );

          this.loadInvoices();
        },
        error: () => {}
      });
  }

}