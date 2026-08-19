import {
  Component,
  EventEmitter,
  Input,
  Output
} from '@angular/core';

interface Product {
  id: string;
  code: string;
  description: string;
  balance: number;
}

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent {

  @Input()
  product: Product | null = null;

  @Output()
  saved = new EventEmitter<void>();

  @Output()
  cancelled = new EventEmitter<void>();

  save(): void {

    // depois entra:
    // ReactiveForms
    // ProductService
    // create/update

    this.saved.emit();
  }

  cancel(): void {
    this.cancelled.emit();
  }
}