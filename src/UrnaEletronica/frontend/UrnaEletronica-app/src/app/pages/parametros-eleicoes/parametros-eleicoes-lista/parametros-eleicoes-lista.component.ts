import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { ParametroEleicaoService } from '../../../services/parametroEleicao/parametroEleicao.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ParametroEleicao } from '../../../shared/models/interfaces/parametroEleicao';
import { ModalDeleteComponent } from '../../../shared';
import { Router } from '@angular/router';

@Component({
  selector: 'app-parametros-eleicoes-lista',
  templateUrl: './parametros-eleicoes-lista.component.html',
})
export class ParametrosEleicoesListaComponent implements OnInit {
  #formBuilder = inject(FormBuilder);
  #dialog = inject(MatDialog);
  #router = inject(Router);

  #parametroService = inject(ParametroEleicaoService);

  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formParametroLista = {} as FormGroup;

  public parametros!: ParametroEleicao;
  public parametro!: ParametroEleicao;

  public parametroId = 0;

  filtroCAndidato() {
    this.getParametros();
  }

  public get ctrF(): any {
    return this.formParametroLista.controls;
  }

  public ngOnInit(): void {
    this.validation();
    this.getParametros();
  }

  private validation(): void {
    this.formParametroLista = this.#formBuilder.group({
      opcaoPesquisa: ['Todos'],
      argumento: [''],
    });
  }

  public getParametros(): void {
    this.#spinnerService.show();

    this.#parametroService
      .getParametros()
      .subscribe({
        next: (parametros: ParametroEleicao) => {
          this.parametros = parametros;
        },
        error: (error: any) => {
          this.#toastrService.error('Erro ao carregar parametros', 'Erro!');
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public abrirModal(
    event: any,
    parametroId: number
  ): void {
    event.stopPropagation();
    this.parametroId = parametroId;

    this.#parametroService.getParametros()
    .subscribe({
      next: (parametro: ParametroEleicao) => {
        this.parametro = parametro;
        const dialogRef = this.#dialog.open(ModalDeleteComponent, {
          data: {
            nomePagina: 'Candidatos',
            id: this.parametroId,
          },
        });
        dialogRef.afterClosed().subscribe((result) => {
          if (result) this.confirmarDelecao();
        });
      },
    });
  }

  public confirmarDelecao(): void {
    this.#spinnerService.show();

    this.#parametroService
      .deleteParametro(this.parametroId)
      .subscribe({
        next: (candidatoExcluido: any) => {
          if (candidatoExcluido.message == 'OK') {
            this.#toastrService.success(
              'Patrimonio excluído com sucesso',
              'Excluído!'
            );
            this.getParametros();
          }
        },
        error: (error: any) => {
          this.#toastrService.error(
            error.error,
            `Erro! Status ${error.status}`
          );
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.show());
  }

  public editarCandidato(_candidatoId: number): void {
    this.#router.navigate([`pages/parametrosEleicoes/detalhe/${_candidatoId}`]);
  }

  public alteracaoDePagina(event: any): void {
    //    this.pagination.currentPage = event.currentPage
    this.getParametros();
  }
}
