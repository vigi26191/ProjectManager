import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ROUTE_PATH } from './_constants/route-names.constant';
import { ProjectsComponent } from './_components/projects/projects.component';
import { UsersComponent } from './_components/users/users.component';
import { TasksComponent } from './_components/tasks/tasks.component';

import { TaskResolver } from './_resolvers/task.resolver';
import { ProjectResolver } from './_resolvers/project.resolver';

const routes: Routes = [
  {
    path: ROUTE_PATH.PROJECTS,
    component: ProjectsComponent,
    resolve: {
      lookUpData: ProjectResolver
    }
  },
  {
    path: ROUTE_PATH.USERS,
    component: UsersComponent
  },
  {
    path: ROUTE_PATH.TASKS + '/:section',
    component: TasksComponent,
    resolve: {
      lookUpData: TaskResolver
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
