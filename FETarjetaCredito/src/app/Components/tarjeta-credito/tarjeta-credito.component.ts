import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { __values } from 'tslib';
import { ToastrService } from 'ngx-toastr';
import { TarjetaService } from 'src/app/services/tarjeta.service';

@Component({
  selector: 'app-tarjeta-credito',
  templateUrl: './tarjeta-credito.component.html',
  styleUrls: ['./tarjeta-credito.component.css']
})
export class TarjetaCreditoComponent {
 listTarjetas: any[] = [];
 accion = 'Agregar';
 form: FormGroup
 id: number | undefined;

  constructor(private fb: FormBuilder,
    private toastr: ToastrService,
    private _tarjetaService: TarjetaService) {
  this.form = this.fb.group({
    titular: ['', Validators.required],
    numeroTarjeta: ['', [Validators.required, Validators.maxLength(16), Validators.minLength(16)]],
    fechaExpiracion: ['', [Validators.required, Validators.maxLength(5), Validators.minLength(5)]],
    cvv: ['', [Validators.required, Validators.maxLength(3), Validators.minLength(3)]],
    })
  }

  ngOnInit(): void { 
    this.obtenerTarjetas();  
}
obtenerTarjetas() {
 this._tarjetaService.getListTarjetas().subscribe(data => {
 this.listTarjetas = data;
  console.log(data);

  }, error => {
    console.log(error);
 })
}
  guardarTarjeta() {
    const tarjeta: any = {
      titular: this.form.get('titular')?.value,
      numeroTarjeta: this.form.get('numeroTarjeta')?.value,
      fechaExpiracion: this.form.get('fechaExpiracion')?.value,
      cvv: this.form.get('cvv')?.value,
    }

  if(this.id == undefined) {
    // agregamos una nueva tarjeta
    this._tarjetaService.saveTarjeta(tarjeta).subscribe(data => {
      this.toastr.success('La tarjeta fue registrada exitosamente.', '¡Tarjeta Registrada!');
      this.obtenerTarjetas();
      this.form.reset();
    }, error => {
      this.toastr.error('La tarjeta no pudo cargarse de forma correcta','Error')
      console.log(error);
    })
} else {
      // editar tarjeta
      tarjeta.id = this.id
      this._tarjetaService.updateTarjeta(this.id, tarjeta).subscribe(data => {
        this.form.reset();
        this.accion = 'Agregar';
        this.id = undefined;
        this.toastr.info('La tarjeta fue actualizada exitosamente', 'Tarjeta Actualizada');
        this.obtenerTarjetas();
      }, error => {
        console.log(error);
      })
    }

  }
  eliminarTarjeta(id: number) {
  // this.listTarjetas.splice(index, 1);
  this._tarjetaService.deleteTarjeta(id). subscribe(data => {
   this.toastr.error('La tarjeta fue eliminada exitosamente.', '¡Tarjeta Eliminada!');
    this.obtenerTarjetas();
    },error => {
      console.log(error);
    })
  }

  editarTarjeta(tarjeta: any) {
    this.accion = "Editar"
    this.id = tarjeta.id;

    this.form.patchValue({
     titular: tarjeta.titular,
     numeroTarjeta: tarjeta.numeroTarjeta,
     fechaExpiracion: tarjeta.fechaExpiracion,
     cvv: tarjeta.cvv
    })
  }
}
