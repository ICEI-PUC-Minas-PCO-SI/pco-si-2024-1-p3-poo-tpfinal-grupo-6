import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent, HomePageComponent } from './pages/home';
import {
  CadastroComponent,
  LoginComponent,
  PerfilComponent,
  UsuariosComponent,
} from './pages/usuarios';
import {
  CandidatoDetalheComponent,
  CandidatosComponent,
  CandidatosListaComponent,
} from './pages/candidatos';

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

  { path: '**', redirectTo: 'pages/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
