export class Exercises {
    /** The name of the exerercise*/
    name: string
    /** The quantity of reps*/
    reps: number
    /** The quantity of series */
    series: number
    /** An http representing the exercise*/
    link: string
    /** The weight lifted */
    weigth: number

    /**
     * Class constructor
     * @param name The name of the exerercise
     * @param reps The quantity of reps
     * @param weight (optional) The weight lifted
     * @param series (optional) The quantity of series
     * @param link (optional) An http representing the exercise
     */
    constructor(name: string, reps: number, weight: number = undefined, series: number = undefined, link: string = undefined) {
        this.name = name
        this.reps = reps
        if (weight !== undefined) {
            this.weigth = weight
        }
        if (series !== undefined) {
            this.series = series
        }
        if (link !== undefined) {
            this.link = link
        }

    }

    toString() {
        return this.name + ' ' + this.reps + 'x' + this.series
    }

}