import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
//import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  //private basePath = 'http://localhost:5082/api/books';

  private basePath = 'https://localhost:7169/api/books';

  constructor(private http: HttpClient) { }

  public getBooks() : Observable<any>{
    return this.http.get(this.basePath);
  }

  public addBook(book: any) : Observable<any>{
    return this.http.post(this.basePath,book);
  }
}
