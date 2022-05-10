import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'morse'
})
export class MorsePipe implements PipeTransform {

  
  transform(value: string, args: string): string {
    let transformedStr = "";
    if (args == "from") {
      let codes = value.split(' ');
      codes.forEach(element => {
        transformedStr += this.getFromMorseCode(element) + " ";
      });
    }
    else if (args == "to") {
      let chars = [...value];
      chars.forEach(element => {
        transformedStr += this.getMorseCode(element) + " ";
      });
    }
    
    return transformedStr;
  }
  morseArr: [string, string][] = [
    ['a',".-"],
    ['b',"-..."],
    ['c',"-.-."],
    ['d',"-.."],
    ['e',"."],
    ['f',"..-."],
    ['g',"--."],
    ['h',"...."],
    ['i',".."],
    ['j',".---"],
    ['k',"-.-"],
    ['l',".-.."],
    ['m',"--"],
    ['n',"-."],
    ['o',"---"],
    ['p',".--."],
    ['q',"--.-"],
    ['r',".-."],
    ['s',"..."],
    ['t',"-"],
    ['u',"..-"],
    ['v',"...-"],
    ['w',".--"],
    ['x',"-..-"],
    ['y',"-.--"],
    ['z',"--.."],
    ['1',".----"],
    ['2',"..---"],
    ['3',"...--"],
    ['4',"....-"],
    ['5',"....."],
    ['6',"-...."],
    ['7',"--..."],
    ['8',"---.."],
    ['9',"----."],
    ['0',"-----"]];

    getMorseCode(char: string): string {
      let result = '';
      this.morseArr.map((morse) => {
        if(morse[0] == char.toLowerCase()) {
          result = morse[1];
        }
        
      });
      return result;
    }
    getFromMorseCode(char: string): string {
      let result = '';
      this.morseArr.map((morse) => {
        if(morse[1] == char) {
          result = morse[0];
        }
        
      });
      return result;
    }



}
