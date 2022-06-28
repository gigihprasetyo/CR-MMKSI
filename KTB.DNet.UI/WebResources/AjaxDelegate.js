function AjaxDelegate(url, callback)
{
	this.url = url;
	this.callback = callback;
	this.callbackArguments = arguments;
	this.Fetch = ajaxFetch;
	this.request = null;
}
function ajaxFetch()
{
	var request = this.request;
	var callback = this.callback;
	var callbackArguments = this.callbackArguments;
	if(window.XMLHttpRequest) {
		try {
			request = new XMLHttpRequest();
		} catch(e) {
			request = null;
		}
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
	if(request) {
		request.onreadystatechange = function () {
										if(request.readyState == 4) {
											if(request.status == 200) {
												callbackArguments[1] = request.responseText;
												callback.apply(this, callbackArguments);
											} else {
											}
											request = null;
										}
									}
		request.open("GET", this.url, true);
		request.send("");
	}
}