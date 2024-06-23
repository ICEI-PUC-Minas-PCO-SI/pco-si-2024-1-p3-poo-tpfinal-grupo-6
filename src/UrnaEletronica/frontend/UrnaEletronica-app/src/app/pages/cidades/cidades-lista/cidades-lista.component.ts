import { Component, OnInit, inject } from '@angular/core';
import { CidadeService } from '../../../services/cidade/cidade.service';
import { MatDialog } from '@angular/material/dialog';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UsuarioService } from '../../../services/usuario';
import { Router } from '@angular/router';
import { Cidade } from '../../../shared/models/interfaces/cidade/Cidade';
import { Usuario } from '../../../shared/models/interfaces/usuario';
import { ModalDeleteComponent } from '../../../shared';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-cidades-lista',
  templateUrl: './cidades-lista.component.html',
  styleUrls: ['./cidades-lista.component.scss']
})
export class CidadesListaComponent implements OnInit {

  #cidadeService = inject(CidadeService);
  #dialog = inject(MatDialog);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);
  #usuarioService = inject(UsuarioService);

  public formCidadesLista = {} as FormGroup;

  public cidades = [] as Cidade[];
  public cidade = {} as Cidade;

  public cidadeId = 0;
  public cidadeNome = "";

  public usuarioAdmin = false;

  public get ctrF(): any {
    return this.formCidadesLista.controls;
  }

  ngOnInit(): void {
    this.validation();
    this.getUserName();
    this.getCidades();
  }

  private validation(): void {
    this.formCidadesLista = this.#formBuilder.group({
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

  public getCidades(): void {
    this.#spinnerService.show();

    this.#cidadeService
      .getCidades()
      .subscribe({
        next: (retorno: Cidade[]) => {
          this.cidades = retorno;
          console.log(this.cidades.length)
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao carregar Cidades", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public abrirModal(event: any, cidadeId: number, cidadeNome: string): void {
    event.stopPropagation();

    this.cidadeId = cidadeId;
    this.cidadeNome = cidadeNome;

    this.validarCidade(this.cidadeId);
  }

  public confirmarDelecao(): void {
    this.#spinnerService.show();

    this.#cidadeService
      .deleteCidade(this.cidadeId)
      .subscribe({
        next: (result: any) => {
          if (result == null)
            this.#toastrService.error("Cidadae não pode se excluída.", "Erro!");

          if (result.message == "OK") {
            this.#toastrService.success(
              "Cidade excluída com sucesso",
              "Excluído!"
            );
            this.getCidades();
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

  public validarCidade(cidadeId: number): void {
    this.#spinnerService.show();

    this.#cidadeService
      .getCidadeById(cidadeId)
      .subscribe({
        next: (cidade: Cidade) => {
          this.cidade = cidade;


            const dialogRef = this.#dialog.open(ModalDeleteComponent, {
              data: {
                nomePagina: "Acervos",
                id: this.cidadeId,
                argumento: this.cidadeNome,
              },
            });

            dialogRef.afterClosed().subscribe((result) => {
              if (result) this.confirmarDelecao();
            });
        },
        error: (error: any) => {
          this.#toastrService.error(
            "Falha ao recuperar Cidade",
            `Erro! Status ${error.status}`
          );
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public editarCidade(cidadeId: number): void {
    this.#router.navigate([`pages/cidades/detalhe/${cidadeId}`]);
  }

  public detalheCidade(event: any, cidadeId: number): void {
    event.stopPropagation();

    this.#router.navigate([`pages/cidade/detalhe/${cidadeId}`], { skipLocationChange: false});
  }
}
