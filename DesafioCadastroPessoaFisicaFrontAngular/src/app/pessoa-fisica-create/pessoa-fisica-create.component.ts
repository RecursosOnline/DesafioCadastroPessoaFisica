import { Component, inject } from '@angular/core';

import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { CadastroService } from '../servicos/cadastro.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-pessoa-fisica-create',
  templateUrl: './pessoa-fisica-create.component.html',
  styleUrl: './pessoa-fisica-create.component.css',
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
export class PessoaFisicaCreateComponent {
  private cadastroService = inject(CadastroService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  pessoaFisicaForm = this.fb.group({
    nomeCompleto: [null, Validators.required],
    dataNascimento: [null, Validators.required],
    valorRenda: [null, Validators.required],
    cpf: [null, Validators.compose([
      Validators.required, Validators.minLength(11), Validators.maxLength(14)])
    ]
  });
  
  onSubmit(): void {
    
    if(this.pessoaFisicaForm.invalid)
      {
        alert("Preencha todos os campos!");
        return;        
      }    
    var pessoaFisica = {       
      nomeCompleto: this.pessoaFisicaForm.get("nomeCompleto")?.value || "", 
      dataDeNascimento: this.pessoaFisicaForm.get("dataNascimento")?.value || "", 
      valorDaRenda: this.pessoaFisicaForm.get("valorRenda")?.value  || "", 
      cpf: this.pessoaFisicaForm.get("cpf")?.value || "",
      pessoaFisicaId: "" };
      console.log(pessoaFisica);
      this.cadastroService.addPessoa(pessoaFisica);
      this.cadastroService.getPessoas();
      this.router.navigate(['/']);

  }
}
