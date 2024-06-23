import { NgModule } from '@angular/core';
import { provideClientHydration } from '@angular/platform-browser';

import { AppComponent } from './app.component';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from './shared';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';

import { CandidatosModule } from './pages/candidatos/candidatos.module';

import { CidadesModule } from './pages/cidades/cidades.module';
import { PartidosModule } from './pages/partidos/partidos.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    NgbModule,
    HttpClientModule,

    SharedModule,
    CandidatosModule,
    CidadesModule,
    PartidosModule
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(),
    provideHttpClient(withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
