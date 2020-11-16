//All comments are below code being discussed
//var num = 0;
var vehiclepng = 
[
	"assets/vehicle-land-1.jpg", 
	"assets/vehicle-water-1.png", 
	"assets/vehicle-air-1.jpg",
	"assets/vehicle-space-2.jpg",
	
];
var vehicleindex = 0;
var vehicleimage;
window.onload = function(){
	
	//confirm (num);
	//tester to know if the code is being run
	if ($('body.order').length > 0){
	vehicleimage  = document.getElementById("vehicle-image");
	onvehiChanged();
	}
}

function onvehiChanged(){
	updatevehiclename();
	vehicleimage.src = vehiclepng[vehicleindex];
	
}
function updatevehiclename(){
	var vehiclename = document.getElementById("vehicle-name");
	switch(vehicleindex){
	case 0:
	vehiclename.value = "Land";
	break;
	case 1:
	vehiclename.value = "Sea";
	break;
	case 2:
	vehiclename.value = "Air";
	break;
	case 3:
	vehiclename.value = "Space";
	break;
	}
}
function onVehicleChange(increment){
	var incrementIndex = (vehicleindex + increment);
	
	//If it's negative, set the index to the last image:
	if(incrementIndex < 0)
		vehicleindex = vehiclepng.length + increment;
	
	//Otherwise just add the offset and modulo by the length to "wrap around" the values
	else
		vehicleindex = (vehicleindex + increment) % vehiclepng.length;
	
	//Fire callback for when the body has changed
	onvehiChanged();
	
}

function PictureClicked() {
	var opti = document.getElementsByClassName("optionchoice");
	for(i=0; i<opti.length; i++) {
    opti[i].style.display = 'none';
  }
	document.getElementById("spacenone").style.display = "block";
	document.getElementById("option" + vehicleindex).style.display = "block";
	document.getElementById("Cart").style.display = "block";
}


var latlng = {lat: 53.228927, lng: -0.549473}; ;
function initMap() { 
		//variable that takes in langitude and longitude
        var map = new google.maps.Map(document.getElementById('map'),  {
		//grabs map tag from html document
          zoom: 12,
		  //determines initial zoom of the map
          center: latlng
        });
        var marker = new google.maps.Marker({
          position: latlng,
          map: map
        });
      }
	  // code above taken from googlemaps but moved into the javascript file and editted by me
	  //var Not = document.getElementById("NotWorking");
function Geolocation() {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(showPosition,showError);
		initMap();
		//var map = document.getElementById("map")
		//map.fitbound(latlng)
    } 
	else { 
        document.getElementById("NotWorking").innerHTML = "Geolocation is not supported by this browser."
		;}
}

function showPosition(position) {
		var latat = position.coords.latitude		
		var longi = position.coords.longitude;
		latlng = {lat: latat, lng: longi};;
}
function showError(error) {
    switch(error.code) {
        case error.PERMISSION_DENIED:
            document.getElementById("NotWorking").innerHTML = "User denied the request for Geolocation."
			//latlng = {lat: 53.228927, lng: -0.549473};
            break;
        case error.POSITION_UNAVAILABLE:
            document.getElementById("NotWorking").innerHTML = "Location information is unavailable."
			//latlng = {lat: 53.228927, lng: -0.549473};
            break;
        case error.TIMEOUT:
            document.getElementById("NotWorking").innerHTML = "The request to get user location timed out."
			//latlng = {lat: 53.228927, lng: -0.549473};
            break;
        case error.UNKNOWN_ERROR:
            document.getElementById("NotWorking").innerHTML = "An unknown error occurred."
			//latlng = {lat: 53.228927, lng: -0.549473};
            break;
    }
}

//$('#cartcontent').datagrid({
    //url:'datagrid_data.json',
    //columns:[[
      //  {field:'code',title:'Code',width:100},
      //  {field:'name',title:'Name',width:100},
        //{field:'price',title:'Price',width:100,align:'right'}
   // ]]
//});


/*
window.onload = function() - is loaded on startup, anything you want to happen when opening the html page
alert - basic pop up message
confirm - a pop up message with ok and cancel options, if they click ok confirm is set to true
prompt - a pop up message with a user inputtable text line, use an if (output != null) to access what is said
\n - go to next line in a "" string

*/