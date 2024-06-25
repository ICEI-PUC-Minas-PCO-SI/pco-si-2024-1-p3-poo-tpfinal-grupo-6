import { Component, OnInit, inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { EleicaoService } from '../../../services/eleicao/eleicao.service';
import { ToastrService } from 'ngx-toastr';
import { Eleicao } from '../../../shared/models/interfaces/eleicao';

@Component({
  selector: 'app-config-administrador',
  templateUrl: './config-administrador.component.html',
})
export class ConfigAdministradorComponent implements OnInit {
  #eleicaoService = inject(EleicaoService);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public eleicaoPendente: boolean = false;

  public eleicoes = [] as Eleicao[]

  ngOnInit() {

    this.getEleicoes();
  }


  public getEleicoes(): void {
    this.#spinnerService.show();

    this.#eleicaoService
      .getEleicoes()
      .subscribe({
        next: (eleicoes: Eleicao[]) => {
          this.eleicoes = eleicoes;

          this.eleicaoPendente = this.eleicoes.some(eleicao => !eleicao.encerrarVotacao);
          console.log(eleicoes)
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar eleicoes", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

//   public getPartidos(): void {
//     this.#spinnerService.show();

//     this.#partidoService
//       .getPartidos()
//       .subscribe({
//         next: (partidos: Partido[]) => {
//           this.partidos = partidos;
//         },
//         error: (error: any) => {
//           this.#toastrService.error("Falha ao recuperar Partidos", "Erro!");
//           console.error(error);
//         },
//       })
//       .add(() => this.#spinnerService.hide());
//   }

public stopEleicao(eleicaoId: number): void {
  this.#spinnerService.show();

  this.#eleicaoService
    .encerrarEleicao(eleicaoId)
    .subscribe({
      next: (finish: boolean) => {
        this.#toastrService.success("Eleições finalizadas", "Sucesso!");
      },

      error: (error: any) => {
        this.#toastrService.error("Falha ao encerrar Eleição", "Erro!");
        console.error(error);
      },
   })
    .add(() => this.#spinnerService.hide());
  }

  public startEleicao(): void {
    this.#spinnerService.show();

    this.#eleicaoService
      .iniciarEleicao()
      .subscribe({
        next: (start: boolean) => {
          this.#toastrService.success("Eleições iniciadas", "Sucesso!");
        },

        error: (error: any) => {
          this.#toastrService.error("Falha ao iniciar Eleição", "Erro!");
          console.error(error);
        },
     })
      .add(() => this.#spinnerService.hide());
    }
}
