	var divs = $('ptnotice_none').getElementsByTagName('div');
	var adobj = null;

	for(var i = 0; i < divs.length; i++) {
		if(divs[i].id.substr(0, 3) == 'ptnotice_' && (adobj = $(divs[i].id.substr(0, divs[i].id.length - 5)))) {
			if(divs[i].innerHTML) {
				evalscript(divs[i].innerHTML);
				adobj.innerHTML = divs[i].innerHTML;
				adobj.className = divs[i].className;
			}

		}
	}
	$('ptnotice_none').parentNode.removeChild($('ptnotice_none'));
