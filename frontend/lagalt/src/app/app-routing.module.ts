import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';

import { ProjectPage } from './pages/project/project.page';
import { MainPage } from "./pages/main/main.page";
import { ProfilePage } from './pages/profile/profile.page';
import { AdminGiveAccessPage } from './pages/admin-give-access/admin-give-access.page';

const routes: Routes = [
  {
      path: '',
      pathMatch: 'full',
      redirectTo: 'main'
  },
  {
      path: 'main',
      component: MainPage
  },
  {
      path : 'profile',
      component : ProfilePage,
      canActivate: [AuthGuard]
  },
  {
      path : 'project',
      component : ProjectPage,
  },
  {
    path : 'approval',
    component : AdminGiveAccessPage,
  }
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
