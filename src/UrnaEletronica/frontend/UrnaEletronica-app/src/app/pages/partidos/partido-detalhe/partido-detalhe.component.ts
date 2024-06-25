import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PartidoService } from '../../../services/partido/partido.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Partido } from '../../../shared/models/interfaces/partido';
import { FormValidator } from '../../../util/class';
import { ColigacaoService } from '../../../services/coligacao/coligacao.service';
import { Coligacao } from '../../../shared/models/interfaces/coligacao';

@Component({
  selector: 'app-partido-detalhe',
  templateUrl: './partido-detalhe.component.html',
})
export class PartidoDetalheComponent implements OnInit {

  #activevateRouter = inject(ActivatedRoute);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);

  #partidoService = inject(PartidoService);
  #coligacaoService = inject(ColigacaoService);

  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formPartido = {} as FormGroup;

  public partido = {} as Partido;
  public partidoParam: any = "";

  public coligacoes = [] as Coligacao[];

  public editMode: boolean = false;

  public get ctrF(): any {
    return this.formPartido.controls;
  }

  ngOnInit() {
    this.formValidator();

    this.partidoParam = this.#activevateRouter.snapshot.paramMap.get("id");
    this.editMode = this.partidoParam != null ? true : false;

    if (this.editMode) this.getPartido();

    this.getColigacoes();
  }

  public formValidator(): void {
    this.formPartido = this.#formBuilder.group({
      partidoId: [""],
      nome: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      sigla: ["", Validators.required,],
      coligacaoId: ["", Validators.required,],
    });
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  public clearForm(): void {
    this.formPartido.reset();
  }

  public saveChange(): void {
    if (this.formPartido.valid)
      if (!this.editMode) {
        this.novoPartido();
      } else {
        this.salvarPartido();
      }
  }

  public getPartido(): void {
    this.#spinnerService.show();

    this.#partidoService
      .getPartidoById(+this.partidoParam)
      .subscribe({
        next: (partido: Partido) => {
          this.partido = partido;
          this.formPartido.patchValue(this.partido);
          this.ctrF.partidoId.setValue(this.partido.id);
          this.ctrF.coligacaoId.setValue(this.partido.coligacao.id)
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar Partido", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public novoPartido(): void {
    this.#spinnerService.show();

    this.partido = { ...this.formPartido.value };

    this.#partidoService
      .createPartido(this.partido)
      .subscribe({
        next: (novoPartido: Partido) => {
          this.#toastrService.success("Partido cadastrado!", "Sucesso!");
          window.location.reload;
          this.#router.navigateByUrl(
            `/pages/partidos/detalhe/${novoPartido.id}`
          );
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao cadastrar Partido", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public salvarPartido(): void {
    this.#spinnerService.show();

    this.partido = {
      id: this.ctrF.partidoId.value,
      ...this.formPartido.value,
    };

    this.#partidoService
      .savePartido(this.partido)
      .subscribe({
        next: (partido: Partido) => {
          this.#toastrService.success("Partido Atualizado!", "Sucesso!");
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao atualizar Partido.", "Erro!");
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
          next: (coligacoes: Coligacao[]) => {
            this.coligacoes = coligacoes;
            console.log(coligacoes)
          },
          error: (error: any) => {
            this.#toastrService.error("Falha ao recuperar Coligacoes", "Erro!");
            console.error(error);
          },
        })
        .add(() => this.#spinnerService.hide());
    }
}
