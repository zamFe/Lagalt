import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPage } from "./pages/main/main.page";

const routes: Routes = [
  {
      path: '',
      pathMatch: 'full',
      redirectTo: 'main'
  },
  {
      path: 'main',
      component: MainPage
  }


]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
