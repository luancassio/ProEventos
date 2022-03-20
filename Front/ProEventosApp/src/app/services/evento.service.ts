import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../core/models/interface/IEvento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  apiURL= 'https://localhost:44300/api/Evento';
  constructor(private http: HttpClient) {}
  
  public getEventos(): Observable<Evento[]>{
    return this.http.get<Evento[]>(this.apiURL);
  }

  public getEventosByTema(tema: string): Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.apiURL}/${tema}/tema`);
  }

  public getEventoById(id: number): Observable<Evento>{
    return this.http.get<Evento>(`${this.apiURL}/${id}`);
  }

}
