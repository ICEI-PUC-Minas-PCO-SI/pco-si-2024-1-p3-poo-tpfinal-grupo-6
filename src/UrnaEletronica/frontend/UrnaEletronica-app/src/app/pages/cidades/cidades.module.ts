import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CidadesComponent } from './cidades.component';
import { SharedModule } from '../../shared';
import { CidadesListaComponent } from './cidades-lista/cidades-lista.component';
import { CidadeDetalheComponent } from './cidade-detalhe/cidade-detalhe.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [CidadesComponent, CidadesListaComponent, CidadeDetalheComponent],
  exports: [CidadesComponent, CidadesListaComponent, CidadeDetalheComponent]
})
export class CidadesModule { }
