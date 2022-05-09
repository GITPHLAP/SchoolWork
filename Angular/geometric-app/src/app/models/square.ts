export abstract class Square{
    lengthA: number = 0;
    lengthB: number = 0;
    lengthC: number = 0;
    lengthD: number = 0;

    perimeter(): number {
        return Number(this.lengthA) + Number(this.lengthB) + Number(this.lengthC) + Number(this.lengthD);
    }
}
