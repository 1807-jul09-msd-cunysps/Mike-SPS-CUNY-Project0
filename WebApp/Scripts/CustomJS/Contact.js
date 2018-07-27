// https://www.zipcodeapi.com/API API key
const zipcodeApiKey = "js-8CmO1wuQQBeCe7z5bIChCXAOTbwaJm8reizkWEG8Gmg5IhAcqOfUQQoYxHq5lAFm";

// Init listeners after page load
window.onload = function () {
    init();
};

// Initialize variables
function init() {
    // Add listener to zipcodes
    document.querySelector("#inputZipcode").addEventListener("blur", zipcodePopCityState);
    document.querySelector("#inputPrimZipcode").addEventListener("blur", zipcodePopPrimCityState);
}

// Generic AJAX for GET requests
function ajaxGet(url, cbFunction) {                 
    var req;                                        
    if (window.XMLHttpRequest) {                  
        req = new XMLHttpRequest();
    } else {                                       
        req = new ActiveXObject("Microsoft.XMLHTTP");
    }
    req.onreadystatechange = function () {        
        if (this.readyState == 4 &&
            this.status == 200) {
            cbFunction(this);                      
        }
    };
    req.open("GET", url, true);                   
    req.send();                                     
}

// Populate city and state for Address Info
function zipcodePopCityState(e) {
    let zipcode = e.target.value;
    let url = "https://www.zipcodeapi.com/rest/" + zipcodeApiKey + "/info.json/" + zipcode + "/radians";
    var cbFunc = function (req) {
        var data = JSON.parse(req.responseText);
        document.querySelector("#inputCity").value = data.city;
        document.querySelector("#inputState").value = data.state;
    }
    ajaxGet(url, cbFunc);
}

// Populate city and state for Primary Address Info
function zipcodePopPrimCityState(e) {
    let zipcode = e.target.value;
    let url = "https://www.zipcodeapi.com/rest/" + zipcodeApiKey + "/info.json/" + zipcode + "/radians";
    var cbFunc = function (req) {
        var data = JSON.parse(req.responseText);
        document.querySelector("#inputPrimCity").value = data.city;
        document.querySelector("#inputPrimState").value = data.state;
    }
    ajaxGet(url, cbFunc);
}

// Function for showing Primary Address div
function togglePrimAddr() {
    var selector = document.querySelector("#isPrimAddr").value;
    var primAddrDiv = document.querySelector("#primaryAddr");
    if (selector == "No") {
        primAddrDiv.style.display = "block";
    }
    else {
        primAddrDiv.style.display = "none";
    }
}
