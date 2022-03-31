import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../core/models/interface/IEvento';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class EventoService {

  apiURL= environment.apiURL+'api/Evento';

  constructor(private http: HttpClient) {}
  
  public getEventos(): Observable<Evento[]>{
    return this.http
               .get<Evento[]>(this.apiURL)
               .pipe(take(1));
  }

  public getEventosByTema(tema: string): Observable<Evento[]>{
    return this.http
               .get<Evento[]>(`${this.apiURL}/${tema}/tema`)
               .pipe(take(1));
  }

  public getEventoById(id: number): Observable<Evento>{
    return this.http
               .get<Evento>(`${this.apiURL}/${id}`)
               .pipe(take(1));
  }

  public postEvento(evento: Evento): Observable<Evento>{
    return this.http
               .post<Evento>(this.apiURL, evento)
               .pipe(take(1));
  }

  public putEvento(evento: Evento, eventoId: number): Observable<Evento>{
    return this.http
               .put<Evento>(`${this.apiURL}/${eventoId}`, evento)
               .pipe(take(1));
  }

  public deleteEvento(eventoId: number): Observable<any>{
    return this.http
               .delete<any>(`${this.apiURL}/${eventoId}`)
               .pipe(take(1));
  }

  public postUpload(eventoId: number, file: File | any): Observable<Evento>{
    const fileToUpload = file[0] as File
    const formData = new FormData();
    formData.append('file', fileToUpload);
    return this.http
               .post<Evento>(`${this.apiURL}/upload-image/${eventoId}`, formData)
               .pipe(take(1));
  }

}
