<%@ Control Language="C#" ClassName="TinyMce" %>

<script type="text/javascript" src="../usercontrol/tinymce_js/tiny_mce.js"></script>

<script type="text/javascript">
	tinyMCE.init({
		mode :"specific_textareas",
		editor_selector : "mceEditor",
		content_css : "../usercontrol/tinymce.css",
		theme : "advanced",
		theme_advanced_buttons1 : "bold,italic,underline,strikethrough,bullist,numlist,undo,redo,link,image,copy,pastetext,pasteword,cleanup",
		theme_advanced_buttons2 : "",
		theme_advanced_buttons3 : "",
		theme_advanced_path : false,
		theme_advanced_resizing : true,
		theme_advanced_resizing_use_cookie : false,
		theme_advanced_resize_horizontal : false,
		theme_advanced_statusbar_location : "bottom",
		theme_advanced_toolbar_align : "center",
		theme_advanced_toolbar_location : "top",
		
		plugins: "paste",
			paste_create_paragraphs : true,
			paste_create_linebreaks : false,
			paste_use_dialog : false,
			paste_auto_cleanup_on_paste : true,
			paste_convert_middot_lists : false,
			paste_unindented_list_class : "unindentedList",
			paste_convert_headers_to_strong : false
	});
</script>

