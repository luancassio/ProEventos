import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import Swal from 'sweetalert2';

import { Evento } from 'src/app/core/models/interface/IEvento';
import { EventoService } from 'src/app/services/evento.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  public eventos: Evento[] = [];
  public eventosFilter: any = [];
  private _filtroPesquisa: string = '';
  public esconderImgame: boolean = true;
  public eventoId:number = 0;

  // Modal
  public modalRef?: BsModalRef;

  get filtroPesquisa (){ return this._filtroPesquisa }
  set filtroPesquisa (value: string ){ 
    this._filtroPesquisa = value ;
    this.eventosFilter = this.filtroPesquisa ? 
    this.filterEventos(this.filtroPesquisa) : this.eventos;
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private router: Router,
) { }

  ngOnInit() {
    this.getEventos();
        // /** spinner starts on init */
        // this.spinner.show();
        // setTimeout(() => {
        //   /** spinner ends after 5 seconds */
        //   this.spinner.hide();
        // }, 5000);
  }

  public getEventos(): void{
 
    Swal.fire('Carregando lista de eventos!');
    Swal.showLoading();
    this.eventoService.getEventos().subscribe((eventos: Evento[]) => {
      this.eventos = eventos;
      this.eventosFilter = this.eventos;
    }, err =>{ 
      console.error(err);
      Swal.fire('Error','Não foi possível carregar a lista!', 'error');
      Swal.hideLoading();
    }).add(() => Swal.close());
  }

  // metodo pra filtra na busca por tema ou local
  public filterEventos(valor: string): Evento[]{
      let filtro = valor.toLocaleLowerCase();
      return this.eventos.filter((evento: { tema: string; local: string; }) =>
         evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1 || 
         evento.local.toLocaleLowerCase().indexOf(filtro) !== -1
      );
  }

  openModal(template: TemplateRef<any>, eventoId: number) {
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
 
  mostrarImagem(imagem: string):string{
    return (imagem !== '' 
    ? `${environment.apiURL}Resources/Images/${imagem}` 
    : 'assets/imagem/semImagem.png');
  }

  confirmar(eventoId: number): void {
    this.eventoId = eventoId;
    Swal.fire({
      title: 'Deletar Evento?',
      text: `Deseja deletar o evento ${this.eventoId}`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim',
      cancelButtonText: 'Não'
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire('Deletando Evento!');
        Swal.showLoading();
        this.eventoService.deleteEvento(this.eventoId).subscribe((response: any) => {
          if (response.message === 'Deletado') {
              Swal.hideLoading();
              Swal.fire('Deletado!',`Evento ${this.eventoId} deletado com Sucesso.`,'success')
                  .then(() => {this.getEventos();});
            }
          });
      }
    }).catch((err) =>{      
      Swal.fire('Erro', `Erro ao  deletar evento, erro: ${{err}}!`, 'error');
      Swal.hideLoading();
    });
      
 
  }
 
  recusar(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number){
    this.router.navigate([`eventos/detalhe/${id}`]);      
  }
}
