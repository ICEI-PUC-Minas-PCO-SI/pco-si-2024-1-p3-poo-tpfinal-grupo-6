import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent, HomePageComponent } from './pages/home';
import {
  CandidatoDetalheComponent,
  CandidatosComponent,
  CandidatosListaComponent,
} from './pages/candidatos';

import { CadastroComponent, LoginComponent, PerfilComponent, UsuariosComponent } from './pages/usuarios';
import { CidadeDetalheComponent, CidadesComponent, CidadesListaComponent } from './pages/cidades';
import { PartidosComponent } from './pages/partidos/partidos.component';
import { PartidoListaComponent } from './pages/partidos/partido-lista/partido-lista.component';
import { PartidoDetalheComponent } from './pages/partidos/partido-detalhe/partido-detalhe.component';

const routes: Routes = [
  { path: '', redirectTo: 'pages/home', pathMatch: 'full' },

  {
    path: 'pages/home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'homePage', pathMatch: 'full' },
      { path: 'homePage', component: HomePageComponent },
    ],
  },

  {
    path: 'usuarios',
    redirectTo: 'pages/usuarios/login',
    pathMatch: 'full',
  },
  {
    path: 'pages/usuarios',
    component: UsuariosComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'login' },
      { path: 'login', component: LoginComponent },
      { path: 'perfil', component: PerfilComponent },
      { path: 'cadastro', component: CadastroComponent },
    ],
  },

  {
    path: 'candidatos',
    redirectTo: 'pages/candidatos/lista',
    pathMatch: 'full',
  },
  {
    path: 'pages/candidatos',
    component: CandidatosComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'lista' },
      { path: 'lista', component: CandidatosListaComponent },
      { path: 'detalhe/:id', component: CandidatoDetalheComponent },
      { path: 'cadastrar', component: CandidatoDetalheComponent },
    ],
  },

  {
    path: "cidades",
    redirectTo: "pages/cidades/lista",
    pathMatch: "full",
  },
  {
    path: "pages/cidades",
    component: CidadesComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "lista" },
      { path: "lista", component: CidadesListaComponent },
      { path: "detalhe/:id", component: CidadeDetalheComponent },
      { path: "cadastrar", component: CidadeDetalheComponent },
    ],
  },
  {
    path: "partidos",
    redirectTo: "pages/partidos/lista",
    pathMatch: "full",
  },
  {
    path: "pages/partidos",
    component: PartidosComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "lista" },
      { path: "lista", component: PartidoListaComponent },
      { path: "detalhe/:id", component: PartidoDetalheComponent },
      { path: "cadastrar", component: PartidoDetalheComponent },
    ],
  },

  { path: "**", redirectTo: "pages/home", pathMatch: "full" },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
