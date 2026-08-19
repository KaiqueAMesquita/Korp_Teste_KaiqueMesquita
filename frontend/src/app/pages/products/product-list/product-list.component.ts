import {
  Component,
  inject,
  OnInit
} from '@angular/core';

import { finalize } from 'rxjs';

import { Product } from '../../../shared/models/product';
import { ProductService } from '../../../core/services/product.service';

import {
  ProductFormComponent
} from '../product-form/product-form.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    ProductFormComponent
  ],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {

  private productService =
    inject(ProductService);

  products: Product[] = [];

  loading = false;

  showProductModal = false;

  selectedProduct: Product | null = null;

  ngOnInit(): void {

    this.loadProducts();

  }

  loadProducts(): void {

    this.loading = true;

    this.productService
      .getAll()
      .pipe(
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe({

        next: products => {

          this.products = products;

        },

        error: () => {
          // interceptor
        }

      });

  }

  openCreateModal(): void {

    this.selectedProduct = null;

    this.showProductModal = true;

  }

  openEditModal(
    product: Product
  ): void {

    this.selectedProduct = product;

    this.showProductModal = true;

  }

  closeModal(): void {

    this.showProductModal = false;

    this.selectedProduct = null;

  }

  onProductSaved(): void {

    this.closeModal();

    this.loadProducts();

  }

}