import { Component, inject } from '@angular/core';

import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { CadastroService } from '../servicos/cadastro.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PessoaFisicaDataSource } from '../pessoa-fisica/pessoa-fisica-datasource';


@Component({
  selector: 'app-pessoa-fisica-edit',
  templateUrl: './pessoa-fisica-edit.component.html',
  styleUrl: './pessoa-fisica-edit.component.css',
  standalone: true,
  imports: [
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    ReactiveFormsModule
  ]
})
export class PessoaFisicaEditComponent {
  private cadastroService =inject(CadastroService);
  private route =  inject(ActivatedRoute);
  private router =  inject(Router);
  private fb = inject(FormBuilder);
  pessoaFisicaForm = this.fb.group({
    nomeCompleto: ['', Validators.required],
    dataNascimento: ['', Validators.required],
    valorRenda: ['', Validators.required],
    cpf: ['', Validators.compose([
      Validators.required, Validators.minLength(11), Validators.maxLength(14)])
    ]
  });
  private pessoaFisicaId:string = "";
 constructor(){
  this.pessoaFisicaId = this.route.snapshot.paramMap.get('pessoaFisicaId') || "";
 }
  ngOnInit(){
    let pessoaFisicaId = new String(this.route.snapshot.paramMap.get('pessoaFisicaId') || "");      
    this
    .cadastroService
    .getPessoa(this.pessoaFisicaId)
    .subscribe((pessoaFisica)=>{
      console.table(pessoaFisica);
      this.pessoaFisicaForm.controls["nomeCompleto"].setValue(pessoaFisica?.nomeCompleto || null);
      this.pessoaFisicaForm.controls["dataNascimento"].setValue(pessoaFisica?.dataDeNascimento || null);
      this.pessoaFisicaForm.controls["valorRenda"].setValue(pessoaFisica?.valorDaRenda || null);
      this.pessoaFisicaForm.controls["cpf"].setValue(pessoaFisica?.cpf || null);
      
    });

    //listaPessoaFisica$
  }
  onSubmit(): void {
    if(this.pessoaFisicaForm.invalid)
      {
        alert("Preencha todos os campos!");
        return;        
      }
    let pessoaFisicaId = new String(this.route.snapshot.paramMap.get('pessoaFisicaId') || "");      
    var pessoaFisica = { 
      pessoaFisicaId: pessoaFisicaId.toString(), 
      nomeCompleto: this.pessoaFisicaForm.get("nomeCompleto")?.value || "", 
      dataDeNascimento: this.pessoaFisicaForm.get("dataNascimento")?.value || "", 
      valorDaRenda: this.pessoaFisicaForm.get("valorRenda")?.value  || "", 
      cpf: this.pessoaFisicaForm.get("cpf")?.value || "" };
      console.log(pessoaFisica);
      this.cadastroService.updatePessoa(pessoaFisica);
      this.cadastroService.getPessoas();
      // console.log("Atualizando lista principal....");
      // this.cadastroService.atualizaListaPrincipal()
      // .subscribe({
      //   next: (v) => console.log(v),
      //   error: (e) => console.error(e),
      //   complete: () => {
      //         console.log("Lista principal atualizada");
      //         this.router.navigate(['/'])
      //           .then(() => {
      //             console.log("Atualizando a pagina");
      //             window.location.reload();
      //         }); 
      //   }
      // });
      
      //this.cadastroService.getPessoas();
      // this.router.navigate(['/'])
      // .then(()=>{
      //   window.location.reload();
      // });   
      //this.router.navigateByUrl("/");   
  }
  onDelete(): void {
    if(confirm("Excluir definitivamente?"))
    {
      let pessoaFisicaId = this.route.snapshot.paramMap.get('pessoaFisicaId') || "";      
      this.cadastroService.deletePessoa(pessoaFisicaId);   
      this.cadastroService.getPessoas();
      // this.router.navigate(['/'])
      // .then(()=>{
      //   window.location.reload();
      // });  
      
    }
  }
}
