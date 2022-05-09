import { ISquare } from "./isquare";
import { Square } from "./square";

export class Rectangle extends Square implements ISquare{
    constructor(length: number, width: number) {
        super();
        this.lengthA = length;
        this.lengthB = width;
        this.lengthC = length;
        this.lengthD = width;
    }
    perimeter(): number {
        return this.lengthA + this.lengthB + this.lengthC + this.lengthD;
    }
    area(): number {
        return this.lengthA * this.lengthB;
    }
}
