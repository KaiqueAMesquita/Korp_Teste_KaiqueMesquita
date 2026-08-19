import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DebitStockItem {
  productId: string;
  quantity: number;
}

export interface DebitStockRequest {
  items: DebitStockItem[];
}

@Injectable({
  providedIn: 'root'
})
export class StockService {

  private http = inject(HttpClient);

  private apiUrl =
    'http://localhost:5001/api/stock';

  debit(
    dto: DebitStockRequest
  ): Observable<void> {

    return this.http.post<void>(
      `${this.apiUrl}/debit`,
      dto
    );
  }
}