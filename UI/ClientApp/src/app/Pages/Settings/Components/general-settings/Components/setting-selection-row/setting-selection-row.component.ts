import { Component, Input, OnInit, forwardRef } from '@angular/core';
import { FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-setting-selection-row',
  templateUrl: './setting-selection-row.component.html',
  styleUrls: ['./setting-selection-row.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SettingSelectionRowComponent),
      multi: true,
    }
  ]
})
export class SettingSelectionRowComponent implements OnInit {

  @Input() label: string = '';
  @Input() doesLoading: boolean = false;
  @Input() formControl: FormControl = new FormControl(null);
  @Input() default: any = 0;
  @Input() options: string[] = [];

  constructor(
    private loadingVisualizationService: LoadingVisualizationService
  ) { }

  ngOnInit() {
    this.formControl.setValue(this.default);
  }

  public updateForm(value: any) {
    this.loadingVisualizationService.setIsLoading(true && this.doesLoading);
    this.formControl.setValue(value);

    // TODO: reload page with new language

    this.loadingVisualizationService.setIsLoading(false);
  }

  // Don't know what this does but it's mandatory for the provider

  writeValue(value: string) {
    this.default = value ? value : 0;
  }

  registerOnChange() {}

  registerOnTouched() {}

}
