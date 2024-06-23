import { Component, OnInit, inject } from '@angular/core';
import { PartidoService } from '../../../services/partido/partido.service';
import { MatDialog } from '@angular/material/dialog';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UsuarioService } from '../../../services/usuario';
import { Router } from '@angular/router';
import { Usuario } from '../../../shared/models/interfaces/usuario';
import { ModalDeleteComponent } from '../../../shared';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Partido } from '../../../shared/models/interfaces/partido';

@Component({
  selector: 'app-partidos-lista',
  templateUrl: './partidos-lista.component.html',
})
export class PartidosListaComponent implements OnInit {

  #partidoService = inject(PartidoService);
  #dialog = inject(MatDialog);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);
  #usuarioService = inject(UsuarioService);

  public formPartidosLista = {} as FormGroup;

  public partidos = [] as Partido[];
  public partido = {} as Partido;

  public partidoId = 0;
  public partidoNome = "";

  public usuarioAdmin = false;

  public get ctrF(): any {
    return this.formPartidosLista.controls;
  }

  ngOnInit(): void {
    this.validation();
    this.getUserName();
    this.getPartidos();
  }

  private validation(): void {
    this.formPartidosLista = this.#formBuilder.group({
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

  public getPartidos(): void {
    this.#spinnerService.show();

    this.#partidoService
      .getPartidos()
      .subscribe({
        next: (retorno: Partido[]) => {
          this.partidos = retorno;
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao carregar Partidos", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public abrirModal(event: any, partidoId: number, partidoNome: string): void {
    event.stopPropagation();

    this.partidoId = partidoId;
    this.partidoNome = partidoNome;

    this.validarPartido(this.partidoId);
  }

  public confirmarDelecao(): void {
    this.#spinnerService.show();

    this.#partidoService
      .deletePartido(this.partidoId)
      .subscribe({
        next: (result: any) => {
          if (result == null)
            this.#toastrService.error("Partido não pode ser excluído.", "Erro!");

          if (result.message == "OK") {
            this.#toastrService.success(
              "Partido excluído com sucesso",
              "Excluído!"
            );
            this.getPartidos();
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

  public validarPartido(partidoId: number): void {
    this.#spinnerService.show();

    this.#partidoService
      .getPartidoById(partidoId)
      .subscribe({
        next: (partido: Partido) => {
          this.partido = partido;


            const dialogRef = this.#dialog.open(ModalDeleteComponent, {
              data: {
                nomePagina: "Acervos",
                id: this.partidoId,
                argumento: this.partidoNome,
              },
            });

            dialogRef.afterClosed().subscribe((result) => {
              if (result) this.confirmarDelecao();
            });
        },
        error: (error: any) => {
          this.#toastrService.error(
            "Falha ao recuperar Partido",
            `Erro! Status ${error.status}`
          );
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public editarPartido(partidoId: number): void {
    this.#router.navigate([`pages/partidos/detalhe/${partidoId}`]);
  }

  public detalhePartido(event: any, partidoId: number): void {
    event.stopPropagation();

    this.#router.navigate([`pages/partido/detalhe/${partidoId}`], { skipLocationChange: false});
  }
}
