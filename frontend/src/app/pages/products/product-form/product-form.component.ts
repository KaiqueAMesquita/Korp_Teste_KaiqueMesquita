import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnChanges,
  Output,
  SimpleChanges
} from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { finalize } from 'rxjs';

import { Product } from '../../../shared/models/product';
import { CreateProduct } from '../../../shared/models/create-product';
import { ProductService } from '../../../core/services/product.service';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnChanges {

  private fb = inject(FormBuilder);

  private productService =
    inject(ProductService);

  @Input()
  product: Product | null = null;

  @Output()
  saved = new EventEmitter<void>();

  @Output()
  cancelled = new EventEmitter<void>();

  saving = false;

  form = this.fb.nonNullable.group({

    code: [
      '',
      Validators.required
    ],

    description: [
      '',
      Validators.required
    ],

    balance: [
      0,
      [
        Validators.required,
        Validators.min(0)
      ]
    ]

  });

  ngOnChanges(
    changes: SimpleChanges
  ): void {

    if (!changes['product']) {
      return;
    }

    if (this.product) {

      this.form.reset({
        code: this.product.code,
        description: this.product.description,
        balance: this.product.balance
      });

    } else {

      this.form.reset({
        code: '',
        description: '',
        balance: 0
      });

    }
  }

  submit(): void {

    if (this.form.invalid) {

      this.form.markAllAsTouched();

      return;
    }

    const dto: CreateProduct =
      this.form.getRawValue();

    this.saving = true;

    const request$ =
      this.product
        ? this.productService.update(
            this.product.id,
            dto
          )
        : this.productService.create(dto);

    request$
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe({

        next: () => {
          this.saved.emit();
        },

        error: () => {
          // interceptor trata
        }

      });
  }

  cancel(): void {
    this.cancelled.emit();
  }
}