import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { Observable, take } from 'rxjs';
import { Partido } from '../../shared/models/interfaces/partido/Partido';

@Injectable({
  providedIn: 'root'
})
export class PartidoService {
#http = inject(HttpClient);

  baseURL = `${environment.apiURL}Partidos/`;

  public getPartidos(): Observable<Partido[]> {
      return this.#http.get<Partido[]>(this.baseURL).pipe(take(3));
  }

  public getPartidoById(id: number): Observable<Partido> {
    return this.#http.get<Partido>(`${this.baseURL}${id}`).pipe(take(3));
  }

  public createPartido(partido: Partido): Observable<Partido> {
    return this.#http.post<Partido>(this.baseURL, partido).pipe(take(3));
  }

  public savePartido(partido: Partido): Observable<Partido> {
    return this.#http
      .put<Partido>(`${this.baseURL}${partido.id}`, partido)
      .pipe(take(3));
  }

  public deletePartido(partidoId: number): Observable<any> {
    return this.#http
      .delete(`${this.baseURL}${partidoId}?partido=${partidoId}`)
      .pipe(take(3));
  }
}
