// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
    ev.target.style.margin = "0"; // Remove margin when dragging starts
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");

    // Find the receiving box by its class name
    var receivingBox = ev.target.closest('.box');

    if (receivingBox) {
        // Append the draggable element to the receiving box
        receivingBox.appendChild(document.getElementById(data));
    }
}

function checkHeight(id) {

    // Determine how many draggable elements are in the receiving box
    var target = document.getElementById(id);
    var numChildren = target.getElementsByClassName('draggable').length;

    if (numChildren == 0) {
        target.style.height = '35px'; //Receiving box is set to 35px if it is empty
    }
    else if (numChildren >= 1) {
        target.style.height = (numChildren) * (35) + 'px'; // Height of each draggable element + margin + additional margin for the last element
    }
}