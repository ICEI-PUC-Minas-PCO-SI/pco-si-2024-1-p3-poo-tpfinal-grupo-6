import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormValidator } from '../../../util/class';
import { NgxSpinnerService } from 'ngx-spinner';
import { UploadService } from '../../../services/upload';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { CandidatoService } from '../../../services/candidato';
import { Candidato } from '../../../shared/models/interfaces/candidato';

@Component({
  selector: 'app-candidato-detalhe',
  templateUrl: './candidato-detalhe.component.html',
  styleUrls: ['./candidato-detalhe.component.scss']
})
export class CandidatoDetalheComponent {

  #activevateRouter = inject(ActivatedRoute);
  #formBuilder = inject(FormBuilder);
  #router = inject(Router);

  #candidatoService = inject(CandidatoService);
  #uploadService = inject(UploadService)

  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public formCandidato = {} as FormGroup;

  public fotoUpload: string = "not-available.png";
  public fotoURL: string = "../../../../assets/images/upload.png";
  public file!: File[];

  public cidades = []
  //public cidades = [] as Cidade[]
  public partidos = []
  //public partidos = [] as Partidos[]

  public candidato = {} as Candidato;
  public candidatoParam: any = "";

  public editMode: boolean = false;
  public temDataNascimento: boolean = true;

  public candidatoImagem: string = "../../../../assets/images/candidato.png"

  public get ctrF(): any {
     return this.formCandidato.controls;
  }

  ngOnInit() {
    this.formValidator();
    this.verificarTipoCandidato();

    this.candidatoParam = this.#activevateRouter.snapshot.paramMap.get("id");
    this.editMode = this.candidatoParam != null ? true : false;

    if (this.editMode) this.getCandidato();
  }

  public formValidator(): void {
    this.formCandidato = this.#formBuilder.group({
      candidatoId: [ 0],
      ehExecutivo: [ ""],
      ehLegislativo: [ ""],
      votosValidos: [""],
      qtdVotos: [ 0],
      opcaoCandidato: ["Executivo", Validators.required],
      nome: ["", [Validators.required, Validators.minLength(10), Validators.maxLength(100)]],
      candidatoReal: ["Sim", Validators.required],
      dataNascimento: [new Date("01/01/2000"), Validators.required],
      tipoCandidatura: ["Prefeito"],
      cidadeId: [0, Validators.required],
      partidoId: [0, Validators.required],
      coligacaoId: [0,],
      nomeColigacao: [""],
    });
  }

  public verificarTipoCandidato(): void {
    this.ctrF.ehExecutivo.setValue(this.ctrF.opcaoCandidato.value == "Executivo")
    this.ctrF.ehLegislativo.setValue(this.ctrF.opcaoCandidato.value == "Legislativo")
    this.ctrF.votosValidos.setValue(this.ctrF.candidatoReal.value == "Sim")
    this.ctrF.tipoCandidatura.setValue(this.ctrF.opcaoCandidato.value == "Executivo" ? "Prefeito" : "Vereador" )
    this.temDataNascimento = this.ctrF.candidatoReal.value == "Sim"
  }

  public fieldValidator(campoForm: FormControl): any {
     return FormValidator.checkFieldsWhithError(campoForm);
  }

  public messageReturned(nomeCampo: FormControl, nomeElemento: string): any {
     return FormValidator.returnMessage(nomeCampo, nomeElemento);
  }

  public clearForm(): void {
    this.formCandidato.reset();
  }

  public saveChange(): void {
    if (this.formCandidato.valid)
      if (!this.editMode) {
        this.novoCandidato();
      } else {
        this.salvarCandidato();
    }
  }

  public getCandidato(): void {
    this.#spinnerService.show();

    this.#candidatoService
      .getCandidatoById(+this.candidatoParam)
      .subscribe({
        next: (candidato: Candidato) => {
          this.candidato = candidato;
          this.formCandidato.patchValue(this.candidato);
          this.ctrF.candidatoId.setValue(this.candidato.id);

          if (candidato.fotoURL)
             this.candidatoImagem = candidato.fotoURL;
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao recuperar Candidato", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public novoCandidato(): void {
    this.#spinnerService.show();

    this.candidato = { ...this.formCandidato.value };

    this.#candidatoService
      .createCandidato(this.candidato)
      .subscribe({
        next: (novoCandidato: Candidato) => {
          this.#toastrService.success("Candidato cadastrado!", "Sucesso!");
          window.location.reload;
          this.#router.navigateByUrl(
            `/pages/candidados/detalhe/${novoCandidato.id}`
          );
        },
        error: (error: any) => {
          this.#toastrService.error("Erro ao cadastrar Candidato", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public salvarCandidato(): void {
    this.#spinnerService.show();

    this.candidato = {id: this.ctrF.patrimonioId.value,
       ...this.formCandidato.value,
    };

    this.#candidatoService
      .saveCandidato(this.candidato)
      .subscribe({
        next: (candidato: Candidato) => {
          this.#toastrService.success("Candidato Atualizado!", "Sucesso!");
        },
        error: (error: any) => {
          this.#toastrService.error("Falha ao atualizar Candidato.", "Erro!");
          console.error(error);
        },
      })
      .add(() => this.#spinnerService.hide());
  }

  public changePhoto(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => (this.fotoUpload = event.target.result);

    this.file = ev.target.files;

    reader.readAsDataURL(this.file[0]);

    this.uplodaPhoto();
  }

  public uplodaPhoto(): void {
    this.#spinnerService.show();

    this.#uploadService
      .salvarFotoCandidato(this.file)
      .subscribe({
        next: () => {
          this.#toastrService.success("Foto atualizada!", "Sucesso!");
//          this.getUsuarioLogado();
//          this.#router.navigateByUrl("/pages/usuarios/perfil");
//          location.replace("/pages/usuarios/perfil");
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
}
