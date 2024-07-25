import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InvestimentoCDBModel } from '../investimentoCDB.model';

@Injectable({
  providedIn: 'root'
})
export class InvestimentoCDBService {

  private apiUrl = 'https://localhost:72096/api/investimento';
  constructor(private http: HttpClient) { }

  public calcular(valorInicial: number, prazoMeses: number): Observable<InvestimentoCDBModel> {
    const payload = {
      valorInicial,
      prazoMeses
    };

    return this.http.post<InvestimentoCDBModel>(`${this.apiUrl}/calcular`, payload);
  }
}
