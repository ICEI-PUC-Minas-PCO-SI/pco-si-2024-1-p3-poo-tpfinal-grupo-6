import { Component, inject } from '@angular/core';
import { Candidato } from '../../../shared/models/interfaces/candidato';
import { NgxSpinnerService } from 'ngx-spinner';
import { CandidatoService } from '../../../services/candidato';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

 @Component({
   selector: 'app-home-page',
   templateUrl: './home-page.component.html'
 })
 export class HomePageComponent {
  #candidatoService = inject(CandidatoService);
  #router = inject(Router);
  #spinnerService = inject(NgxSpinnerService);
  #toastrService = inject(ToastrService);

  public candidatos = [] as Candidato[];
  public candidato = {} as Candidato;
  public candidatosPrefeitos = [] as Candidato[]
  public candidatosVereadores = [] as Candidato[];

  public votouPrefeito: boolean = false;
  public votouVereador: boolean = false;

  public currentUrl = this.#router.url;

   public ngOnInit(): void {
     this.getCandidatos();

     if (this.votouPrefeito && this.votouVereador) {
      console.log("aquiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii")
      this.votouPrefeito = this.votouVereador = false
      setTimeout(() => {
        this.#router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.#router.navigate([this.currentUrl]);
        });
      }, 10000);
    }
   }

  public getCandidatos(): void {
    this.#spinnerService.show();

    this.#candidatoService
      .getCandidatos()
      .subscribe({
        next: (candidatos: Candidato[]) => {
          this.candidatos = candidatos;

          this.candidatosPrefeitos = this.candidatos.filter(candidato => candidato.tipoCandidatura == "Prefeito")
          this.candidatosVereadores = this.candidatos.filter(candidato => candidato.tipoCandidatura == "Vereador")
        },
         error: (error: any) => {
           console.log("aqui 2");
           this.#toastrService.error("Erro ao carregar Acervos", "Erro!");
           console.error(error);
         },
      })
      .add(() => this.#spinnerService.hide());
  }

  public Votar(candidatoId: number, tipoCandidatura: string): void {
    this.#spinnerService.show();

    if (tipoCandidatura == "Prefeito")
      this.votouPrefeito = true
    else
      this.votouVereador = true

    this.#candidatoService
      .registrarVoto(candidatoId)
      .subscribe({
        next: (candidato: Candidato) => {
          this.candidato = candidato
          if (candidato.tipoCandidatura == "Prefeito")
            this.votouPrefeito = true
          else
            this.votouVereador = true

            this.#router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
              this.#router.navigate([this.currentUrl]);

          });

          this.#toastrService.success("Voto registrado", "Sucesso!");

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
