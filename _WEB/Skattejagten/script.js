var ctx;
var chest;
var treasuremap;
var canvas;
var chestX;
var chestY;
var atempts = 0;
var failurerange = 20;
var xHint;

window.onload = function () {

    canvas = document.getElementById("treasuremap");

    ctx = canvas.getContext("2d");


    //make treasuremap as an variable 
    treasuremap = new Image();
    treasuremap.src = "treasuremap.png";

    //wait for image
    treasuremap.onload = function () {
        drawTreasureMap();
    };

    canvas.addEventListener("click", function (event) {

        var x = event.pageX - canvas.offsetLeft - canvas.clientLeft;
        var y = event.pageY - canvas.offsetTop - canvas.clientTop;

        findTreasure(x, y);
    });

    setChestCordi();

    // showChest();
};

function drawTreasureMap() {
    //clear canvas 
    ctx.clearRect(0, 0, canvas.height, canvas.width);

    //Draw canvas
    ctx.drawImage(treasuremap, 0, 0)

};

function reset(){
    location.reload();
}
function findTreasure(x, y) {
    //foreach atempt the global variable will be one higher
    atempts++;

    if (isChestHere(x, y)) {

        drawTreasureMap();
        console.log("Jep");
        showChest();
        // ctx.clearRect(0, 0, canvas.height, canvas.width);

        StartConfetti();
        console.log();

        setTimeout(reset,15000);
        window.test
    }
    else {
        //if user had tried 5 times then the game will be reset
        if (atempts == 5) {
            //face in fade out chest
            showX();
        }
        showHint(x, y);
    }

    console.log("Coordinate x: " + x, "Coordinate y: " + y, atempts);
};

function showChest() {
    chest = new Image();
    chest.src = "chest.png";

    chest.onload = function () {
        drawImage(chest);
    }
};

function showX() {
    xHint = new Image();
    xHint.src = "X.png";
    xHint.onload = function () {

        drawImage(xHint);
    }
}

function drawImage(image) {
    //draw chest in the random generated cordinates and max height and width 20px
    ctx.drawImage(image, chestX, chestY, 20, 20);

};

function setChestCordi() {
    //generate a random integer to chest x cordinates 
    chestX = Math.floor(Math.random() * (893 - 221)) + 221;

    //generate a random integer to chest y cordinates 
    chestY = Math.floor(Math.random() * (606 - 334)) + 334;

    console.log(chestX, chestY);
}

function isChestHere(x, y) {
    if (((chestX - x) >= -failurerange) && ((chestX - x) <= failurerange)) {
        if (((chestY - y) >= -failurerange) && ((chestY - y) <= failurerange)) {
            return true;
        }
    }
    return false;
}

function showHint(x, y) {
    var hintText = "";
    if (x > chestX) {
        //go to left
        hintText += "Go to left ";
    }
    else if (x < chestX) {
        //go to right
        hintText += "Go to right";

    }

    if (y > chestY) {
        //go up
        hintText += " Go up";

    }
    else if (y < chestY) {
        //go down
        hintText += " Go down";
    }

    document.getElementById("hint").innerHTML = hintText;
    document.getElementById("divhint").style.display = "block";

}