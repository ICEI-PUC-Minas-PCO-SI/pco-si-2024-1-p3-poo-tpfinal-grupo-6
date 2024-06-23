import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared';
import { ReactiveFormsModule } from '@angular/forms';
import { CandidatoDetalheComponent, CandidatosComponent, CandidatosListaComponent } from '.';

@NgModule({
  declarations: [
    CandidatosComponent,
    CandidatoDetalheComponent,
    CandidatosListaComponent
  ],
  exports: [
    CandidatosComponent,
    CandidatoDetalheComponent,
    CandidatosListaComponent
  ],
  imports: [
    ReactiveFormsModule,

    SharedModule
  ]
})
export class CandidatosModule { }
