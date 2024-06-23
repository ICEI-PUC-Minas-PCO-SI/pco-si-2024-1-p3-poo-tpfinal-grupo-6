import { AppRoutingModule } from '../app-routing.module';

import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FlexModule } from '@angular/flex-layout';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule, provideToastr } from 'ngx-toastr';

import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { DrawerNavigatorComponent, ModalDeleteComponent, TitleNavigatorComponent } from '.';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from "@angular/material/dialog";
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatInputModule } from '@angular/material/input';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MAT_DATE_LOCALE, provideNativeDateAdapter } from '@angular/material/core';
import { RouterModule } from '@angular/router';
import { NgxMaskDirective, provideEnvironmentNgxMask } from 'ngx-mask';
import { JwtInterceptor } from '../util/security';
import { LoginService, UsuarioService } from '../services/usuario';
import { CandidatoService } from '../services/candidato';
import { NgbModule, NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';




@NgModule({
  declarations: [
   DrawerNavigatorComponent, TitleNavigatorComponent, ModalDeleteComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    CommonModule,
    FlexModule,
    NgbCollapseModule,
    NgbModule,
    NgxMaskDirective,
    NgxSpinnerModule.forRoot({ type: "timer"}),
    RouterModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),

    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatSelectModule,
    MatSidenavModule,
    MatTooltipModule,

  ],
  exports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FlexModule,
    NgbCollapseModule,
    NgbModule,
    NgxSpinnerModule,
    RouterModule,
    ToastrModule,

    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatSelectModule,
    MatSidenavModule,
    MatTooltipModule,

    DrawerNavigatorComponent,
    ModalDeleteComponent,
    TitleNavigatorComponent
  ],
  providers: [
    CandidatoService,
    LoginService,
    UsuarioService,

    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline', color: 'primary' }},
    { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },

    provideAnimations(),
    provideAnimationsAsync(),
    provideNativeDateAdapter(),
    provideEnvironmentNgxMask(),
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],

})
export class SharedModule { }
