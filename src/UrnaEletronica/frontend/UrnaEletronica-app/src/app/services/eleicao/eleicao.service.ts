import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { Observable, take } from 'rxjs';
import { Eleicao, ResultadoEleicao } from '../../shared/models/interfaces/eleicao';

@Injectable({
  providedIn: 'root'
})
export class EleicaoService {
  #http = inject(HttpClient);

  baseURL = `${environment.apiURL}Eleicoes/`;

  public iniciarEleicao(): Observable<boolean> {
      return this.#http.post<boolean>(`${this.baseURL}iniciarEleicao`, null).pipe(take(3));
  }

  public encerrarEleicao(eleicaoId: number): Observable<boolean> {
    return this.#http.post<boolean>(`${this.baseURL}${eleicaoId}/encerrarEleicao`, true).pipe(take(3));
  }

  public getEleicoes(): Observable<Eleicao[]> {
    return this.#http.get<Eleicao[]>(this.baseURL).pipe(take(3));
  }

  public apurarVencedor(eleicaoId: number): Observable<ResultadoEleicao[]> {
    return this.#http.post<ResultadoEleicao[]>(`${this.baseURL}${eleicaoId}/calcularVencedor`, null).pipe(take(3));
  }

}
