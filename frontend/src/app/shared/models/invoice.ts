import { InvoiceItem } from "./invoice-item";

export interface Invoice {
  id: string;
  number: number;
  status: number;
  createdAt: string;
  items: InvoiceItem[];
}