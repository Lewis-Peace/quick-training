import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-assigned-to-me',
  templateUrl: './assigned-to-me.component.html',
  styleUrls: ['./assigned-to-me.component.css']
})
export class AssignedToMeComponent implements OnInit {

  public isLoading: boolean = false;

  constructor(
    private loadingVisualizationService: LoadingVisualizationService
  ) { }

  ngOnInit() {
    this.loadingVisualizationService.setIsLoading(true);
    this.loadingVisualizationService.setIsLoading(false);
  }

}
