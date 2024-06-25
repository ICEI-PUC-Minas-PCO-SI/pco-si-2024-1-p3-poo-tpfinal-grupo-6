import { Component, OnInit, inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { EleicaoService } from '../../../services/eleicao/eleicao.service';
import { Eleicao, ResultadoEleicao } from '../../../shared/models/interfaces/eleicao';

@Component({
  selector: 'app-home-apuracao',
  templateUrl: './home-apuracao.component.html',
})
export class HomeApuracaoComponent implements OnInit {
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);
  #eleicaoService = inject(EleicaoService);

  public eleicoes = [] as Eleicao[]
  public eleicoesAtivas = [] as Eleicao[]
  public resultados = [] as ResultadoEleicao[]

  ngOnInit() {
  }

  public apurarVotos(): void {
    this.#spinnerService.show();

    this.#eleicaoService
      .getEleicoes()
      .subscribe({
        next: (eleicoes: Eleicao[]) => {
          this.eleicoes = eleicoes.sort((a, b) => b.id - a.id);
          this.eleicoesAtivas = this.eleicoes.slice(0, 2);
          console.log(this.eleicoesAtivas)

//          this.eleicoesAtivas.forEach(eleicao => {
            this.calcularVencedor(7, )
//          });
        },
        error: (error: any) => {
          console.log("aqui 2");
          this.#toastrService.error("Erro ao recuperar eleições", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public calcularVencedor(eleicaoId: number): void {
    this.#eleicaoService
      .apurarVencedor(eleicaoId)
      .subscribe({
        next: (resultados: ResultadoEleicao[]) => {
          this.resultados = resultados
        },
        error: (error: any) => {
          console.log("aqui 2");
          this.#toastrService.error("Erro ao registra voto", "Erro!");
          console.error(error);
        },
       })
      .add(() => this.#spinnerService.hide());
  }

}
