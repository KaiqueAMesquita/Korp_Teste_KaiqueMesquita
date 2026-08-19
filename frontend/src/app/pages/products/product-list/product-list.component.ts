import { Component } from '@angular/core';
import { ProductFormComponent } from '../product-form/product-form.component';

interface Product {
  id: string;
  code: string;
  description: string;
  balance: number;
}

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    ProductFormComponent
  ],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent {

  showProductModal = false;

  selectedProduct: Product | null = null;

  products: Product[] = [
    {
      id: '1',
      code: '34567',
      description: 'Produto A',
      balance: 8
    },
    {
      id: '2',
      code: '223232',
      description: 'Produto B',
      balance: 2
    },
    {
      id: '3',
      code: '99881',
      description: 'Produto C',
      balance: 15
    }
  ];

  openCreateModal(): void {
    this.selectedProduct = null;
    this.showProductModal = true;
  }

  openEditModal(product: Product): void {
    this.selectedProduct = product;
    this.showProductModal = true;
  }

  closeModal(): void {
    this.showProductModal = false;
    this.selectedProduct = null;
  }

  onProductSaved(): void {
    this.closeModal();

    // depois:
    // this.loadProducts();
  }
}