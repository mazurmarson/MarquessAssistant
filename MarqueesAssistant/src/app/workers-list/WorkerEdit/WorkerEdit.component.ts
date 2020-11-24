import { FnParam } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { WorkerService } from 'src/app/_services/worker.service';

@Component({
  selector: 'app-WorkerEdit',
  templateUrl: './WorkerEdit.component.html',
  styleUrls: ['./WorkerEdit.component.css']
})
export class WorkerEditComponent implements OnInit {

  model: any ={};
  isLoaded: boolean = false;
  id = this.authService.getWorkerId();
  editForm: FormGroup;
  

  constructor(private authService: AuthService,private alertify: AlertifyService, private workerService: WorkerService, private route: ActivatedRoute, private fb: FormBuilder) { }

  ngOnInit() {
  //  this.editForm = new FormGroup({
     
  //    login: new FormControl(this.model.login, [Validators.required, Validators.minLength(4)]),
     
  //  }, this.comparePasswords);
    this.getProfile();
  }

  matchFunction()
  {
    if(this.model.password === this.model.confirmPassword)
    {
      
      return true;
    }
    else
    {
      return false;
    }
  }

  // createEditForm()
  // {
  //   this.editForm = this.fb.group( {
  //     login: ['', Validators.required],
  //     password: ['',[Validators.required, Validators.minLength(6)]],
  //     confirmPassword: ['', Validators.required]
  //   }, { validator: this.comparePasswords });
  // }

  comparePasswords(fg: FormGroup)
  {
    return fg.get('password').value === fg.get('confirmPassword'). value ? null : { mismatach : true };
  }

  getProfile()
  {
    this.workerService.getWorker(this.id).subscribe( (worker: any) => {
      this.model = worker;
      this.isLoaded = true;
    });
  }

  editProfile()
  {

    if(this.matchFunction())
    {
      this.authService.editProfileByWorker(this.model).subscribe( () => {
        this.alertify.success('Edytowano profil');
    
      }, error => {
        this.alertify.error(error);
      });
    }
    else
    {
      this.alertify.error('Hasła nie są takie same');
    }
    

    
  }

}
