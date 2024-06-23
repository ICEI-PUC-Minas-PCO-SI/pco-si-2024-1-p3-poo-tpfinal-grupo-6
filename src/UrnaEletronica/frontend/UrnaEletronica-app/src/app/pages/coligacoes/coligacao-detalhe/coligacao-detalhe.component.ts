import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ColigacaoService } from '../../../services/coligacao/coligacao.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Coligacao } from '../../../shared/models/interfaces/coligacao';
import { FormValidator } from '../../../util/class';

@Component({
  selector: 'app-coligacao-detalhe',
  templateUrl: './coligacao-detalhe.component.html',
})
export class ColigacaoDetalheComponent implements OnInit {
  #activevateRouter = inject(ActivatedRoute);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);

  #coligacaoService = inject(ColigacaoService);

  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formColigacao = {} as FormGroup;

  public coligacao = {} as Coligacao;
  public coligacaoParam: any = "";

  public editMode: boolean = false;

  public get ctrF(): any {
    return this.formColigacao.controls;
  }

  ngOnInit() {
    this.formValidator();

    this.coligacaoParam = this.#activevateRouter.snapshot.paramMap.get("id");
    this.editMode = this.coligacaoParam != null ? true : false;

    if (this.editMode) this.getColigacao();
  }

  public formValidator(): void {
    this.formColigacao = this.#formBuilder.group({
      coligacaoId: [""],
      nome: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      sigla: ["", Validators.required,],
      qtdVotos: [0, Validators.required,],
    });
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  public clearForm(): void {
    this.formColigacao.reset();
  }

  public saveChange(): void {
    if (this.formColigacao.valid)
      if (!this.editMode) {
        this.novaColigacao();
      } else {
        this.salvarColigacao();
      }
  }

  public getColigacao(): void {
    this.#spinnerService.show();

    this.#coligacaoService
      .getColigacaoById(+this.coligacaoParam)
      .subscribe({
        next: (coligacao: Coligacao) => {
          this.coligacao = coligacao;
          this.formColigacao.patchValue(this.coligacao);
          this.ctrF.coligacaoId.setValue(this.coligacao.id);
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar Coligacao", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public novaColigacao(): void {
    this.#spinnerService.show();

    this.coligacao = { ...this.formColigacao.value };

    this.#coligacaoService
      .createColigacao(this.coligacao)
      .subscribe({
        next: (novaColigacao: Coligacao) => {
          this.#toastrService.success("Coligacao cadastrado!", "Sucesso!");
          window.location.reload;
          this.#router.navigateByUrl(
            `/pages/coligacoes/detalhe/${novaColigacao.id}`
          );
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao cadastrar Coligacao", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public salvarColigacao(): void {
    this.#spinnerService.show();

    this.coligacao = {
      id: this.ctrF.coligacaoId.value,
      ...this.formColigacao.value,
    };

    this.#coligacaoService
      .saveColigacao(this.coligacao)
      .subscribe({
        next: (coligacao: Coligacao) => {
          this.#toastrService.success("Coligacao Atualizado!", "Sucesso!");
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao atualizar Coligacao.", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
    }

}
