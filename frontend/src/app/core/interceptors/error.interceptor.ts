import {
  HttpErrorResponse,
  HttpInterceptorFn
} from '@angular/common/http';

import { inject } from '@angular/core';

import {
  catchError,
  throwError
} from 'rxjs';

import { ApiError } from '../../shared/models/api-error';
import { ModalService } from '../services/modal.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const modalService = inject(ModalService);

  return next(req).pipe(

    catchError((error: HttpErrorResponse) => {

      let apiError: ApiError;

      if (error.status === 0) {

        apiError = {
          status: 0,
          message: 'Não foi possível conectar ao servidor.'
        };

      } else {

        apiError = {
          status: error.status,

          message:
            error.error?.message ??
            'Ocorreu um erro inesperado.'
        };
      }

      let title = 'Erro';

      switch (apiError.status) {

        case 400:
          title = 'Dados inválidos';
          break;

        case 404:
          title = 'Não encontrado';
          break;

        case 409:
          title = 'Operação não permitida';
          break;

        case 503:
          title = 'Serviço indisponível';
          break;

        case 500:
          title = 'Erro interno';
          break;

        case 0:
          title = 'Erro de conexão';
          break;
      }

      modalService.open(
        title,
        apiError.message
      );

      return throwError(() => error);
    })
  );
};