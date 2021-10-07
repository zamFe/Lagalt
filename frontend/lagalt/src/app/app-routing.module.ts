import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfilePage } from './pages/profile/profile.page';

const routes: Routes = [

  {
    path : '',
    pathMatch : 'full',
    redirectTo : '/login'
  },

  {
    path : 'profile',
    component : ProfilePage,
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
