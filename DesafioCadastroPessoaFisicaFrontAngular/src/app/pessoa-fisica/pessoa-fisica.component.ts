import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { MatTableModule, MatTable } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { PessoaFisicaDataSource, PessoaFisicaItem } from './pessoa-fisica-datasource';
import { CadastroService } from '../servicos/cadastro.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-pessoa-fisica',
  templateUrl: './pessoa-fisica.component.html',
  styleUrl: './pessoa-fisica.component.css',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule]
})
export class PessoaFisicaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<PessoaFisicaItem>;  
    
  dataSource: PessoaFisicaItem[] = [];

  private cadastroService =  inject(CadastroService);


  ngOnInit(){
    this
    .cadastroService
    .listaPessoaFisica$
    .subscribe((data)=>{
      console.log(data);
      this.dataSource = data;
    });

    //listaPessoaFisica$
  }
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name', 'cpf','dataNascimento','valorRenda','acao'];

  ngAfterViewInit(): void {

    this.table.dataSource = this.dataSource;
  }
}
