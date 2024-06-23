import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { Observable, take } from 'rxjs';
import { Coligacao } from '../../shared/models/interfaces/coligacao';

@Injectable({
  providedIn: 'root'
})
export class ColigacaoService {
#http = inject(HttpClient);

  baseURL = `${environment.apiURL}Coligacoes/`;

  public getColigacoes(): Observable<Coligacao[]> {
      return this.#http.get<Coligacao[]>(this.baseURL).pipe(take(3));
  }

  public getColigacaoById(id: number): Observable<Coligacao> {
    return this.#http.get<Coligacao>(`${this.baseURL}${id}`).pipe(take(3));
  }

  public createColigacao(coligacao: Coligacao): Observable<Coligacao> {
    return this.#http.post<Coligacao>(this.baseURL, coligacao).pipe(take(3));
  }

  public saveColigacao(coligacao: Coligacao): Observable<Coligacao> {
    return this.#http
      .put<Coligacao>(`${this.baseURL}${coligacao.id}`, coligacao)
      .pipe(take(3));
  }

  public deleteColigacao(coligacaoId: number): Observable<any> {
    return this.#http
      .delete(`${this.baseURL}${coligacaoId}?coligacao=${coligacaoId}`)
      .pipe(take(3));
  }
}
