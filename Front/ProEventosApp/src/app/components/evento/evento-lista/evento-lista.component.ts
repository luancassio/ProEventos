import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/core/models/interface/IEvento';
import { EventoService } from 'src/app/services/evento.service';

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
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService) { }

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
    this.spinner.show();
    this.eventoService.getEventos().subscribe((eventos: Evento[]) => {
      this.eventos = eventos;
      this.eventosFilter = this.eventos;
    }, err =>{ console.error(err) }).add(() => this.spinner.hide());
    // this.spinner.hide();
  }

  // metodo pra filtra na busca por tema ou local
  public filterEventos(valor: string): Evento[]{
      let filtro = valor.toLocaleLowerCase();
      return this.eventos.filter((evento: { tema: string; local: string; }) =>
         evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1 || 
         evento.local.toLocaleLowerCase().indexOf(filtro) !== -1
      );
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
 
  confirmar(): void {
    this.modalRef?.hide();
    this.toastr.success('O evento foi deletado com sucesso', 'Deletado');
  }
 
  recusar(): void {
    this.modalRef?.hide();
    this.toastr.error('=(', 'Major Error');
  }

  detalheEvento(id: number){
    this.router.navigate([`eventos/detalhe/${id}`]);      
  }
}
