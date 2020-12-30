import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BreakdownComponent } from 'src/app/breakdown/breakdown.component';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';

@Component({
  selector: 'app-equipment-add',
  templateUrl: './equipment-add.component.html',
  styleUrls: ['./equipment-add.component.css']
})
export class EquipmentAddComponent implements OnInit {

  model: any = {};

  constructor(private alertify: AlertifyService, private breakdownServiece: BreakdownService, public router: Router) { }

  ngOnInit() {
  }

  addEquipment()
  {
    this.breakdownServiece.addEquipment(this.model).subscribe( () => {
      this.alertify.success('Dodano sprzęt');
      this.router.navigate(['/equipments']);
    }, error => {
      this.alertify.error(error);
    });
  }

  backToPrevious()
  {
    this.router.navigate(['/equipments']);
  }


}
