import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CidadeService } from '../../../services/cidade/cidade.service';
import { Cidade } from '../../../shared/models/interfaces/cidade/Cidade';
import { FormValidator } from '../../../util/class';

@Component({
  selector: 'app-cidade-detalhe',
  templateUrl: './cidade-detalhe.component.html',
  styleUrls: ['./cidade-detalhe.component.scss']
})
export class CidadeDetalheComponent implements OnInit {

  #activevateRouter = inject(ActivatedRoute);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);

  #cidadeService = inject(CidadeService);

  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formCidade = {} as FormGroup;

  public cidade = {} as Cidade;
  public cidadeParam: any = "";

  public editMode: boolean = false;

  public get ctrF(): any {
    return this.formCidade.controls;
  }

  ngOnInit() {
    this.formValidator();

    this.cidadeParam = this.#activevateRouter.snapshot.paramMap.get("id");
    this.editMode = this.cidadeParam != null ? true : false;

    if (this.editMode) this.getCidade();
  }

  public formValidator(): void {
    this.formCidade = this.#formBuilder.group({
      cidadeId: [""],
      nome: [
        "",
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(100),
        ],
      ],
      siglaEstado: ["", Validators.required,],
      nomeEstado: ["", Validators.required,],
      qtdHabitantes: ["", Validators.required,],
    });
  }

  public fieldValidator(campoForm: FormControl): any {
    return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
    return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  public clearForm(): void {
    this.formCidade.reset();
  }

  public saveChange(): void {
    if (this.formCidade.valid)
      if (!this.editMode) {
        this.novaCidade();
      } else {
        this.salvarCidade();
      }
  }

  public getCidade(): void {
    this.#spinnerService.show();

    this.#cidadeService
      .getCidadeById(+this.cidadeParam)
      .subscribe({
        next: (cidade: Cidade) => {
          this.cidade = cidade;
          this.formCidade.patchValue(this.cidade);
          this.ctrF.cidadeId.setValue(this.cidade.id);
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar Cidade", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public novaCidade(): void {
    this.#spinnerService.show();

    this.cidade = { ...this.formCidade.value };

    this.#cidadeService
      .createCidade(this.cidade)
      .subscribe({
        next: (novaCidade: Cidade) => {
          this.#toastrService.success("Caidade cadastrado!", "Sucesso!");
          window.location.reload;
          this.#router.navigateByUrl(
            `/pages/cidades/detalhe/${novaCidade.id}`
          );
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao cadastrar Cidade", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public salvarCidade(): void {
    this.#spinnerService.show();

    this.cidade = {
      id: this.ctrF.cidadeId.value,
      ...this.formCidade.value,
    };

    this.#cidadeService
      .saveCidade(this.cidade)
      .subscribe({
        next: (caidade: Cidade) => {
          this.#toastrService.success("Caidade Atualizado!", "Sucesso!");
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao atualizar Cidade.", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
    }
}
