import { Subscription } from 'rxjs';
import { Component, OnInit, ViewChild, ElementRef, TemplateRef, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/Model/User';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { SearchService } from 'src/app/Services/search.service';
import { UserService } from 'src/app/Services/user.service';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  public suggestedSubscription: Subscription | undefined;
  public suggested: User[] = [];
  public searchValue: any = "";

  @ViewChild('input') input: ElementRef<HTMLInputElement> | undefined
  @ViewChild(MatAutocompleteTrigger) autocomplete: MatAutocompleteTrigger | undefined

  @Output() result: EventEmitter<User[]> = new EventEmitter();

  constructor(
    private searchService: SearchService,
    private snackbarService: SnackBarService,
    private loadingService: LoadingVisualizationService
  ) { }

  ngOnInit() {
    this.suggestedSubscription = this.searchService.userSuggestion$.subscribe(suggestions => {
      this.suggested = suggestions;
    })
  }

  ngOnDestroy() {
    this.suggestedSubscription?.unsubscribe();
  }

  public updateSuggested() {
    const username = this.input?.nativeElement.value;
    if (!username || username == '') {
      return;
    }
    this.searchService.setUserSuggestion(username);
  }

  public async search() {
    const username = this.input?.nativeElement.value;
    if (!username || username == '') {
      return;
    }

    this.loadingService.setIsLoading(true);
    const response = await this.searchService.getUsersByUsername(username);

    if (!response.result) {
      this.snackbarService.operErrorSnackbar(`Failed to load search result. Ex: ${response.notes}`);
    }

    this.loadingService.setIsLoading(false);
    this.autocomplete?.closePanel();
    this.result.emit(response.body);
  }

}
