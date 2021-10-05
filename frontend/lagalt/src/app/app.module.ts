import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPage } from './pages/main/main.page';
import { MainSearchComponent } from './components/main-search/main-search.component';
import { MainFilterComponent } from './components/main-filter/main-filter.component';
import { MainLoginComponent } from './components/main-login/main-login.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPage,
    MainSearchComponent,
    MainFilterComponent,
    MainLoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
