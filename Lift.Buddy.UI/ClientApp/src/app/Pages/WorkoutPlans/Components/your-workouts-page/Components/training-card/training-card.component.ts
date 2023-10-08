import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { Component, Input, OnInit } from '@angular/core';
import { Exercise } from 'src/app/Model/Exercise';

@Component({
    selector: 'app-training-card',
    templateUrl: './training-card.component.html',
    styleUrls: ['./training-card.component.css']
})

export class TrainingCardComponent implements OnInit {

    @Input() exercises: Exercise[] = [];
    @Input() name: string = '';

    constructor() { }

    ngOnInit() {
    }

}
