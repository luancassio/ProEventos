import { Component, ContentChildren, Input, OnChanges, OnInit, QueryList, SimpleChanges, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-table-generic',
  templateUrl: './table-generic.component.html',
  styleUrls: ['./table-generic.component.scss']
})
export class TableGenericComponent implements OnInit, OnChanges {

  @ContentChildren('coluna') colunas!: QueryList<TemplateRef<any>>;
  @ContentChildren('header') headers!: QueryList<TemplateRef<any>>;
  @Input()dados!: Array<any>;
  public coluna: any[] | undefined


  ngOnInit() {} 
  
  constructor() {

  }

   /* Sempre que o @Input() dados for informado no <app-table-generic [dados]="..."
      este método será ativado: */ 
  ngOnChanges(changes: SimpleChanges) {
    if (changes.dados) {
        if(this.dados[0]){
          const primeiroItem = this.dados[0];
          this.coluna = Object.getOwnPropertyNames(primeiroItem);
        }
    }
  }

}
