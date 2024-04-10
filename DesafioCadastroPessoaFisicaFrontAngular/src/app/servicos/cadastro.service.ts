import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { PessoaFisicaItem } from '../pessoa-fisica/pessoa-fisica-datasource';
import { Console } from 'console';
@Injectable({
  providedIn: 'root'
})
export class CadastroService {
  private apiUrl = "http://localhost:8080/PessoaFisica";
  
  http = inject(HttpClient);

  listaPessoaFisica$: Observable<Array<PessoaFisicaItem>> = new Observable();
  
  constructor(){
   this.getPessoas();
  }
  getPessoas(): void
  {
    console.log("Refresh....");
    // let result = this.http.get(this.apiUrl);      
    this.listaPessoaFisica$ = this.http.get<PessoaFisicaItem[]>(this.apiUrl);      
    
  }
  getPessoa(pessoaFisicaId:string): Observable<PessoaFisicaItem | undefined>
  {
    console.log("Getting....");    
    return this.listaPessoaFisica$.pipe(map(p => p.find(e => e.pessoaFisicaId === pessoaFisicaId)));
    
  }
  addPessoa(pessoa:PessoaFisicaItem): void
  {
    console.log("Adicionando :" + pessoa);    
    this.http.post(this.apiUrl, pessoa)    
      .subscribe({
        next: (v) => console.log(v),
        error: (e) => console.error(e),
        complete: () => console.info('complete') 
     });
  }
  updatePessoa(pessoa:PessoaFisicaItem): void
  {
    this.http.put(this.apiUrl, pessoa)
      .subscribe({
        next: (v) => console.log(v),
        error: (e) => console.error(e),
        complete: () => console.info('complete') 
      });
  }
  deletePessoa(pessoaFisicaId:string): void
  {
    console.log("Excluindo :" + pessoaFisicaId);    
    this.http.delete(this.apiUrl, {body: {pessoaFisicaId: pessoaFisicaId}})    
    .subscribe({
      next: (v) => console.log(v),
      error: (e) => console.error(e),
      complete: () => console.info('complete') 
    });
  }
}
