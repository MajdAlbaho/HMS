import { NgModule } from '@angular/core';
import {
    MatDialogModule, MatFormFieldModule, MatButtonModule, MatInputModule
} from '@angular/material';


@NgModule({
    imports: [MatDialogModule, MatFormFieldModule, MatButtonModule, MatInputModule],
    exports: [MatDialogModule, MatFormFieldModule, MatButtonModule, MatInputModule]
})

export class AngularMaterialModule { }