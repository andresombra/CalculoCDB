import { Component, OnInit } from '@angular/core';
import { InvestimentoCDBService } from './investimentoCDB.service';
import { InvestimentoCDBModel } from '../investimentoCDB.model';

@Component({
  selector: 'investimentoCDB',
  templateUrl: './investimentoCDB.component.html',
  styleUrls: ['./investimentoCDB.component.css']
})
export class CalculoInvestimentoComponent implements OnInit {

  valorInicial: number;
  prazoMeses: number;
  valorFinal: number;
  valorLiquido: number;
  resultadoCalculado: boolean;
  errorMessage: string = '';


  constructor(private investimentoService: InvestimentoCDBService) {
    // Inicialização das propriedades
    this.valorInicial = 0;
    this.prazoMeses = 0;
    this.valorFinal = 0;
    this.valorLiquido = 0;
    this.resultadoCalculado = false;
  }

  ngOnInit(): void {
    // Nada a fazer aqui por enquanto
  }

  calcularInvestimento(): void {
    this.errorMessage = '';

    // Validação do prazo em meses
    if (this.prazoMeses <= 1) {
      this.errorMessage = 'O prazo em meses deve ser maior que 1.';
      return;
    }

    this.investimentoService.calcular(this.valorInicial, this.prazoMeses)
      .subscribe({
        next: res => {
          this.valorFinal = res.valorFinal;
          this.valorLiquido = res.valorLiquido;
          this.resultadoCalculado = true;
        },
        error: err => console.error("Ocorreu um erro:", err),
        complete: () => console.log('Cálculo de investimento concluído.')
      });
  }
}
