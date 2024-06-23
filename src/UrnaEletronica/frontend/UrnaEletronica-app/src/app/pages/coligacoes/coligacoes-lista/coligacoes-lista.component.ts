import { Component, OnInit, inject } from '@angular/core';
import { ColigacaoService } from '../../../services/coligacao/coligacao.service';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UsuarioService } from '../../../services/usuario';
import { Coligacao } from '../../../shared/models/interfaces/coligacao';
import { Usuario } from '../../../shared/models/interfaces/usuario';
import { ModalDeleteComponent } from '../../../shared';

@Component({
  selector: 'app-coligacoes-lista',
  templateUrl: './coligacoes-lista.component.html',
})
export class ColigacoesListaComponent implements OnInit {
  #coligacaoService = inject(ColigacaoService);
  #dialog = inject(MatDialog);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);
  #usuarioService = inject(UsuarioService);

  public formColigacoesLista = {} as FormGroup;

  public coligacoes = [] as Coligacao[];
  public coligacao = {} as Coligacao;

  public coligacaoId = 0;
  public coligacaoNome = "";

  public usuarioAdmin = false;

  public get ctrF(): any {
    return this.formColigacoesLista.controls;
  }

  ngOnInit(): void {
    this.validation();
    this.getUserName();
    this.getColigacoes();
  }

  private validation(): void {
    this.formColigacoesLista = this.#formBuilder.group({
      opcaoPesquisa: ["Todos"],
      argumento: [""],
    });
  }

  public getUserName(): void {
    this.#spinnerService.show();

    this.#usuarioService
      .getUsuarioByUserName()
      .subscribe({
        next: (usuario: Usuario) => {
          if (usuario.userName === "Admin") this.usuarioAdmin = true;
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao carregar Usuário", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public getColigacoes(): void {
    this.#spinnerService.show();

    this.#coligacaoService
      .getColigacoes()
      .subscribe({
        next: (retorno: Coligacao[]) => {
          this.coligacoes = retorno;
          console.log(this.coligacoes.length)
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao carregar Coligações", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public abrirModal(event: any, coligacaoId: number, coligacaoNome: string): void {
    event.stopPropagation();

    this.coligacaoId = coligacaoId;
    this.coligacaoNome = coligacaoNome;

    this.validarColigacao(this.coligacaoId);
  }

  public confirmarDelecao(): void {
    this.#spinnerService.show();

    this.#coligacaoService
      .deleteColigacao(this.coligacaoId)
      .subscribe({
        next: (result: any) => {
          if (result == null)
            this.#toastrService.error("Cidadae não pode se excluída.", "Erro!");

          if (result.message == "OK") {
            this.#toastrService.success(
              "Coligacao excluída com sucesso",
              "Excluído!"
            );
            this.getColigacoes();
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
      .add(() => this.#spinnerService.hide());
  }

  public validarColigacao(coligacaoId: number): void {
    this.#spinnerService.show();

    this.#coligacaoService
      .getColigacaoById(coligacaoId)
      .subscribe({
        next: (coligacao: Coligacao) => {
          this.coligacao = coligacao;


            const dialogRef = this.#dialog.open(ModalDeleteComponent, {
              data: {
                nomePagina: "Acervos",
                id: this.coligacaoId,
                argumento: this.coligacaoNome,
              },
            });

            dialogRef.afterClosed().subscribe((result) => {
              if (result) this.confirmarDelecao();
            });
        },
        error: (error: any) => {
          this.#toastrService.error(
            "Falha ao recuperar Coligacao",
            `Erro! Status ${error.status}`
          );
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public editarColigacao(coligacaoId: number): void {
    this.#router.navigate([`pages/coligacoes/detalhe/${coligacaoId}`]);
  }

  public detalheColigacao(event: any, coligacaoId: number): void {
    event.stopPropagation();

    this.#router.navigate([`pages/coligacoes/detalhe/${coligacaoId}`], { skipLocationChange: false});
  }
}
