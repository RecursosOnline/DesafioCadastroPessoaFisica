import { RouterModule, Routes } from '@angular/router';
import { PessoaFisicaCreateComponent } from './pessoa-fisica-create/pessoa-fisica-create.component';
import { NgModule } from '@angular/core';
import { PessoaFisicaComponent } from './pessoa-fisica/pessoa-fisica.component';
import { PessoaFisicaEditComponent } from './pessoa-fisica-edit/pessoa-fisica-edit.component';
export const routes: Routes = [
    { path: 'pessoa-fisica-edit/:pessoaFisicaId', component: PessoaFisicaEditComponent },
    { path: 'pessoa-fisica-create', component: PessoaFisicaCreateComponent },
    { path: '', redirectTo: 'pessoa-fisica', pathMatch: 'full' },
    { path: 'pessoa-fisica', component: PessoaFisicaComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }