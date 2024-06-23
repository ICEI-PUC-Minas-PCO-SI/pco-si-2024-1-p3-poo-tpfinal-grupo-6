import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Candidato } from '../../../shared/models/interfaces/candidato';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CandidatoService } from '../../../services/candidato';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ModalDeleteComponent } from '../../../shared';

@Component({
  selector: 'app-candidatos-lista',
  templateUrl: './candidatos-lista.component.html',
})
export class CandidatosListaComponent implements OnInit {
  #formBuilder = inject(FormBuilder);
  #dialog = inject(MatDialog);
  #router = inject(Router);

  #candidatoService = inject(CandidatoService);

  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formCandidatosLista = {} as FormGroup;

  public candidatos = [] as Candidato[];
  public candidato!: Candidato;

  public candidatoId = 0;
  public nomeCandidato = '';

  public exibirImagem: boolean = true;

  public candidatoImagem = "../../../../assets/images/candidato.png";

  filtroCAndidato() {
    this.getCandidatos();
  }

  public get ctrF(): any {
    return this.formCandidatosLista.controls;
  }

  public ngOnInit(): void {
    this.validation();
    this.getCandidatos();
  }

  private validation(): void {
    this.formCandidatosLista = this.#formBuilder.group({
      opcaoPesquisa: ['Todos'],
      argumento: [''],
    });
  }

  public getCandidatos(): void {
    this.#spinnerService.show();

    this.#candidatoService
      .getCandidatos()
      .subscribe({
        next: (candidatos: Candidato[]) => {
          this.candidatos = candidatos;
        },
        error: (error: any) => {
          this.#toastrService.error('Erro ao carregar candidatos', 'Erro!');
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public alterarImagem() {
    this.exibirImagem = !this.exibirImagem;
  }

  public abrirModal(
    event: any,
    candidatoId: number,
    nomeCandidato: string
  ): void {
    event.stopPropagation();
    this.candidatoId = candidatoId;
    this.nomeCandidato = nomeCandidato;

    this.#candidatoService.getCandidatoById(candidatoId).subscribe({
      next: (candidato: Candidato) => {
        this.candidato = candidato;
        const dialogRef = this.#dialog.open(ModalDeleteComponent, {
          data: {
            nomePagina: 'Candidatos',
            id: this.candidatoId,
            nome: this.nomeCandidato,
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

    this.#candidatoService
      .deleteCandidato(this.candidatoId)
      .subscribe({
        next: (candidatoExcluido: any) => {
          if (candidatoExcluido == null)
            this.#toastrService.error(
              'Patrimonio não pode se excluído.',
              'Erro!'
            );
          if (candidatoExcluido.message == 'OK') {
            this.#toastrService.success(
              'Patrimonio excluído com sucesso',
              'Excluído!'
            );
            this.getCandidatos();
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
    this.#router.navigate([`pages/candidatos/detalhe/${_candidatoId}`]);
  }

  public alteracaoDePagina(event: any): void {
    //    this.pagination.currentPage = event.currentPage
    this.getCandidatos();
  }
}
