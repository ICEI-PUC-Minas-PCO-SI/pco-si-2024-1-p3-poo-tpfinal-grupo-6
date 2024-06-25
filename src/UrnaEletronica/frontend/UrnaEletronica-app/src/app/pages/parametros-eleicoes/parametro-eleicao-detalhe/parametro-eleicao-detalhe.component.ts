import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ParametroEleicaoService } from '../../../services/parametroEleicao/parametroEleicao.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ParametroEleicao } from '../../../shared/models/interfaces/parametroEleicao';
import { FormValidator } from '../../../util/class';
import { CidadeService } from '../../../services/cidade/cidade.service';
import { Cidade } from '../../../shared/models/interfaces/cidade';

@Component({
  selector: 'app-parametro-eleicao-detalhe',
  templateUrl: './parametro-eleicao-detalhe.component.html',
})
export class ParametroEleicaoDetalheComponent implements OnInit {
  #activevateRouter = inject(ActivatedRoute);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);

  #parametroService = inject(ParametroEleicaoService);
  #cidadeService = inject(CidadeService);

  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formParametro = {} as FormGroup;

  public parametroEleicao = {} as ParametroEleicao;
  public parametroEleicaoParam: any = "";

  public cidades = [] as Cidade[];

  public editMode: boolean = false;

  public get ctrF(): any {
    return this.formParametro.controls;
  }

  ngOnInit() {
    this.formValidator();

    this.parametroEleicaoParam = this.#activevateRouter.snapshot.paramMap.get("id");
    this.editMode = this.parametroEleicaoParam != null ? true : false;
    console.log(this.parametroEleicao, this.editMode)
    if (this.editMode) this.getParametro()

    this.getCidades();
  }

  public formValidator(): void {
    this.formParametro = this.#formBuilder.group({
      cidadeId: ["", Validators.required],
      numParam: [""],
      primeiroTurno: [false],
      segundoTurno: [false, Validators.required,],
      qtdVotosSomentePrimeiroTurno: [200000, Validators.required,],
      qtdCadeiras: [9, Validators.required,],
      dataEleicaoPrimeiroTurno: ["", Validators.required,],
      dataEleicaoSegundoTurno: ["", Validators.required,],
      turno: ["Primeiro"]
    });
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  public clearForm(): void {
    this.formParametro.reset();
  }

  public saveChange(): void {
    if (this.formParametro.valid)
      if (!this.editMode) {
        this.novoParametro();
      } else {
        this.salvarParametro();
      }
  }

  public getParametro(): void {
    this.#spinnerService.show();

    console.log(this.parametroEleicao)
    this.#parametroService
      .getParametroById(this.parametroEleicaoParam)
      .subscribe({
        next: (parametro: ParametroEleicao) => {
          this.parametroEleicao = parametro;
          this.formParametro.patchValue(this.parametroEleicao);
          this.ctrF.numParam.setValue(this.parametroEleicao.id);
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar Parametro", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public novoParametro(): void {
    this.#spinnerService.show();

    this.parametroEleicao = { ...this.formParametro.value };

    this.#parametroService
      .createParametro(this.parametroEleicao)
      .subscribe({
        next: (novoParametro: ParametroEleicao) => {
          this.#toastrService.success("Parâmetro cadastrado!", "Sucesso!");
          window.location.reload;
          this.#router.navigateByUrl(
            `/pages/parametrosEleicoes/detalhe/${novoParametro.id}`
          );
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao cadastrar Parmâmetro Eleição", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public salvarParametro(): void {
    this.#spinnerService.show();

    this.parametroEleicao = {
      id: this.ctrF.numParam.value,
      ...this.formParametro.value,
    };

    this.#parametroService
      .saveParametro(this.parametroEleicao)
      .subscribe({
        next: (parametro: ParametroEleicao) => {
          this.#toastrService.success("Parametro Atualizado!", "Sucesso!");
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao atualizar Parametro.", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
    }

  public verificarTurno(): void {
    this.ctrF.primeiroTurno.setValue(this.ctrF.turno.value == "Primeiro" ? true : false)
    this.ctrF.segundoTurno.setValue(this.ctrF.truno.value == "Segundo" ? true : false)
  }

  public getCidades(): void {
    this.#spinnerService.show();

    this.#cidadeService
      .getCidades()
      .subscribe({
        next: (cidades: Cidade[]) => {
          this.cidades = cidades;
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar Cidades", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public verificarCidade(): void {
    this.#spinnerService.show();

    this.#cidadeService
      .getCidadeById(this.ctrF.cidadeId.value)
      .subscribe({
        next: (cidade: Cidade) => {
           this.ctrF.cidadeId.setValue(cidade.id)
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar Partidos", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }
}
