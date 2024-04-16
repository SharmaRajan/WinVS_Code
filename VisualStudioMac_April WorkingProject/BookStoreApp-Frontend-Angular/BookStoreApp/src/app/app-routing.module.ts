import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllBooksComponent } from './components/all-books/all-books.component';
import { AddBookComponent } from './components/add-book/add-book.component';

const routes: Routes = [
  {path : 'books', component: AllBooksComponent},
  {path : 'add', component: AddBookComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
