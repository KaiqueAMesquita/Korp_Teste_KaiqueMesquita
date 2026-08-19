import { inject, Injectable } from '@angular/core';
import { CreateInvoice } from "../../shared/models/create-invoice";
import { Invoice } from "../../shared/models/invoice";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private http = inject(HttpClient);

  private apiUrl =
    `${environment.billingApiUrl}/Invoice`;

  getAll(): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(
      this.apiUrl
    );
  }

  getById(id: string): Observable<Invoice> {
    return this.http.get<Invoice>(
      `${this.apiUrl}/${id}`
    );
  }

  create(dto: CreateInvoice): Observable<Invoice> {
    return this.http.post<Invoice>(
      this.apiUrl,
      dto
    );
  }

  print(id: string): Observable<Invoice> {
    return this.http.post<Invoice>(
      `${this.apiUrl}/${id}/print`,
      {}
    );
  }
}