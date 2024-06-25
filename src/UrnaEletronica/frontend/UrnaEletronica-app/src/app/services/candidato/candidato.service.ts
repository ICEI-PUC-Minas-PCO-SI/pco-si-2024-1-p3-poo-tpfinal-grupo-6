import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { Observable, take } from 'rxjs';
import { Candidato } from '../../shared/models/interfaces/candidato';

@Injectable()
export class CandidatoService {
  #http = inject(HttpClient)

  baseURL =`${environment.apiURL}Candidatos/`;

  public getCandidatos(): Observable<Candidato[]> {
    return this.#http.get<Candidato[]>(this.baseURL).pipe(take(3));
  }

  public getCandidatoById(candidatoId: number): Observable<Candidato> {
    return this.#http
      .get<Candidato>(`${this.baseURL}${candidatoId}`)
      .pipe(take(3));
  }

  public createCandidato(candidato: Candidato): Observable<Candidato> {
    return this.#http.post<Candidato>(this.baseURL, candidato).pipe(take(3));
  }

  public saveCandidato(candidato: Candidato): Observable<Candidato> {
    return this.#http
      .put<Candidato>(`${this.baseURL}${candidato.id}`, candidato)
      .pipe(take(3));
  }

  public deleteCandidato(candidatoId: number): Observable<any> {
    return this.#http
      .delete(`${this.baseURL}${candidatoId}?candidato=${candidatoId}`)
      .pipe(take(3));
  }

  public registrarVoto(candidatoId: number): Observable<Candidato> {
    return this.#http.post<Candidato>(`${this.baseURL}${candidatoId}/VotoOnline`, null)
      .pipe(take(1));
  }
}
