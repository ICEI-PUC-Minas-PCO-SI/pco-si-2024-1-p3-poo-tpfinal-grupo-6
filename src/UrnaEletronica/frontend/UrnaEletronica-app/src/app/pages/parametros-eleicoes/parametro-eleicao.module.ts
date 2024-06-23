import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ParametroEleicaoDetalheComponent, ParametrosEleicoesComponent, ParametrosEleicoesListaComponent } from '.';
import { SharedModule } from '../../shared';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    ReactiveFormsModule, SharedModule
  ],
  declarations: [ParametrosEleicoesComponent, ParametroEleicaoDetalheComponent, ParametrosEleicoesListaComponent],
  exports: [ParametrosEleicoesComponent, ParametroEleicaoDetalheComponent, ParametrosEleicoesListaComponent],
})
export class ParametroEleicaoModule { }
