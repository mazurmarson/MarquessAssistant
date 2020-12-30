import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { WorkerService } from 'src/app/_services/worker.service';

@Component({
  selector: 'app-worker-edit-by-admin',
  templateUrl: './worker-edit-by-admin.component.html',
  styleUrls: ['./worker-edit-by-admin.component.css']
})
export class WorkerEditByAdminComponent implements OnInit {

  model: any ={};
  isLoaded: boolean = false;
  id: number;
  ranks: string[] = ["pracownik", "kierownik", "admin"];

  constructor(private authService: AuthService,private alertify: AlertifyService, private workerService: WorkerService, private route: ActivatedRoute, public router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.getProfile();
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

  backToPrevious()
  {
    this.router.navigate(['/workers']);
  }

}
