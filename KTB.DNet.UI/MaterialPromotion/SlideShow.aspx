<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SlideShow.aspx.vb" Inherits="SlideShow" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SlideShow</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" type="text/javascript">

		var allSlides;
		var currSlide = 0;
		var slideInterval;


		function initSlides() 
		{
			
			var myDate = new Date( );
			getAllSlides();
			slideInterval = setInterval("nexttimer()", 4000);
		}

		function getAllSlides() {
			var allChildren = document.getElementById("pnlSlideShow").childNodes;
			
			var slideElems = new Array( );
			var x=0;
			for (var i = 0; i < allChildren.length-1; i++) 
			{
				
				if (allChildren[i].id != null)
				{	
								
					if (allChildren[i].id.substring(0,7) == "MyImage") 
					{
						slideElems[slideElems.length] = allChildren[i];
						x++;
					}
				}
			}
			allSlides = slideElems;
		}


		function displayslide(indexslide)
		{
			allSlides[currSlide].style.display = "none";
			currSlide=indexslide-1;
			next( );
		}

		function next( ) 
		{
			if (currSlide < 0) 
			{
				allSlides[++currSlide].style.display = "block";
			} 
			else if (currSlide < allSlides.length - 1) 
			{
				allSlides[currSlide].style.display = "none";
				allSlides[++currSlide].style.display = "block";
			} else if (currSlide == allSlides.length - 1) 
			{
				allSlides[currSlide].style.display = "none";
				allSlides[0].style.display = "block";
				currSlide=0;
			}
			

			clearInterval(slideInterval);
			slideInterval = setInterval("nexttimer()", 4000);
		}

		function previous( ) 
		{
			if (currSlide == 0) 
			{
				allSlides[0].style.display = "none";
				currSlide=allSlides.length - 1;
				allSlides[currSlide].style.display = "block";
			} 
			else if (currSlide <= allSlides.length - 1) 
			{
				allSlides[currSlide].style.display = "none";
				allSlides[currSlide-1].style.display = "block";
				currSlide=currSlide-1;
			} 
			

			clearInterval(slideInterval);
			slideInterval = setInterval("nexttimer()", 4000);
		}


		function nexttimer() 
		{
			next( );
		}

		
		</script>
	</HEAD>
	<body onload="initSlides();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td align="center"><asp:panel id="pnlSlideShow" runat="server" Height="100px" Width="100px" BorderStyle="Solid"
							BorderWidth="1px"></asp:panel><asp:image id="Image1" style="CURSOR: hand" onclick="previous();" runat="server" ImageUrl="../images/page_prev.gif"></asp:image>&nbsp;&nbsp; 
						&nbsp; &nbsp;&nbsp;&nbsp;
						<asp:image id="Image2" style="CURSOR: hand" onclick="next();" runat="server" ImageUrl="../images/page_next.gif"></asp:image></td>
				</tr>

			</table>
		</form>
	</body>
</HTML>
