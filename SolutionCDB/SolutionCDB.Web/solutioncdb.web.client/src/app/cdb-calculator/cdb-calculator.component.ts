import { Component } from '@angular/core';
import { CdbService } from '../cdb.service';


@Component({
  selector: 'app-cdb-calculator',
  templateUrl: './cdb-calculator.component.html',
  styleUrls: ['./cdb-calculator.component.css']
})
export class CdbCalculatorComponent {
  ValorInvestimento: number = 0;
  PrazoMes: number = 0;
  valorBruto: number | null = null;
  valorLiquido: number | null = null;
  mensagemErro: string | null = null;

  constructor(private cdbService: CdbService) { }

  calcular() {
    if (this.ValorInvestimento <= 0) {
      this.mensagemErro = 'O valor do Investimento deve ser maior que zero.';
      return;
    }
    if (this.PrazoMes <= 0) {
      this.mensagemErro = 'O valor do PrazoMes deve ser maior que 1.';
      return;
    }

    this.mensagemErro = '';


      this.cdbService.calcularCdb(this.ValorInvestimento, this.PrazoMes).subscribe(
        response => {
          this.valorBruto = response.ValorBruto;
          this.valorLiquido = response.ValorLiquido;
          this.mensagemErro = null;
        },
        error => {
          this.mensagemErro = error.message;
          this.valorBruto = null;
          this.valorLiquido = null;
        }
      );
    
  }
}
