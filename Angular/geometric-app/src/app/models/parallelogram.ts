import { ISquare } from "./isquare";
import { Square } from "./square";

export class Parallelogram extends Square implements ISquare {
    height: number = 0;
    setHeight(_height:number){
        this.height = _height;
    }
    constructor(length: number, width: number) {
        super();
        this.lengthA = length;
        this.lengthB = width;
        this.lengthC = length;
        this.lengthD = width;
    }
    area(): number {
        return this.lengthA * this.height;
    }
}
