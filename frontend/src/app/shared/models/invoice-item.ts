export interface InvoiceItem {
  id: string;
  productId: string;
  productCode: string;
  productDescription: string;
  quantity: number;
  invoiceId: string;
}