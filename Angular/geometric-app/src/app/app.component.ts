import { Component } from '@angular/core';
import { Rectangle } from './models/rectangle';
import {FourSquare } from './models/four-square';
import { Parallelogram } from './models/parallelogram';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'geometric-app';
  Square: FourSquare = {} as FourSquare;

  squareSide: number = 0;
  squareArea: number = 0;
  squarePerimeter: number = 0;

  rectangle: Rectangle = {} as Rectangle;
  rectLength: number = 0;
  rectWidth: number = 0;
  rectArea:number = 0;
  rectPerimeter:number = 0;

  paral: Parallelogram = {} as Parallelogram;
  paralLength: number = 0;
  paralWidth: number = 0;
  paralHeight: number = 0;
  paralArea:number = 0;
  paralPerimeter:number = 0;
  
  calculateFourSquare() {
    
    this.Square = new FourSquare(this.squareSide);
    this.squareArea = this.Square?.area();
    this.squarePerimeter = this.Square?.perimeter();
  }

  calculateRect() {
    
    this.rectangle = new Rectangle(this.rectLength, this.rectWidth);
    this.rectArea = this.rectangle?.area();
    this.rectPerimeter = this.rectangle?.perimeter();
  }

  calculateParal() {
    
    this.paral = new Parallelogram(this.paralLength, this.paralWidth);
    this.paral.setHeight(this.paralHeight);
    this.paralArea = this.paral?.area();
    this.paralPerimeter = this.paral?.perimeter();
  }
}
