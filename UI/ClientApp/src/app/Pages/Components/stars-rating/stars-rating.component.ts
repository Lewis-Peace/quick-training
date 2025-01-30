import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-stars-rating',
  templateUrl: './stars-rating.component.html',
  styleUrls: ['./stars-rating.component.css']
})
export class StarsRatingComponent implements OnInit {

  @Input() rating: number = 0;
  @Input() readonly: boolean = false;

  @Output() onChange: EventEmitter<number> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  public getImage(index: number) {
    const value = this.rating - index;
    if (value >= 0) {
      return '../../../../assets/full-star.png';
    } else if (value == -0.5 && false) {
      return '../../../../assets/half-star.png';
    } else {
      return '../../../../assets/empty-star.png';
    }
  }

  public setRating(index: number) {
    if (!this.readonly) {
      this.rating = index;
      this.onChange.emit(this.rating);
    }
  }

}
