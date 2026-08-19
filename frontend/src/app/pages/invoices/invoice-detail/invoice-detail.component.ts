import { Component, inject, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';

import { Invoice } from '../../../shared/models/invoice';
import { InvoiceStatus } from '../../../shared/enums/invoice-status';

import { InvoiceService } from '../../../core/services/invoice.service';
import { ModalService } from '../../../core/services/modal.service';

@Component({
  selector: 'app-invoice-detail',
  standalone: true,
  imports: [
    RouterLink,
    DatePipe
  ],
  templateUrl: './invoice-detail.component.html',
  styleUrl: './invoice-detail.component.scss'
})
export class InvoiceDetailComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private invoiceService = inject(InvoiceService);
  private modalService = inject(ModalService);

  invoice: Invoice | null = null;

  loading = false;
  printing = false;

  InvoiceStatus = InvoiceStatus;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      return;
    }

    this.loadInvoice(id);
  }

  loadInvoice(id: string): void {
    this.loading = true;

    this.invoiceService
      .getById(id)
      .pipe(
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe({
        next: invoice => {
          this.invoice = invoice;
        },
        error: () => {}
      });
  }

  print(): void {
    if (!this.invoice) {
      return;
    }

    if (this.invoice.status !== InvoiceStatus.Opened) {
      return;
    }

    this.printing = true;

    this.invoiceService
      .print(this.invoice.id)
      .pipe(
        finalize(() => {
          this.printing = false;
        })
      )
      .subscribe({
        next: () => {
          this.modalService.open(
            'Impressão concluída',
            'A nota fiscal foi impressa e finalizada com sucesso.'
          );

          this.router.navigate(['/invoices']);
        },
        error: () => {}
      });
  }

}