import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavigationBarComponent } from "./Components/navigation-bar/navigation-bar.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavigationBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'SMS_UI';

  constructor(private router: Router) { }

  
}
