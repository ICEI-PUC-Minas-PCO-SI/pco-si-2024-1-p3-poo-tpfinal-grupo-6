import { Injectable, inject } from '@angular/core';
import { environment } from '../../../assets/environments';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Usuario } from '../../shared/models/interfaces/usuario';
import { Candidato } from '../../shared/models/interfaces/candidato';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  #http = inject(HttpClient)

  baseURL = environment.apiURL + 'Uploads/'

  public salvarFotoUsuario(file: File[]): Observable<Usuario> {
    const fileUpload = file[0] as File;
    const formData = new FormData();

    formData. append('file', fileUpload);

    return this.#http
    .post<Usuario>(`${this.baseURL}upload-user-photo`, formData)
    .pipe(take(1));
  }

  public salvarFotoCandidato(file: File[], candidatoId: number): Observable<Candidato> {
    const fileUpload = file[0] as File;
    const formData = new FormData();

    formData. append('file', fileUpload);

    return this.#http
    .post<Candidato>(`${this.baseURL}upload-candidato-photo/${candidatoId}`, formData)
    .pipe(take(1));
  }
}
