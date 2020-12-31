// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var xhttp = new XMLHttpRequest();


xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {

        var jn = JSON.parse(xhttp.responseText);
        results = jn;
        var options = "";
        for (var i = 0; i < jn.length; i++) {
            options += '<option value="' + i +'">' + jn[i].name + "</option>"; 
        }
        document.getElementById("inputGroupSelect01").innerHTML += options;
    }
};
xhttp.open("GET", "https://localhost:5001/api/BooksCategory", true);
xhttp.send();

function getBooks() {
    var xhttpBooks = new XMLHttpRequest();
    xhttpBooks.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var jn = JSON.parse(xhttpBooks.responseText);

            var tr = "";
            for (var i = 0; i < jn.length; i++) {
                tr += "<tr><th scope='row'>" + jn[i].id + "</th><td>" + jn[i].name + "</td><td>" + jn[i].isbn + "</td><td>" + jn[i].author.name + " " + jn[i].author.surname + "</td><td>" + jn[i].publisher + "</td></tr>";
            }
            document.getElementById("books").innerHTML = tr;
        }
    };
    xhttpBooks.open("GET", "https://localhost:5001/api/Books", true);
    xhttpBooks.send();

}

getBooks();


function selectedOption() {
    var index = document.getElementById("inputGroupSelect01").value;
    if (index == -1) {
        getBooks();
    } else {
        var options = "";

        for (var i = 0; i < results[index].subCategories.length; i++) {
            options += '<option value="' + results[index].subCategories[i].id + '">' + results[index].subCategories[i].name + "</option>";
        }
        document.getElementById("inputGroupSelect02").innerHTML = options;
        getBooksByCat();
    }
   
    
}


function getBooksByCat() {
    var id = document.getElementById("inputGroupSelect02").value;
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            var jn = JSON.parse(xhttp.responseText);
            console.log(jn);
            var tr = "";
            for (var i = 0; i < jn.length; i++) {
                tr += "<tr><th scope='row'>" + jn[i].id + "</th><td>" + jn[i].name + "</td><td>" + jn[i].isbn + "</td><td>" + jn[i].author.name + " " + jn[i].author.surname + "</td><td>" + jn[i].publisher +"</td></tr>";
            }
            document.getElementById("books").innerHTML = tr;
        }
    };
    xhttp.open("GET", "https://localhost:5001/api/Books?categoryId="+id, true);
    xhttp.send();
}
