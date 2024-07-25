import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CalculoInvestimentoComponent } from './investimentoCDB/investimentoCDB.component'; // Importe seu componente
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    CalculoInvestimentoComponent // Adicione seu componente aqui
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, 
    RouterModule.forRoot([
      { path: '', component: CalculoInvestimentoComponent, pathMatch: 'full' } // Atualize para o seu componente
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
