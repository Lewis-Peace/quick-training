import { Subscription } from 'rxjs';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { User } from 'src/app/Model/User';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  public searchResults: User[] = [];
  public isLoading: boolean = false;
  private isLoadingSubscription: Subscription | undefined;

  constructor(
    private loadingService: LoadingVisualizationService
  ) { }

  ngOnInit() {
    this.isLoadingSubscription = this.loadingService.$isLoading.subscribe(isLoading =>
      this.isLoading = isLoading
    )
  }

  ngOnDestroy() {
    this.isLoadingSubscription?.unsubscribe();
  }

  public setSearchResult(result: User[]) {
    this.searchResults = result;
  }

}
