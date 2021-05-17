var instrumentsAry;

function AddInstruments() {
    let harp = new Harp(2.0, 10); // Harp object 

    let guitar = new Guitar("wood", 12);

    let saxophone = new Saxophone("wood");

    let flute = new Flute("wood", 6);

    instrumentsAry = [harp, guitar, saxophone, flute];

    let show = new ShowInstruments();
    show.Add();
}

class Instrument {
    name;

    //jeg har fjernet sound fra play methoden for at undgå redundans, det gav bedre mening at sætte en property på instrumentet.
    play_() {

        var audio = new Audio(this.sound); //create new audio from the sound path
        return audio
    }
}

class Stringed extends Instrument {

    constructor(numberOfStrings) {
        super(); //this need to be declared first because its extend from instrument (inheritance) 

        this.numberOfStrings = numberOfStrings //set number of strings
    }

}

class Harp extends Stringed { //extend from stringed

    name = "Harp"; // declare name

    sound = 'Harp.mp3'; //file path

    constructor(height, numberOfStrings) {
        super(numberOfStrings); //again use same parameter as parent constructor
        this.height = height;

    }
}

class Guitar extends Stringed {

    name = "Guitar";

    sound = 'Guitar.mp3'; //file path



    constructor(material, numberOfStrings) {
        super(numberOfStrings);

        this.material = material;

    }
}

class Saxophone extends Instrument {

    name = "Saxophone"

    sound = 'Saxophone.mp3'; //file path


    constructor(material) {
        super();
        this.material = material;
    }
}

class Flute extends Instrument {

    name = "Flute"

    sound = 'Flute.mp3'; //file path

    constructor(material, holes) {
        super();
        this.material = material;

        this.holes = holes;
    }
}

class ShowInstruments { //Class to show all instruments

    constructor() {
        this.div = document.querySelector('.instrument_class');//this select the first element which equal to .instrument_class
    }
    Add() {
        instrumentsAry.forEach(element => { // Do this for each instruments in list
            Object.entries(element).forEach(property => {

                const p = document.createElement('p'); //to create a paragraph

                p.innerHTML = `${property[0]}: ${property[1]}` //Set content on paragraph

                this.div.appendChild(p); //add it as a child in div

            });
            const btn = document.createElement('BUTTON'); // constant btn because I want to create a button

            btn.onclick = function () { var sound = element.play_(); sound.play(); }; //Set OnClick on button // function to onclick event

            btn.innerHTML = "Afspil lyd"; //Set content on button

            this.div.appendChild(btn); //Add it to div.
        });

    }
}
