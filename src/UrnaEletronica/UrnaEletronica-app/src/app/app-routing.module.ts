import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent, HomePageComponent } from './pages/home';
import { CadastroComponent, LoginComponent, PerfilComponent, UsuariosComponent } from './pages/usuarios';
import { PartidosComponent } from './pages/partidos/partidos.component';
import { PartidoListaComponent } from './pages/partidos/partido-lista/partido-lista.component';
import { PartidoDetalheComponent } from './pages/partidos/partido-detalhe/partido-detalhe.component';

const routes: Routes = [
  { path: "", redirectTo: "pages/home", pathMatch: "full" },

  {
    path: "pages/home",
    component: HomeComponent,
    children: [
      { path: "", redirectTo: "homePage", pathMatch: "full" },
      { path: "homePage", component: HomePageComponent },
    ],
  },

  {
    path: "usuarios",
    redirectTo: "pages/usuarios/login",
    pathMatch: "full",
  },
  {
    path: "pages/usuarios",
    component: UsuariosComponent,
    children: [
      { path: "", pathMatch: "full", redirectTo: "login" },
      { path: "login", component: LoginComponent },
      { path: "perfil", component: PerfilComponent },
      { path: "cadastro", component: CadastroComponent },
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
  exports: [RouterModule]
})
export class AppRoutingModule { }
