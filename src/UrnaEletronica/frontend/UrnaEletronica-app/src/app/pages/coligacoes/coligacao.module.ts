import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared';
import { ColigacaoDetalheComponent, ColigacoesComponent, ColigacoesListaComponent } from '.';

@NgModule({
  imports: [ReactiveFormsModule, SharedModule],
  declarations: [ColigacoesComponent, ColigacoesListaComponent, ColigacaoDetalheComponent],
  exports: [ColigacoesComponent, ColigacoesListaComponent, ColigacaoDetalheComponent]
})
export class ColigacaoModule { }
