import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EleicoesComponent } from './eleicoes.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared';

@NgModule({
  imports: [
    ReactiveFormsModule, SharedModule
  ],
  declarations: [EleicoesComponent],
  exports: [EleicoesComponent]
})
export class EleicaoModule { }
