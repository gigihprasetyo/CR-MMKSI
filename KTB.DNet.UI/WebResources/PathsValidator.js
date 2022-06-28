function allPathsOk( theForm )
			{				
				if ( theForm != null ) {
					var len = theForm.elements.length;
					var pathstart = /^[a-zA-Z]:\\/;
					var empty = true;
					for ( var i = 0; i < len; ++i ) {
						if ( theForm.elements[i].type == "file" ) {
							var theValue = theForm.elements[i].value;
							if ( theValue == "" ) {
							alert( "Nama file tidak valid" );
							return false;
						}
						empty = false;
						if ( ! theValue.match( pathstart )) {
							theForm.elements[i].value="";
							alert( "Nama file tidak valid" );
							theForm.elements[i].focus();
							return false;
						}
					}
				}
				
				if ( empty ) {
					alert( "File Tidak Ditemukan" );
					theForm.elements[i].focus();
					return false;
				}
				  return true;
				 }
	}
	
	function PathsOk( theForm )
				{				
				if ( theForm != null ) {
					var len = theForm.elements.length;
					var pathstart = /^[a-zA-Z]:\\/;
					var empty = true;
					for ( var i = 0; i < len; ++i ) {
						if ( theForm.elements[i].type == "file" ) {
							var theValue = theForm.elements[i].value;
							if ( theValue == "" ) {
							return true;
						}
						empty = false;
						if ( ! theValue.match( pathstart )) {
							theForm.elements[i].value="";
							alert( "Nama file tidak valid" );
							theForm.elements[i].focus();
							return false;
						}
					}
				}
				
				if ( empty ) {
					alert( "File Tidak Ditemukan" );
					theForm.elements[i].focus();
					return false;
				}
				  return true;
				 }
	}