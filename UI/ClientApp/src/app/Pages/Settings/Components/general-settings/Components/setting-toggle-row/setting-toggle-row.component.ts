import { Component, Input, OnInit, forwardRef } from '@angular/core';
import { FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { LoadingVisualizationService } from 'src/app/Services/loading-visualization.service';

@Component({
  selector: 'app-setting-toggle-row',
  templateUrl: './setting-toggle-row.component.html',
  styleUrls: ['./setting-toggle-row.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SettingToggleRowComponent),
      multi: true,
    }
  ]
})
export class SettingToggleRowComponent implements OnInit {

  @Input() label: string = '';
  @Input() doesLoading: boolean = false;
  @Input() formControl: FormControl = new FormControl(null);
  @Input() default: boolean | undefined = false;
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

  writeValue(value: boolean) {
    this.default = value ? value : false;
  }

  registerOnChange() {}

  registerOnTouched() {}
}
