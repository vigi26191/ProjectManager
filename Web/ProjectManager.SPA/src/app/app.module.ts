import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { AlertModule } from 'ngx-bootstrap/alert';
import { DataTablesModule } from 'angular-datatables';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NavComponent } from './_shared/components/nav/nav.component';
import { ProjectsComponent } from './_components/projects/projects.component';
import { UsersComponent } from './_components/users/users.component';
import { TasksComponent } from './_components/tasks/tasks.component';

import { OnlyNumberDirective } from './_directives/only-number.directive';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    ProjectsComponent,
    UsersComponent,
    TasksComponent,
    OnlyNumberDirective
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BsDatepickerModule.forRoot(),
    AlertModule.forRoot(),
    DataTablesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
