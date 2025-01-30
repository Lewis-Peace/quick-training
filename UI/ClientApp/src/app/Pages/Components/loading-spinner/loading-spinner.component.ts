import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.css']
})
export class LoadingSpinnerComponent implements OnInit {

  constructor(
    private loadingService: LoadingVisualizationService
  ) { }

  @Output() OnLoading: EventEmitter<boolean> = new EventEmitter();
  public isLoading: boolean = false;
  public isLoadingSubscription: Subscription | undefined;

  ngOnInit() {
    this.isLoadingSubscription = this.loadingService.$isLoading.subscribe(isLoading => {
      this.isLoading = isLoading;
      this.OnLoading.emit(isLoading);
    });
  }

  ngOnDestroy() {
    this.isLoadingSubscription?.unsubscribe();
  }

}
