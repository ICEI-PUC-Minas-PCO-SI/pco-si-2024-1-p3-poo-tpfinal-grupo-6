import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";


import { SharedModule } from "../../shared";
import { ReactiveFormsModule } from "@angular/forms";
import { HomeApuracaoComponent, HomeComponent, HomePageComponent } from ".";


@NgModule({
  declarations: [HomeComponent, HomePageComponent, HomeApuracaoComponent],
  imports: [ReactiveFormsModule, SharedModule],
})
export class HomeModule {}
