import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ROUTE_PATH } from './_constants/route-names.constant';
import { ProjectsComponent } from './_components/projects/projects.component';
import { UsersComponent } from './_components/users/users.component';
import { TasksComponent } from './_components/tasks/tasks.component';

const routes: Routes = [
  { path: ROUTE_PATH.PROJECTS, component: ProjectsComponent },
  { path: ROUTE_PATH.USERS, component: UsersComponent },
  { path: ROUTE_PATH.TASKS, component: TasksComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
