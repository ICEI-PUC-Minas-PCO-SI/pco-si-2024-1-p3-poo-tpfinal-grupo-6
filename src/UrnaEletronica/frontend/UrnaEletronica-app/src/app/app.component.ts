import { ChangeDetectionStrategy, Component, Inject, PLATFORM_ID, inject } from '@angular/core';
import { LoginService } from './services/usuario';
import { Router } from '@angular/router';
import { BreakpointObserver } from '@angular/cdk/layout';
import { Usuario } from './shared/models/interfaces/usuario';
import { Constants } from './util/constants';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  #loginService = inject(LoginService);
  #router = inject(Router);
  #breakpointObserver = inject(BreakpointObserver);


  title = 'UrnaEletronica-app';

  public openedDrawer = true;

  public get modeDrawer() {
    return this.openedDrawer ? "side" : "over";
  }

  public constructor(@Inject(PLATFORM_ID) private platformId: any) {}

  public showDrawer(): boolean {
  return (
      this.#router.url != "/pages/usuarios/login" &&
      this.#router.url != "/pages/usuarios/cadastro"
    );
  }

  ngAfterContentInit(): void {
    this.#breakpointObserver.observe(["(max-width: 800px)"])
    .subscribe((res) => this.openedDrawer = !res.matches);
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  public setCurrentUser(): void {
    if (isPlatformBrowser(this.platformId)) {
      let usuario = {} as Usuario;

      if (localStorage.getItem(Constants.LOCAL_STORAGE_NAME))
        usuario = JSON.parse(
          localStorage.getItem(Constants.LOCAL_STORAGE_NAME) ?? "{}"
        );

      if (usuario) this.#loginService.setCurrentUser(usuario);

    }
  }
}
