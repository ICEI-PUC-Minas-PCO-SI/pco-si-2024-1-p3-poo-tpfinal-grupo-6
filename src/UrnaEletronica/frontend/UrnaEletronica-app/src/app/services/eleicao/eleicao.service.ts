import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { Observable, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EleicaoService {
  #http = inject(HttpClient);

  baseURL = `${environment.apiURL}Eleicoes/`;

  public iniciarEleicao(): Observable<boolean> {
      return this.#http.get<boolean>(`${this.baseURL}iniciarEleicao`).pipe(take(3));
  }

  // public createParametro(parametro: ParametroEleicao): Observable<ParametroEleicao> {
  //   return this.#http.post<ParametroEleicao>(this.baseURL, parametro).pipe(take(3));
  // }

  // public saveParametro(parametro: ParametroEleicao): Observable<ParametroEleicao> {
  //   return this.#http
  //     .put<ParametroEleicao>(`${this.baseURL}${parametro.id}`, parametro)
  //     .pipe(take(3));
  // }

  // public deleteParametro(parametroId: number): Observable<any> {
  //   return this.#http
  //     .delete(`${this.baseURL}${parametroId}?parametro=${parametroId}`)
  //     .pipe(take(3));
  // }

}
