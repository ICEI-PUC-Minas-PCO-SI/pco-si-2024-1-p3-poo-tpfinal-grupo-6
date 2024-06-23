import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared';
import { ReactiveFormsModule } from '@angular/forms';
import { PartidosComponent } from './partidos.component';
import { PartidoDetalheComponent } from './partido-detalhe/partido-detalhe.component';
import { PartidosListaComponent } from './partido-lista/partido-lista.component';




@NgModule({
  declarations: [
    PartidosComponent,
    PartidoDetalheComponent,
    PartidosListaComponent
  ],
  exports: [
    PartidosComponent,
    PartidoDetalheComponent,
    PartidosListaComponent
  ],
  imports: [
    ReactiveFormsModule,

    SharedModule
  ]
})
export class PartidosModule { }
