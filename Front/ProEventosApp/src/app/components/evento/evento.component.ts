import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from 'src/app/core/models/interface/IEvento';
import { EventoService } from 'src/app/services/evento.service';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-evento',
  templateUrl: './evento.component.html',
  styleUrls: ['./evento.component.scss']
})
export class EventoComponent implements OnInit {
  
  
  ngOnInit(): void {}

  constructor(){}

 

}
