import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'products',
    pathMatch: 'full'
  },

  {
    path: 'products',
    loadComponent: () =>
      import('./pages/products/product-list/product-list.component')
        .then(m => m.ProductListComponent)
  },

  {
    path: 'invoices',
    loadComponent: () =>
      import('./pages/invoices/invoice-list/invoice-list.component')
        .then(m => m.InvoiceListComponent)
  },

  {
    path: 'invoices/create',
    loadComponent: () =>
      import('./pages/invoices/invoice-create/invoice-create.component')
        .then(m => m.InvoiceCreateComponent)
  },

  {
    path: 'invoices/:id',
    loadComponent: () =>
      import('./pages/invoices/invoice-detail/invoice-detail.component')
        .then(m => m.InvoiceDetailComponent)
  }
];