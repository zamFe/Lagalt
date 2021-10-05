import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPage } from './pages/main/main.page';
import { MainSearchComponent } from './components/main-search/main-search.component';
import { MainFilterComponent } from './components/main-filter/main-filter.component';
import { MainLoginComponent } from './components/main-login/main-login.component';
import { MainYourProjectsComponent } from './components/main-your-projects/main-your-projects.component';
import { MainListOfProjectsComponent } from './components/main-list-of-projects/main-list-of-projects.component';
import { MainFilterOptionComponent } from './components/main-filter-option/main-filter-option.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProfilePage } from './pages/profile/profile.page';
import { ProfileImageComponent } from './components/profile-image/profile-image.component';
import { ProfileUserInfoComponent } from './components/profile-user-info/profile-user-info.component';
import { ProfileSettingsComponent } from './components/profile-settings/profile-settings.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPage,
    MainSearchComponent,
    MainFilterComponent,
    MainLoginComponent,
    MainYourProjectsComponent,
    MainListOfProjectsComponent,
    MainFilterOptionComponent,
    NavbarComponent,
    ProfilePage,
    ProfileImageComponent,
    ProfileUserInfoComponent,
    ProfileSettingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }