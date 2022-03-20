import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  @Input() titulo: string | undefined;
  // @Input() subtitulo: string | undefined;
  @Input() iconClass: string | undefined;
  @Input() btnListar: boolean = false;
  constructor(private router: Router) { }

  ngOnInit() {
  }

  listar(){

    this.router.navigate([`/${this.titulo?.toLocaleLowerCase()}/lista`])
  }

}
