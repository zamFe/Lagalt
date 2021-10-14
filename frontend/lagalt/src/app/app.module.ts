import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPage } from './pages/main/main.page';
import { SearchComponent } from './components/search/search.component';
import { MainLoginComponent } from './components/main-login/main-login.component';
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
import { ProjectStatusComponent } from './components/project-status/project-status.component';
import {MatRadioModule} from '@angular/material/radio';
import {MatCheckboxModule} from '@angular/material/checkbox';


@NgModule({
  declarations: [
    AppComponent,
    MainPage,
    SearchComponent,
    MainLoginComponent,
    MainListOfProjectsComponent,
    NavbarComponent,
    MainListOfProjectsItemComponent,
    ProfilePage,
    ProfileUserInfoComponent,
    ProfileSettingsComponent,
    ProjectPage,
    ProjectChatComponent,
    ProjectApplyPopUpComponent,
    ProjectStatusComponent
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
