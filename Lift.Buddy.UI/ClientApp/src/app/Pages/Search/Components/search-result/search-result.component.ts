import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.css', '../../../../app.component.scss']
})
export class SearchResultComponent implements OnInit {

  @Input() user: User | undefined;

  constructor() { }

  ngOnInit() {
  }

}
