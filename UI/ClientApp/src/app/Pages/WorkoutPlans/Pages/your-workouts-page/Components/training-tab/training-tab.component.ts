import { WorkoutDay } from 'src/app/Model/WorkoutDay';
import { Component, Input, OnInit } from '@angular/core';
import { Exercise } from 'src/app/Model/Exercise';

@Component({
    selector: 'app-training-tab',
    templateUrl: './training-tab.component.html',
    styleUrls: ['./training-tab.component.css']
})

export class TrainingTabComponent implements OnInit {

    @Input() exercises: Exercise[] = [];
    @Input() name: string = '';

    constructor() { }

    ngOnInit() {
    }

}
