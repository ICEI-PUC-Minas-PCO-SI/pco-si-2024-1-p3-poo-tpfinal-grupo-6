import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared';
import { ReactiveFormsModule } from '@angular/forms';
import { PartidoDetalheComponent, PartidosComponent, PartidosListaComponent } from '.';

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
