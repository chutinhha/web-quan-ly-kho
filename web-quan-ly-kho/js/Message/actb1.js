function actb1(obj, ca, ka, obj1, ska, obj2){
	/* ---- Public Variables ---- */
	this.actb1_timeOut = -1; // Autocomplete Timeout in ms (-1: autocomplete never time out)
	this.actb1_lim = -1;    // Number of elements autocomplete can show (-1: no limit)
	this.actb1_firstText = false; // should the auto complete be limited to the beginning of keyword?
	this.actb1_mouse = true; // Enable Mouse Support
	this.actb1_delimiter = new Array(';',',');  // Delimiter for multiple autocomplete. Set it to empty array for single autocomplete
	this.actb1_startcheck = 1; // Show widget only after this number of characters is typed in.
	/* ---- Public Variables ---- */

	/* --- Styles --- */
	this.actb1_bgColor = '#006da5';
	this.actb1_textColor = '#FFFFFF';
	this.actb1_hColor = '#6b96c6';
	this.actb1_fFamily = 'Verdana';
	this.actb1_fSize = '11px';
	this.actb1_hStyle = 'text-decoration:underline;font-weight="bold"';
	/* --- Styles --- */

	/* ---- Private Variables ---- */
	var actb1_delimwords = new Array();
	var actb1_cdelimword = 0;
	var actb1_delimchar = new Array();
	var actb1_display = false;
	var actb1_pos = 0;
	var actb1_total = 0;
	var actb1_curr = null;	
	var actb1_rangeu = 0;
	var actb1_ranged = 0;
	var actb1_bool = new Array();
	var actb1_pre = 0;
	var actb1_toid;
	var actb1_tomake = false;
	var actb1_getpre = "";
	var actb1_mouse_on_list = 1;
	var actb1_kwcount = 0;
	var actb1_caretmove = false;
	this.actb1_keywords = new Array();
	//Son add here
	this.actb1_keys = new Array();	
	var actb1_subkeys = new Array();
	var actb1_subcurr = null;
	var actb1_frmobj = null;
	//Son add here	
	/* ---- Private Variables---- */
	
	this.actb1_keywords = ca;
	//Son add here
	this.actb1_keys = ka;
	actb1_subkeys = ska;
	actb1_subcurr = obj1;
	actb1_frmobj = obj2;
	var actb1_self = this;

	actb1_curr = obj;
	
	addEvent(actb1_curr,"focus",actb1_setup);
	function actb1_setup(){
		addEvent(document,"keydown",actb1_checkkey);
		addEvent(actb1_curr,"blur",actb1_clear);
		addEvent(document,"keypress",actb1_keypress);
	}

	function actb1_clear(evt){
		if (!evt) evt = event;
		removeEvent(document,"keydown",actb1_checkkey);
		removeEvent(actb1_curr,"blur",actb1_clear);
		removeEvent(document,"keypress",actb1_keypress);
		actb1_removedisp();
	}
	function actb1_parse(n){
		if (actb1_self.actb1_delimiter.length > 0){
			var t = actb1_delimwords[actb1_cdelimword].trim().addslashes();
			var plen = actb1_delimwords[actb1_cdelimword].trim().length;
		}else{
			var t = actb1_curr.value.addslashes();
			var plen = actb1_curr.value.length;
		}
		var tobuild = '';
		var i;

		if (actb1_self.actb1_firstText){
			var re = new RegExp("^" + t, "i");
		}else{
			var re = new RegExp(t, "i");
		}
		var p = n.search(re);
				
		for (i=0;i<p;i++){
			tobuild += n.substr(i,1);
		}
		tobuild += "<font style='"+(actb1_self.actb1_hStyle)+"'>"
		for (i=p;i<plen+p;i++){
			tobuild += n.substr(i,1);
		}
		tobuild += "</font>";
			for (i=plen+p;i<n.length;i++){
			tobuild += n.substr(i,1);
		}
		return tobuild;
	}
	function actb1_generate(){
		if (document.getElementById('tat_table')){ actb1_display = false;document.body.removeChild(document.getElementById('tat_table')); } 
		if (actb1_kwcount == 0){
			actb1_display = false;
			return;
		}
		a = document.createElement('table');
		a.cellSpacing='1px';
		a.cellPadding='2px';
		a.style.position='absolute';
		a.style.top = eval(curTop(actb1_curr) + actb1_curr.offsetHeight) + "px";
		a.style.left = curLeft(actb1_curr) + "px";
		a.style.backgroundColor=actb1_self.actb1_bgColor;
		a.id = 'tat_table';
		document.body.appendChild(a);
		var i;
		var first = true;
		var j = 1;
		if (actb1_self.actb1_mouse){
			a.onmouseout = actb1_table_unfocus;
			a.onmouseover = actb1_table_focus;
		}
		var counter = 0;
		for (i=0;i<actb1_self.actb1_keywords.length;i++){
			if (actb1_bool[i]){
				counter++;
				r = a.insertRow(-1);
				if (first && !actb1_tomake){
					r.style.backgroundColor = actb1_self.actb1_hColor;
					first = false;
					actb1_pos = counter;
				}else if(actb1_pre == i){
					r.style.backgroundColor = actb1_self.actb1_hColor;
					first = false;
					actb1_pos = counter;
				}else{
					r.style.backgroundColor = actb1_self.actb1_bgColor;
				}
				r.id = 'tat_tr'+(j);
				c = r.insertCell(-1);
				c.style.color = actb1_self.actb1_textColor;
				c.style.fontFamily = actb1_self.actb1_fFamily;
				c.style.fontSize = actb1_self.actb1_fSize;
				c.innerHTML = actb1_parse(actb1_self.actb1_keywords[i]);
				c.id = 'tat_td'+(j);
				c.setAttribute('pos',j);
				if (actb1_self.actb1_mouse){
					c.style.cursor = 'pointer';
					c.onclick=actb1_mouseclick;
					c.onmouseover = actb1_table_highlight;
				}
				j++;
			}
			if (j - 1 == actb1_self.actb1_lim && j < actb1_total){
				r = a.insertRow(-1);
				r.style.backgroundColor = actb1_self.actb1_bgColor;
				c = r.insertCell(-1);
				c.style.color = actb1_self.actb1_textColor;
				c.style.fontFamily = 'arial narrow';
				c.style.fontSize = actb1_self.actb1_fSize;
				c.align='center';
				replaceHTML(c,'\\/');
				if (actb1_self.actb1_mouse){
					c.style.cursor = 'pointer';
					c.onclick = actb1_mouse_down;
				}
				break;
			}
		}
		actb1_rangeu = 1;
		actb1_ranged = j-1;
		actb1_display = true;
		if (actb1_pos <= 0) actb1_pos = 1;
		actb1_showiframe();
	}
	function actb1_remake(){
		document.body.removeChild(document.getElementById('tat_table'));
		a = document.createElement('table');
		a.cellSpacing='1px';
		a.cellPadding='2px';
		a.style.position='absolute';
		a.style.top = eval(curTop(actb1_curr) + actb1_curr.offsetHeight) + "px";
		a.style.left = curLeft(actb1_curr) + "px";
		a.style.backgroundColor=actb1_self.actb1_bgColor;
		a.id = 'tat_table';
		if (actb1_self.actb1_mouse){
			a.onmouseout= actb1_table_unfocus;
			a.onmouseover=actb1_table_focus;
		}
		document.body.appendChild(a);
		var i;
		var first = true;
		var j = 1;
		if (actb1_rangeu > 1){
			r = a.insertRow(-1);
			r.style.backgroundColor = actb1_self.actb1_bgColor;
			c = r.insertCell(-1);
			c.style.color = actb1_self.actb1_textColor;
			c.style.fontFamily = 'arial narrow';
			c.style.fontSize = actb1_self.actb1_fSize;
			c.align='center';
			replaceHTML(c,'/\\');
			if (actb1_self.actb1_mouse){
				c.style.cursor = 'pointer';
				c.onclick = actb1_mouse_up;
			}
		}
		for (i=0;i<actb1_self.actb1_keywords.length;i++){
			if (actb1_bool[i]){
				if (j >= actb1_rangeu && j <= actb1_ranged){
					r = a.insertRow(-1);
					r.style.backgroundColor = actb1_self.actb1_bgColor;
					r.id = 'tat_tr'+(j);
					c = r.insertCell(-1);
					c.style.color = actb1_self.actb1_textColor;
					c.style.fontFamily = actb1_self.actb1_fFamily;
					c.style.fontSize = actb1_self.actb1_fSize;
					c.innerHTML = actb1_parse(actb1_self.actb1_keywords[i]);
					c.id = 'tat_td'+(j);
					c.setAttribute('pos',j);
					if (actb1_self.actb1_mouse){
						c.style.cursor = 'pointer';
						c.onclick=actb1_mouseclick;
						c.onmouseover = actb1_table_highlight;
					}
					j++;
				}else{
					j++;
				}
			}
			if (j > actb1_ranged) break;
		}
		if (j-1 < actb1_total){
			r = a.insertRow(-1);
			r.style.backgroundColor = actb1_self.actb1_bgColor;
			c = r.insertCell(-1);
			c.style.color = actb1_self.actb1_textColor;
			c.style.fontFamily = 'arial narrow';
			c.style.fontSize = actb1_self.actb1_fSize;
			c.align='center';
			replaceHTML(c,'\\/');
			if (actb1_self.actb1_mouse){
				c.style.cursor = 'pointer';
				c.onclick = actb1_mouse_down;
			}
		}
		actb1_showiframe();
	}
	function actb1_goup(){
		if (!actb1_display) return;
		if (actb1_pos == 1) return;
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_bgColor;
		actb1_pos--;
		if (actb1_pos < actb1_rangeu) actb1_moveup();
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_hColor;
		if (actb1_toid) clearTimeout(actb1_toid);
		if (actb1_self.actb1_timeOut > 0) actb1_toid = setTimeout(function(){actb1_mouse_on_list=0;actb1_removedisp();},actb1_self.actb1_timeOut);
	}
	function actb1_godown(){
		if (!actb1_display) return;
		if (actb1_pos == actb1_total) return;
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_bgColor;
		actb1_pos++;
		if (actb1_pos > actb1_ranged) actb1_movedown();
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_hColor;
		if (actb1_toid) clearTimeout(actb1_toid);
		if (actb1_self.actb1_timeOut > 0) actb1_toid = setTimeout(function(){actb1_mouse_on_list=0;actb1_removedisp();},actb1_self.actb1_timeOut);
	}
	function actb1_movedown(){
		actb1_rangeu++;
		actb1_ranged++;
		actb1_remake();
	}
	function actb1_moveup(){
		actb1_rangeu--;
		actb1_ranged--;
		actb1_remake();
	}

	/* Mouse */
	function actb1_mouse_down(){
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_bgColor;
		actb1_pos++;
		actb1_movedown();
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_hColor;
		actb1_curr.focus();
		actb1_mouse_on_list = 0;
		if (actb1_toid) clearTimeout(actb1_toid);
		if (actb1_self.actb1_timeOut > 0) actb1_toid = setTimeout(function(){actb1_mouse_on_list=0;actb1_removedisp();},actb1_self.actb1_timeOut);
	}
	function actb1_mouse_up(evt){
		if (!evt) evt = event;
		if (evt.stopPropagation){
			evt.stopPropagation();
		}else{
			evt.cancelBubble = true;
		}
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_bgColor;
		actb1_pos--;
		actb1_moveup();
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_hColor;
		actb1_curr.focus();
		actb1_mouse_on_list = 0;
		if (actb1_toid) clearTimeout(actb1_toid);
		if (actb1_self.actb1_timeOut > 0) actb1_toid = setTimeout(function(){actb1_mouse_on_list=0;actb1_removedisp();},actb1_self.actb1_timeOut);
	}
	function actb1_mouseclick(evt){
		if (!evt) evt = event;
		if (!actb1_display) return;
		actb1_mouse_on_list = 0;
		actb1_pos = this.getAttribute('pos');
		actb1_penter();
	}
	function actb1_table_focus(){
		actb1_mouse_on_list = 1;
	}
	function actb1_table_unfocus(){
		actb1_mouse_on_list = 0;
		if (actb1_toid) clearTimeout(actb1_toid);
		if (actb1_self.actb1_timeOut > 0) actb1_toid = setTimeout(function(){actb1_mouse_on_list = 0;actb1_removedisp();},actb1_self.actb1_timeOut);
	}
	function actb1_table_highlight(){
		actb1_mouse_on_list = 1;
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_bgColor;
		actb1_pos = this.getAttribute('pos');
		while (actb1_pos < actb1_rangeu) actb1_moveup();
		while (actb1_pos > actb1_ranged) actb1_movedown();
		document.getElementById('tat_tr'+actb1_pos).style.backgroundColor = actb1_self.actb1_hColor;
		if (actb1_toid) clearTimeout(actb1_toid);
		if (actb1_self.actb1_timeOut > 0) actb1_toid = setTimeout(function(){actb1_mouse_on_list = 0;actb1_removedisp();},actb1_self.actb1_timeOut);
	}
	/* ---- */

	function actb1_insertword1(a) {
		actb1_subcurr.value = a;
	}
	function actb1_insertword(a){
		if (actb1_self.actb1_delimiter.length > 0){
			str = '';
			l=0;
			for (i=0;i<actb1_delimwords.length;i++){
				if (actb1_cdelimword == i){
					prespace = postspace = '';
					gotbreak = false;
					for (j=0;j<actb1_delimwords[i].length;++j){
						if (actb1_delimwords[i].charAt(j) != ' '){
							gotbreak = true;
							break;
						}
						prespace += ' ';
					}
					for (j=actb1_delimwords[i].length-1;j>=0;--j){
						if (actb1_delimwords[i].charAt(j) != ' ') break;
						postspace += ' ';
					}
					str += prespace;
					str += a;
					l = str.length;
					if (gotbreak) str += postspace;
				}else{
					str += actb1_delimwords[i];
				}
				if (i != actb1_delimwords.length - 1){
					str += actb1_delimchar[i];
				}
			}
			actb1_curr.value = str;
			setCaret(actb1_curr,l);
		}else{
			actb1_curr.value = a;
		}
		actb1_mouse_on_list = 0;
		actb1_removedisp();
	}
	//Press Enter
	function actb1_penter(){
		if (!actb1_display) return;
		actb1_display = false;
		var word = '';
		var c = 0;
		/*
		for (var i=0;i<=actb1_self.actb1_keywords.length;i++){
			if (actb1_bool[i]) c++;
			if (c == actb1_pos){
				word = actb1_self.actb1_keywords[i];
				break;
			}
		}
		actb1_insertword(word);
		*/
		//Son add here
		var word1 = '';
		if (actb1_subkeys == null) {
			for (var i=0;i<=actb1_self.actb1_keywords.length;i++){
				if (actb1_bool[i]) c++;
				if (c == actb1_pos){
					word = actb1_self.actb1_keys[i];
					break;
				}
			}
			actb1_insertword(word);
		} else {
			for (var i=0;i<=actb1_self.actb1_keywords.length;i++){
				if (actb1_bool[i]) c++;
				if (c == actb1_pos){					
					word = actb1_self.actb1_keys[i];
					word1 = actb1_subkeys[i];
					break;
				}
			}
			actb1_insertword(word);		
			actb1_insertword1(word1);
		}
		l = getCaretStart(actb1_curr);
	}
	function actb1_removedisp(){
		if (actb1_mouse_on_list==0){
			actb1_display = 0;
			if (document.getElementById('tat_table')){ document.body.removeChild(document.getElementById('tat_table')); }
			if (actb1_toid) clearTimeout(actb1_toid);
		}
		actb1_hideiframe();
	}
	function actb1_keypress(e){
		if (actb1_caretmove) stopEvent(e);
		return !actb1_caretmove;
	}
	function actb1_checkkey(evt){
		if (!evt) evt = event;
		a = evt.keyCode;
		caret_pos_start = getCaretStart(actb1_curr);
		actb1_caretmove = 0;
		switch (a){
			case 38:
				actb1_goup();
				actb1_caretmove = 1;
				return false;
				break;
			case 40:
				actb1_godown();
				actb1_caretmove = 1;
				return false;
				break;
			case 13: case 9:
				if (actb1_display){
					actb1_caretmove = 1;
					actb1_penter();
					return false;
				}else{
					return true;
				}
				break;
			default:
				setTimeout(function(){actb1_tocomplete(a)},50);
				break;
		}
	}

	function actb1_tocomplete(kc){
		if (kc == 38 || kc == 40 || kc == 13) return;
		var i;
		if (actb1_display){ 
			var word = 0;
			var c = 0;
			for (var i=0;i<=actb1_self.actb1_keywords.length;i++){
				if (actb1_bool[i]) c++;
				if (c == actb1_pos){
					word = i;
					break;
				}
			}
			actb1_pre = word;
		}else{ actb1_pre = -1};
		
		if (actb1_curr.value == ''){
			actb1_mouse_on_list = 0;
			actb1_removedisp();
			return;
		}
		if (actb1_self.actb1_delimiter.length > 0){
			caret_pos_start = getCaretStart(actb1_curr);
			caret_pos_end = getCaretEnd(actb1_curr);
			
			delim_split = '';
			for (i=0;i<actb1_self.actb1_delimiter.length;i++){
				delim_split += actb1_self.actb1_delimiter[i];
			}
			delim_split = delim_split.addslashes();
			delim_split_rx = new RegExp("(["+delim_split+"])");
			c = 0;
			actb1_delimwords = new Array();
			actb1_delimwords[0] = '';
			for (i=0,j=actb1_curr.value.length;i<actb1_curr.value.length;i++,j--){
				if (actb1_curr.value.substr(i,j).search(delim_split_rx) == 0){
					ma = actb1_curr.value.substr(i,j).match(delim_split_rx);
					actb1_delimchar[c] = ma[1];
					c++;
					actb1_delimwords[c] = '';
				}else{
					actb1_delimwords[c] += actb1_curr.value.charAt(i);
				}
			}

			var l = 0;
			actb1_cdelimword = -1;
			for (i=0;i<actb1_delimwords.length;i++){
				if (caret_pos_end >= l && caret_pos_end <= l + actb1_delimwords[i].length){
					actb1_cdelimword = i;
				}
				l+=actb1_delimwords[i].length + 1;
			}
			var ot = actb1_delimwords[actb1_cdelimword].trim(); 
			var t = actb1_delimwords[actb1_cdelimword].addslashes().trim();
		}else{
			var ot = actb1_curr.value;
			var t = actb1_curr.value.addslashes();
		}
		if (ot.length == 0){
			actb1_mouse_on_list = 0;
			actb1_removedisp();
		}
		if (ot.length < actb1_self.actb1_startcheck) return this;
		if (actb1_self.actb1_firstText){
			var re = new RegExp("^" + t, "i");
		}else{
			var re = new RegExp(t, "i");
		}

		actb1_total = 0;
		actb1_tomake = false;
		actb1_kwcount = 0;
		for (i=0;i<actb1_self.actb1_keywords.length;i++){
			actb1_bool[i] = false;
			if (re.test(actb1_self.actb1_keywords[i])){
				actb1_total++;
				actb1_bool[i] = true;
				actb1_kwcount++;
				if (actb1_pre == i) actb1_tomake = true;
			}
		}

		if (actb1_toid) clearTimeout(actb1_toid);
		if (actb1_self.actb1_timeOut > 0) actb1_toid = setTimeout(function(){actb1_mouse_on_list = 0;actb1_removedisp();},actb1_self.actb1_timeOut);
		actb1_generate();
	}
	function actb1_showiframe() {
		var layer = document.getElementById('tat_table');
		/*
		var iframe = document.getElementById('iframe');
		iframe.style.display = 'block';
		iframe.style.width = layer.offsetWidth;
		iframe.style.height = layer.offsetHeight;
		iframe.style.left = layer.offsetLeft;
		iframe.style.top = layer.offsetTop;
		*/
		actb1_frmobj.style.display = 'block';
		actb1_frmobj.style.width = layer.offsetWidth;
		actb1_frmobj.style.height = layer.offsetHeight;
		actb1_frmobj.style.left = layer.offsetLeft;
		actb1_frmobj.style.top = layer.offsetTop;		
	}

	function actb1_hideiframe() {
		// hide IFRAME
		//var iframe = document.getElementById('iframe');
		//iframe.style.display = 'none';
		actb1_frmobj.style.display = 'none';
	}	
	return this;
}