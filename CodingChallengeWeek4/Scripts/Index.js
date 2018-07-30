var data;

// On load
window.onload = function () {
    getHeroes();
};

// Populate page
function getHeroes() {
    var requestURL = 'https://mdn.github.io/learning-area/javascript/oojs/json/superheroes.json';
    var request = new XMLHttpRequest();
    request.open('GET', requestURL);
    request.responseType = 'json';
    request.send();
    request.onload = function () {
        data = request.response;
        populateHeader();
        showHeroes();
    }
}

function populateHeader(heroes) {
    let header = document.querySelector("header");

    let h3 = document.createElement('h3');
    h3.setAttribute("id", "squadTitle");
    h3.innerText = "Super Hero Squad";

    let h4 = document.createElement('h4');
    h4.setAttribute("id", "heroInfo");
    h4.innerText = data.homeTown + "//" + data.formed;

    header.appendChild(h3);
    header.appendChild(h4);
}

function showHeroes(heroes) {
    var section = document.querySelector("section");

    let table = document.createElement('table');
    table.setAttribute("width", "100%");
    table.setAttribute("border", "2");

    // Header
    let headerRow = document.createElement('tr');
    headerRow.setAttribute("width", "100%");

    for (hero in data.members) {
        let td = document.createElement('td');
        td.innerText = data.members[hero].name;
        td.setAttribute("width", "33%");
        headerRow.appendChild(td);
    }
    table.appendChild(headerRow);

    // Identity
    let idRow = document.createElement('tr');
    idRow.setAttribute("width", "100%");

    for (hero in data.members) {
        let td = document.createElement('td');
        td.innerText = "Secret identity: " + data.members[hero].secretIdentity;
        td.setAttribute("width", "33%");
        idRow.appendChild(td);
    }
    table.appendChild(idRow);

    // Age
    let ageRow = document.createElement('tr');
    ageRow.setAttribute("width", "100%");

    for (hero in data.members) {
        let td = document.createElement('td');
        td.innerText = "Age: " + data.members[hero].age;
        td.setAttribute("width", "33%");
        ageRow.appendChild(td);
    }
    table.appendChild(ageRow);

    // Powers
    let powersRow = document.createElement('tr');
    powersRow.setAttribute("width", "100%");
    table.setAttribute("text-align", "center");

    for (hero in data.members) {
        let td = document.createElement('td');
        td.innerText = "Super powers:";
        td.setAttribute("width", "33%");
        // power list
        let list = document.createElement('ul');
        for (power in data.members[hero].powers) {
            let item = document.createElement('li');
            item.innerText = data.members[hero].powers[power];
            list.appendChild(item);
        }

        td.appendChild(list);
        powersRow.appendChild(td);
    }
    table.appendChild(powersRow);

    section.appendChild(table);
}