import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColigacaoComponent } from './coligacao.component';
import { ColigacoesListaComponent } from './coligacoes-lista/coligacoes-lista.component';
import { ColigacaoDetalheComponent } from './coligacao-detalhe/coligacao-detalhe.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [ColigacaoComponent, ColigacoesListaComponent, ColigacaoDetalheComponent],
  exports: [ColigacaoComponent, ColigacoesListaComponent, ColigacaoDetalheComponent],
  imports: [ReactiveFormsModule, SharedModule]
})
export class ColigacaoModule { }
