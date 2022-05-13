import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AlldictatorsComponent } from './dictator/alldictators/alldictators.component';
import { DictatorProfileComponent } from './dictator/dictator-profile/dictator-profile.component';

const routes: Routes = [
  {path: 'dictator/:id', component: DictatorProfileComponent},
  {path: '', component: AlldictatorsComponent},
  // TODO: add 404page 
  // {path: '**', component: } // this path is wildcard for all pages which not exist
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
