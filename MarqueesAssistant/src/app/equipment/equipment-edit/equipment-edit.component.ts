import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Equipment } from 'src/app/_models/equipment';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { BreakdownService } from 'src/app/_services/breakdown.service';

@Component({
  selector: 'app-equipment-edit',
  templateUrl: './equipment-edit.component.html',
  styleUrls: ['./equipment-edit.component.css']
})
export class EquipmentEditComponent implements OnInit {

  id: number;
  model: any;
  isLoaded: boolean = false;

  constructor(private alertify: AlertifyService, private breakdownServiece: BreakdownService, private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.id = params['id'];
      
    });
  }

  ngOnInit() {
    this.getEquipment(this.id);
  }

  getEquipment(id: number)
  {
    this.breakdownServiece.getEquimpment(id).subscribe( (equipment: Equipment) => {
      this.model = equipment;
      this.isLoaded = true;
    } );
  }

  editEquipment()
  {
    this.breakdownServiece.editEquipment(this.model, this.id).subscribe( () => {
      this.alertify.success('Edycja udana');
    }, error => {
      this.alertify.error('Wystąpił błąd');
    });
  }

}