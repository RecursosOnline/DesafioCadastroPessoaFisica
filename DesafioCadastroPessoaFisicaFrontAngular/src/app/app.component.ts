import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
@Component({
  selector: 'app-root',
  standalone: true,
  styleUrl: './app.component.css',
  imports: [
    RouterOutlet,
    MatSlideToggleModule,
    MatButtonModule
],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'Cadastro de Pessoa Fisica';
}
