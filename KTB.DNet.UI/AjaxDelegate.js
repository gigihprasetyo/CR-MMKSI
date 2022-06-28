<!--
/*
AjaxDelegate(url, callback)
	url = the url of the page that will do the server-side processing
	callback = the name of the function to call once the call has completed
	
	Any number of additional arguments can also be specified. The extra 
	arguments will be available to the callback function when it is called.
	
	The callback function will receive the following arguments:
		callback(url, response, [argument[0]], etc.)
	where:
		url = the url of the page that did the server-side processing
		response = the actual HTTP responseText returned from the call
		[argument[0]], etc. = the remaining arguments that were originally passed to the AjaxDelegate constructor
*/
function AjaxDelegate(url, callback)
{
	// basic properties
	this.url = url;
	this.callback = callback;
	this.callbackArguments = arguments;

	// methods
	this.Fetch = ajaxFetch;

	// XmlHttpRequest object
	this.request = null;
}

/*
ajaxFetch()
	Asynchronously calls the url specified in the AjaxDelegate constructor.
	When the call completes, the callback specified in the AjaxDelegate constructor
	is called, passing the responseText data in.
*/
function ajaxFetch()
{
	// this gets the variables into a local scope
    var request = this.request;
    var callback = this.callback;
    var callbackArguments = this.callbackArguments;

    // branch for native XMLHttpRequest object
    if(window.XMLHttpRequest) {
    	try {
			request = new XMLHttpRequest();
        } catch(e) {
			request = null;
        }
    // branch for IE/Windows ActiveX version
    } else if(window.ActiveXObject) {
       	try {
        	request = new ActiveXObject("Msxml2.XMLHTTP");
      	} catch(e) {
        	try {
          		request = new ActiveXObject("Microsoft.XMLHTTP");
        	} catch(e) {
          		request = null;
        	}
		}
    }

	// if we were able to create the XmlHTTPRequest object, we can make the request
	if(request) {
		request.onreadystatechange = function () {
										if(request.readyState == 4) {
											if(request.status == 200) {
												// we replace the second argument (callback function) with the response data)
												// kind of cheesy, but it is nice and easy and works well
												callbackArguments[1] = request.responseText;
												callback.apply(this, callbackArguments);
											} else {
												alert("There was a problem retrieving the XML data:\n" + request.statusText);
											}

											// clean up
											request = null;
										}
									}
		request.open("GET", this.url, true);
		request.send("");
	}
}
//-->
