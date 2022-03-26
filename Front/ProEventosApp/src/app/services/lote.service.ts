import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Lote } from '../core/models/interface/ILote';

@Injectable()
export class LoteService {

  apiURL= environment.apiURL+'api/Lotes';
  constructor(private http: HttpClient) {}
  
  public getLotesByEventoId(eventoId: number): Observable<Lote[]>{
    return this.http
               .get<Lote[]>(`${this.apiURL}/${eventoId}`)
               .pipe(take(1));
  }


  public SaveLotes(eventoId: number, lotes: Lote[]): Observable<Lote[]>{
    return this.http
               .put<Lote[]>(`${this.apiURL}/${eventoId}`, lotes)
               .pipe(take(1));
  }

  public deleteLote(eventoId: number, loteId: number): Observable<any>{
    console.log(eventoId, loteId)
    return this.http
               .delete<any>(`${this.apiURL}/${eventoId}/${loteId}`)
               .pipe(take(1));
  }

}
