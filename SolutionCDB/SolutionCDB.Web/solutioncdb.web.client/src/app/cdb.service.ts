import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
interface CdbRequest {
  ValorInvestimento: number; // double
  PrazoMes: number; // int

}

interface ResponseDto {
  sucesso: boolean;
  mensagem?: string;
  dados?: {
    valorBruto: number;
    valorLiquido: number;
  };
}

@Injectable({
  providedIn: 'root'
})
export class CdbService {
  private apiUrl = 'https://localhost:7096/Cdb/Calcular';

  dados: any;
  constructor(private http: HttpClient) { }

  calcularCdb(ValorInvestimento: number, PrazoMes: number): Observable<{ ValorBruto: number, ValorLiquido: number }> {
    const requestBody: CdbRequest = { ValorInvestimento, PrazoMes };
    return this.http.post<ResponseDto>(this.apiUrl, requestBody).pipe(
      map(response => {
        if (response.sucesso && response.dados) {
          return {
            ValorBruto: response.dados.valorBruto,
            ValorLiquido: response.dados.valorLiquido
          };
        } else {
          throw new Error(response.mensagem ?? 'Erro desconhecido');
        }
      })
    );
  }
}
