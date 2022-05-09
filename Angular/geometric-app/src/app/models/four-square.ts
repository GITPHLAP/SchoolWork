import { ISquare } from "./isquare";
import { Square } from "./square";

export class FourSquare extends Square implements ISquare {
    constructor(length: number) {
        super();
        this.lengthA = length;
        this.lengthB = length;
        this.lengthC = length;
        this.lengthD = length;
    }
    perimeter(): number {
        return this.lengthA + this.lengthB + this.lengthC + this.lengthD;
    }
    area(): number {
        return this.lengthA * this.lengthB;
    }
}
