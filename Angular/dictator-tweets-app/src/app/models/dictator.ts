import { BehaviorSubject, Observable } from "rxjs";
import { Tweet } from "./tweet";

export interface Dictator {
    id: number;
    firstName: string;
    lastName: string;
    birthYear: number;
    deadYear: number;
    shortDescription: string;
    mappningText: string;
    tweet$: BehaviorSubject<Tweet>;
    tweets$: BehaviorSubject<Tweet[]>;
}
