import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from '@auth0/auth0-angular';
import { AppComponent } from './app.component';
import { MainPage } from './pages/main/main.page';
import { SearchComponent } from './components/search/search.component';
import { MainListOfProjectsComponent } from './components/main-list-of-projects/main-list-of-projects.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MainListOfProjectsItemComponent } from './components/main-list-of-projects-item/main-list-of-projects-item.component';
import { ProfilePage } from './pages/profile/profile.page';
import { ProfileUserInfoComponent } from './components/profile-user-info/profile-user-info.component';
import { ProfileSettingsComponent } from './components/profile-settings/profile-settings.component';
import { ProjectPage } from "./pages/project/project.page";
import { ProjectChatComponent } from './components/project-chat/project-chat.component';
import { HttpClientModule} from "@angular/common/http";
import { ProjectApplyPopUpComponent } from './components/project-apply-pop-up/project-apply-pop-up.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginButtonComponent } from './components/login-button/login-button.component';
import { AuthButtonComponent } from './components/auth-button/auth-button.component';
import { LogoutButtonComponent } from './components/logout-button/logout-button.component';
import { AuthNavComponent } from './components/auth-nav/auth-nav.component';

import { ProjectStatusComponent } from './components/project-status/project-status.component';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AddNewProjectComponent } from './components/add-new-project/add-new-project.component';
import { AdminGiveAccessPage } from './pages/admin-give-access/admin-give-access.page';
import { MyProjectsComponent } from './pages/my-projects/my-projects.component';
import { MyProjectsListOfProjectsComponent } from './components/my-projects-list-of-projects/my-projects-list-of-projects.component';
import { MyProjectsListOfProjectsItemComponent } from './components/my-projects-list-of-projects-item/my-projects-list-of-projects-item.component';



@NgModule({
  declarations: [
    AppComponent,
    MainPage,
    SearchComponent,
    MainListOfProjectsComponent,
    NavbarComponent,
    MainListOfProjectsItemComponent,
    ProfilePage,
    ProfileUserInfoComponent,
    ProfileSettingsComponent,
    ProjectPage,
    ProjectChatComponent,
    ProjectApplyPopUpComponent,
    LoginButtonComponent,
    AuthButtonComponent,
    LogoutButtonComponent,
    AuthNavComponent,
    ProjectStatusComponent,
    AddNewProjectComponent,
    AdminGiveAccessPage,
    MyProjectsComponent,
    MyProjectsListOfProjectsComponent,
    MyProjectsListOfProjectsItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    MatSlideToggleModule,
    HttpClientModule,
    NgbModule,
    AuthModule.forRoot({
      domain: 'dev--rmchv2w.eu.auth0.com',
      clientId: 'OoAzUGX9zMtfwak3auYzMkZCuURGRbA3'
    }),
    MatRadioModule,
    MatCheckboxModule,
  ],
  exports: [
    MatSlideToggleModule,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
