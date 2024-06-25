import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { Observable, take } from 'rxjs';
import { Cidade } from '../../shared/models/interfaces/cidade/Cidade';
import { ParametroEleicao } from '../../shared/models/interfaces/parametroEleicao';

@Injectable({
  providedIn: 'root'
})
export class ParametroEleicaoService {
#http = inject(HttpClient);

  baseURL = `${environment.apiURL}ParametrosEleicoes/`;

  public getParametros(): Observable<ParametroEleicao[]> {
      return this.#http.get<ParametroEleicao[]>(this.baseURL).pipe(take(3));
  }

  public getParametroById(parametroId: number): Observable<ParametroEleicao> {
    return this.#http.get<ParametroEleicao>(`${this.baseURL}${parametroId}`).pipe(take(3));
  }

  public createParametro(parametro: ParametroEleicao): Observable<ParametroEleicao> {
    return this.#http.post<ParametroEleicao>(this.baseURL, parametro).pipe(take(3));
  }

  public saveParametro(parametro: ParametroEleicao): Observable<ParametroEleicao> {
    return this.#http
      .put<ParametroEleicao>(`${this.baseURL}${parametro.id}`, parametro)
      .pipe(take(3));
  }

  public deleteParametro(parametroId: number): Observable<any> {
    return this.#http
      .delete(`${this.baseURL}${parametroId}?parametro=${parametroId}`)
      .pipe(take(3));
  }
}
