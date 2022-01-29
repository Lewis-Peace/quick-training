import { Exercises } from "./Exercises"

export class Training {
    days: string[]
    exercises: Exercises[][]

    constructor(days: string[], exercises: Exercises[][]) {
        this.days = days
        this.exercises = exercises
    }

}