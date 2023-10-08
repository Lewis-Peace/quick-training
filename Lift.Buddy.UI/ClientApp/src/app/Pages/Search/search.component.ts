import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  public searchResults: User[] = [];

  constructor() { }

  ngOnInit() {
  }

  public setSearchResult(result: User[]) {
    this.searchResults = result;
  }

}
