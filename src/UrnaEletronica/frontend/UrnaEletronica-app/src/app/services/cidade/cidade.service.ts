import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { Observable, take } from 'rxjs';
import { Cidade } from '../../shared/models/interfaces/cidade/Cidade';

@Injectable({
  providedIn: 'root'
})
export class CidadeService {
#http = inject(HttpClient);

  baseURL = `${environment.apiURL}Cidades/`;

  public getCidades(): Observable<Cidade[]> {
      return this.#http.get<Cidade[]>(this.baseURL).pipe(take(3));
  }

  public getCidadeById(id: number): Observable<Cidade> {
    return this.#http.get<Cidade>(`${this.baseURL}${id}`).pipe(take(3));
  }

  public createCidade(cidade: Cidade): Observable<Cidade> {
    return this.#http.post<Cidade>(this.baseURL, cidade).pipe(take(3));
  }

  public saveCidade(cidade: Cidade): Observable<Cidade> {
    return this.#http
      .put<Cidade>(`${this.baseURL}${cidade.id}`, cidade)
      .pipe(take(3));
  }

  public deleteCidade(cidadeId: number): Observable<any> {
    return this.#http
      .delete(`${this.baseURL}${cidadeId}?cidade=${cidadeId}`)
      .pipe(take(3));
  }
}
