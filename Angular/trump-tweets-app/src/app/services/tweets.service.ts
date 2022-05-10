import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TweetsService {

  constructor() { }

  tweets =  [
    // tslint:disable-next-line:max-line-length
    {date: new Date('2019-09-25T13:24'), likes: 162500, text: 'There has been no President in the history of our Country who has been treated so badly as I have. The Democrats are frozen with hatred and fear. They get nothing done. This should never be allowed to happen to another President. Witch Hunt!'},
    // tslint:disable-next-line:max-line-length
    {date: new Date('2019-09-25T13:24'), likes: 105700, text: 'Such an important day at the United Nations, so much work and so much success, and the Democrats purposely had to ruin and demean it with more breaking news Witch Hunt garbage. So bad for our Country!'},
    // tslint:disable-next-line:max-line-length
    {date: new Date('2019-09-24T05:36'), likes: 219500, text: 'She seems like a very happy young girl looking forward to a bright and wonderful future. So nice to see!'},
    // tslint:disable-next-line:max-line-length
    {date: new Date('2019-09-22T02:04'), likes: 64800, text: '...The Fake News Media nowadays not only doesn’t check for the accuracy of the facts, they knowingly make up the facts. They even make up sources in order to protect their partners, the Democrats. It is so wrong, but they don’t even care anymore. They have gone totally CRAZY!!!!'},
    // tslint:disable-next-line:max-line-length
    {date: new Date('2019-09-21T23:47'), likes: 73700, text: ' “It appears that an American spy in one of our intelligence agencies may have been spying on our own president. The complaint suggests that this intel agent was listening in on Trump’s conversation....'},
    // tslint:disable-next-line:max-line-length
    {date: new Date('2019-07-14T12:27'), likes: 207000, text: ' So interesting to see “Progressive” Democrat Congresswomen, who originally came from countries whose governments are a complete and total catastrophe, the worst, most corrupt and inept anywhere in the world (if they even have a functioning government at all), now loudly.....'},
     // tslint:disable-next-line:max-line-length
     {date: new Date('2013-05-09T03:37'), likes: 156500, text: 'Sorry losers and haters, but my I.Q. is one of the highest -and you all know it! Please don\'t feel so stupid or insecure,it\'s not your fault'},
     // tslint:disable-next-line:max-line-length
     {date: new Date('2019-09-19T16:47'), likes: 88300, text: '....Knowing all of this, is anybody dumb enough to believe that I would say something inappropriate with a foreign leader while on such a potentially “heavily populated” call. I would only do what is right anyway, and only do good for the USA!'},
     // tslint:disable-next-line:max-line-length
     {date: new Date('2017-03-01T04:48'), likes: 117900, text: ' We are one people, with one destiny. We all bleed the same blood. We all salute the same flag. And we are all made by the same God.'},
     // tslint:disable-next-line:max-line-length
     {date: new Date('2016-11-29T12:55'), likes: 193700, text: ' Nobody should be allowed to burn the American flag - if they do, there must be consequences - perhaps loss of citizenship or year in jail!'},
     // tslint:disable-next-line:max-line-length
     {date: new Date('2013-12-10T13:32'), likes: 17300, text: 'How amazing, the State Health Director who verified copies of Obama’s “birth certificate” died in plane crash today. All others lived'},
     // tslint:disable-next-line:max-line-length
     {date: new Date('2017-02-06T06:01'), likes: 17300, text: 'Any negative polls are fake news, just like the CNN, ABC, NBC polls in the election. Sorry, people want border security and extreme vetting.'},
     {date: new Date('2012-11-07T20:24'), likes: 17300, text: 'It\'s freezing and snowing in New York--we need global warming!'},
     // tslint:disable-next-line:max-line-length
     {date: new Date('2010-08-20T19:51'), likes: 17300, text: '....The Prime Minister was able to save a great deal of expense and effort for both the United States and Denmark by being so direct. I thank her for that and look forward to rescheduling sometime in the future!'},
  ];
}
