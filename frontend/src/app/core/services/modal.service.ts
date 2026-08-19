import { Injectable, signal } from '@angular/core';

export interface ModalData {
  title: string;
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  private modalState = signal<ModalData | null>(null);

  modal = this.modalState.asReadonly();

  open(title: string, message: string): void {
    this.modalState.set({
      title,
      message
    });
  }

  close(): void {
    this.modalState.set(null);
  }
}