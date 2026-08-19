import {
  Component,
  inject,
  OnInit
} from '@angular/core';

import {
  FormArray,
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import {
  Router,
  RouterLink
} from '@angular/router';

import {
  finalize
} from 'rxjs';

import { Product } from '../../../shared/models/product';

import { CreateInvoice } from '../../../shared/models/create-invoice';

import { ProductService } from '../../../core/services/product.service';

import { InvoiceService } from '../../../core/services/invoice.service';

@Component({
  selector: 'app-invoice-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './invoice-create.component.html',
  styleUrl: './invoice-create.component.scss'
})
export class InvoiceCreateComponent implements OnInit {

  private fb =
    inject(FormBuilder);

  private productService =
    inject(ProductService);

  private invoiceService =
    inject(InvoiceService);

  private router =
    inject(Router);

  products: Product[] = [];

  loadingProducts = false;

  saving = false;

  form = this.fb.group({

    items: this.fb.array([])

  });

  get items(): FormArray {

    return this.form.controls.items;

  }

  ngOnInit(): void {

    this.loadProducts();

    this.addItem();

  }

  loadProducts(): void {

    this.loadingProducts = true;

    this.productService
      .getAll()
      .pipe(
        finalize(() => {

          this.loadingProducts = false;

        })
      )
      .subscribe({

        next: products => {

          this.products = products;

        },

        error: () => {}

      });

  }

  createItem() {

    return this.fb.group({

      productId: [
        '',
        Validators.required
      ],

      quantity: [
        1,
        [
          Validators.required,
          Validators.min(1)
        ]
      ]

    });

  }

  addItem(): void {

    this.items.push(
      this.createItem()
    );

  }

  removeItem(
    index: number
  ): void {

    if (this.items.length <= 1) {
      return;
    }

    this.items.removeAt(index);

  }

  submit(): void {

    if (this.form.invalid) {

      this.form.markAllAsTouched();

      return;

    }

    const dto: CreateInvoice = {

      items:
        this.items.controls.map(
          item => ({

            productId:
              item.get('productId')!
                .value!,

            quantity:
              Number(
                item.get('quantity')!
                  .value
              )

          })
        )

    };

    this.saving = true;

    this.invoiceService
      .create(dto)
      .pipe(
        finalize(() => {

          this.saving = false;

        })
      )
      .subscribe({

        next: invoice => {

          this.router.navigate([
            '/invoices',
            invoice.id
          ]);

        },

        error: () => {}

      });

  }

}