var totalCost =0;
var data = {"total":0,"rows":[]};
var row;
var pop = false;
var count = 1;
//$.fn.datagrid.defaults;
$(function(){
	
	$("#image").draggable({containment: ".top", scroll: false, revert: true, //helper: "clone"
	});
	
	
	if ($('body.order').length > 0){
		
	$(".item").draggable({
		containment: "body", 
		revert: true,
		//helper: "clone",
		//Proxy: "clone" would have used clone but images went back to original size
		drag: function( event, ui ) {
		$(this).css('z-index',10);
		}		
      });

	$("#Cart").droppable({	
		drop: function( event, ui // e, source
		){
		var name = $(ui.draggable).find('p.title').text();
		var price = $(ui.draggable).find('p.title').attr("data");
		addProduct(name, parseFloat(price), data);
		//$(ui.draggable).revert();
		//$( this ).find( "th" ).html("dropped");
		//console.log(name);
		}
	});
	
	//$('#cartcontent').datagrid({
		//url: 'datagrid.json',
		//columns: [[
		//{field="name" width="120" title="Name"},
		//{field="quantity" width="40" align="right" title= "Quantity"},
		//{field="price" width="60" align="right" title="Price"},
		
		
		//]]
	//});
	
	
	
	}
	//console.log("working");
});

function addProduct(n , p, d){
	count = 1;
	d.rows.push({name:n,quantity:1,price:p});
	var i =0;
	while(i<d.total)// for(var i=0; i<d.total; i++)
	{
			if (d.rows[i].name == n
			){
				d.rows[i].quantity +=1;
				
				//d.rows.pop();
				//i = d.total - 1;
				return;
			}
		i++;
		console.log(d.rows[i].quantity);
	}
	
	
	for(var f=0; f<=d.total; f++){
	$('#item'+count).replaceWith('<tr class = "Products"id = "item"'+count+'><td>'+d.rows[f].name+'</td><td>'+d.rows[f].quantity+'</td><td>'+d.rows[f].price+'</td></tr>');
	count++;
	}
	d.total++;
	//$('#cartcontent').append();
	//$('#cartcontent').datagrid('loadData', d);
	//$('#item'+count).append('<td>'+n+'</td><td></td><td>'+p+'</td>');
	totalCost += p;
	$('p.total').html('Total: £'+totalCost);
	
}




//$(document).on('mousedown','.item', function() {
//	$(this).css('z-index', '10');
//});

//$(window).on('resize', function() {
   // var currCenter = map.getCenter();
   // google.maps.event.trigger(map, 'resize');
   // map.setCenter(currCenter);
//})

$( function() {
    $( "#accordion" ).accordion();
} );
