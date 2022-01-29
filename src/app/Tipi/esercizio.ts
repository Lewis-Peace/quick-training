export class Esercizio {
    public type: string;
    public peso: number;
    public rep: number;

    constructor(type, peso, rep) {
        this.type = type;
        this.peso = peso;
        this.rep = rep;
    }
}
