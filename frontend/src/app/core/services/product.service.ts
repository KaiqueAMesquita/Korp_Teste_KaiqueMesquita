import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateProduct } from '../../shared/models/create-product';
import { Product } from './../../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private http = inject(HttpClient);

  private apiUrl = `${environment.stockApiUrl}/Product`;

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getById(id: string): Observable<Product> {
    
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  create(product: CreateProduct): Observable<Product> {

    return this.http.post<Product>(this.apiUrl,product);
  }

  update(id: string,product: CreateProduct): Observable<Product> {

    return this.http.put<Product>(`${this.apiUrl}/${id}`,product);
  }
}