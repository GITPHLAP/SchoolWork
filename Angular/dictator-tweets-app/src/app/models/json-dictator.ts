import { NumberSymbol } from "@angular/common";

export class JsonDictator {
    id: number;
    firstName: string;
    lastName: string;
    birthYear: number;
    deadYear: number;
    shortDescription: string;
    mappningText: string;

    constructor(_id: number,
        _firstName: string,
        _lastName: string,
        _birthYear: number,
        _deadYear: number,
        _shortDescription: string,
        _mappningText: string) {
            this.id = _id;
            this.firstName = _firstName;
            this.lastName = _lastName;
            this.birthYear = _birthYear;
            this.deadYear = _deadYear;
            this.shortDescription = _shortDescription;
            this.mappningText = _mappningText;

    }
}
