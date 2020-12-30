import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  ranks: string[] = ["pracownik", "kierownik", "admin"];
  model: any = {};
  @Output() cancelRegister = new EventEmitter();

  constructor(private authService: AuthService, private alertify: AlertifyService, public router: Router) { }

  ngOnInit() {
  }

  register()
  {
    this.authService.register(this.model).subscribe( () => {
     this.alertify.success('Rejestracja udana');
    }, error => {
      this.alertify.error(error);
    });

  }

  checkRole()
  {
    return this.authService.checkRole();
  }


  backToPrevious()
  {
    this.router.navigate(['/workers']);
  }

}
