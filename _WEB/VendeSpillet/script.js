//Global variables
var board;
var colors = [
    `rgba( 255,191,0, 1 )`,
    `rgba( 166,124,100, 1 )`,
    `rgba( 191,55,58, 1 )`,
    `rgba( 104,84,137, 1 )`,
    `rgba( 167,69,127, 1 )`,
    `rgba( 255,155,131, 1 )`,
    `rgba( 91, 225, 255, 1 )`,
    `rgba( 120, 255, 68, 1 )`,
    `rgba( 253, 255, 106, 1 )`,
    `rgba( 32, 133, 239, 1 )`,
    `rgba( 0, 194, 143, 1 )`,
    `rgba( 189, 128, 0, 1 )`,
];
var cards = [];
var flipped_card;
var flippedcards = 0;
window.onload = function () {
    //get div board 
    board = document.getElementById("board");
    //Start game
    startNewGame();
}


function startNewGame() {

    //create 12 cards pair 
    for (index = 0; index < 12; index++) {
        var color = colors[index];
        cards.push(new card(index, color));
        cards.push(new card(index, color));
    }

    // shuffleCards();
    cards.forEach(function (card) {
        card.addCardToBoard();

        // card.element.style.backgroundColor = card.color;
    });
}

function pickCard(card) {
    //if there is alread picked 2 cards nothing should happend
    if (flippedcards >= 2) {
        return null;
    }
    //if there is a flippef card then set the "old" flipped card to firscard
    if (flipped_card != undefined) var firstcard = flipped_card;
    //set flippedcard to card
    flipped_card = card;
    //set the background color on the card div
    card.element.style.backgroundColor = card.color;
    //add 1 to flippedcards counter
    flippedcards++;
    //if first card contain something then its mean its second pick
    if (firstcard != undefined) {
        //timeout for 1 seconds so user can see color before card flip again
        setTimeout(function () { secondtry(firstcard, flipped_card) }, 1000);
    }

}
//second try function
function secondtry(card1, card2) {
    //If card matched then unflipcards
    if (!checkCardMatch(card1, card2)) {
        unflipCards(card1, card2);
    }
    //Else it means it a pair
    else {
        //When cards is matched they dont need onclick function
        card1.element.onclick = null;
        card2.element.onclick = null;

    }
    //Reset variables 
    firstcard = undefined;
    flipped_card = undefined;
    flippedcards = 0;
}

function unflipCards(firstcard1, secondcard) {
    //Change color on card elements
    firstcard1.element.style.backgroundColor = "lightgrey";
    secondcard.element.style.backgroundColor = "lightgrey";
}
//Check if cards number match
function checkCardMatch(firstcard, secondcard) {
    if (firstcard.pairnum == secondcard.pairnum) {
        return true;
    }
    return false
}
//Card object
class card {
    //Constructor for card
    constructor(pairnum, color) {
        //Set values on object
        this.color = color;
        this.pairnum = pairnum;
        //create object as a div element
        this.element = document.createElement("div");
        //set div element to card class
        this.element.classList.add("card");
        //card is this object
        var card = this;
        //set elemenet click 
        this.element.onclick = function () {
            card.flipcard(card);
        };
    }
    //flipcards method
    flipcard(card) {
        pickCard(card);
    }
    //add card to board
    addCardToBoard() {
        console.log("Tilføjere kort");
        board.appendChild(this.element);
    }

}
//shuffle cards
function shuffleCards() {
    cards.sort(function (a, b) { return 0.5 - Math.random() });
}