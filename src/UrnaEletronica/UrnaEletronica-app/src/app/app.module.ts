import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from './shared';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { PartidosModule } from './pages/partidos/partidos.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    NgbModule,
    HttpClientModule,
    SharedModule,
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
