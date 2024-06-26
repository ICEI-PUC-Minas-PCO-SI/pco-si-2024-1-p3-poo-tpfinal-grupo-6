import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeApuracaoComponent, HomeComponent, HomePageComponent } from './pages/home';
import {
  CandidatoDetalheComponent,
  CandidatosComponent,
  CandidatosListaComponent,
} from './pages/candidatos';

import { CadastroComponent, LoginComponent, PerfilComponent, UsuariosComponent } from './pages/usuarios';
import { CidadeDetalheComponent, CidadesComponent, CidadesListaComponent } from './pages/cidades';
import { PartidoDetalheComponent, PartidosComponent, PartidosListaComponent } from './pages/partidos';
import { ColigacaoDetalheComponent, ColigacoesComponent, ColigacoesListaComponent } from './pages/coligacoes';
import { ParametroEleicaoDetalheComponent, ParametrosEleicoesComponent, ParametrosEleicoesListaComponent } from './pages/parametros-eleicoes';
import { ConfigAdministradorComponent, EleicoesComponent } from './pages/eleicoes';




const routes: Routes = [
  { path: '', redirectTo: 'pages/home', pathMatch: 'full' },

  {
    path: 'pages/home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'homePage', pathMatch: 'full' },
      { path: 'homePage', component: HomePageComponent },
      { path: 'homeApuracao', component: HomeApuracaoComponent },
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
      { path: "lista", component: PartidosListaComponent },
      { path: "detalhe/:id", component: PartidoDetalheComponent },
      { path: "cadastrar", component: PartidoDetalheComponent },
    ],
  },

  {
    path: "coligacoes",
    redirectTo: "pages/coligacoes/lista",
    pathMatch: "full",
  },
  {
    path: "pages/coligacoes",
    component: ColigacoesComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "lista" },
      { path: "lista", component: ColigacoesListaComponent },
      { path: "detalhe/:id", component: ColigacaoDetalheComponent },
      { path: "cadastrar", component: ColigacaoDetalheComponent },
    ],
  },

  {
    path: "parametrosEleicoes",
    redirectTo: "pages/parametrosEleicoes/lista",
    pathMatch: "full",
  },
  {
    path: "pages/parametrosEleicoes",
    component: ParametrosEleicoesComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "lista" },
      { path: "lista", component: ParametrosEleicoesListaComponent },
      { path: "detalhe/:id", component: ParametroEleicaoDetalheComponent },
      { path: "cadastrar", component: ParametroEleicaoDetalheComponent },
    ],
  },

  {
    path: "eleicoes",
    redirectTo: "pages/eleicoes/lista",
    pathMatch: "full",
  },
  {
    path: "pages/eleicoes",
    component: EleicoesComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "lista" },
      { path: "lista", component: ParametrosEleicoesListaComponent },
      { path: "detalhe/:id", component: ParametroEleicaoDetalheComponent },
      { path: "admin", component: ConfigAdministradorComponent },
    ],
  },

  { path: "**", redirectTo: "pages/home", pathMatch: "full" },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
