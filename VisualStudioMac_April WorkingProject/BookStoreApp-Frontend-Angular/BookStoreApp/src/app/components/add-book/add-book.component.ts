import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.scss'
})
export class AddBookComponent implements OnInit{

  public bookForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private service: BookService){}

  ngOnInit(): void {
    this.init();
  }

  private init() : void{
    this.bookForm = this.formBuilder.group({
      title : [],
      description : []
    });
  }

  public saveBook(): void {
    this.service.addBook(this.bookForm.value).subscribe(result =>{
      alert(`New book is added with id ${result}`)
    });
  }
}
