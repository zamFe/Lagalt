import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPage } from "./pages/main/main.page";
import { ProfilePage } from './pages/profile/profile.page';

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
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
