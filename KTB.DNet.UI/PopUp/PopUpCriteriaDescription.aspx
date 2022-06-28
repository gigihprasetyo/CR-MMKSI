<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpCriteriaDescription.aspx.vb" Inherits="PopUpCriteriaDescription" smartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Deskripsi Kriteria</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../WebResources/stylesheet.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../WebResources/InputValidation.js"></script>
		<base target="_self">
		<script language="javascript">
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="5" width="100%" border="1">
				<tr>
					<th style="COLOR: white" bgColor="#cc3333" colSpan="4">
						Sample Data
					</th>
				</tr>
				<TR>
					<TD style="WIDTH: 50px" align="center">NO</TD>
					<TD style="WIDTH: 205px" align="center">KODE DEALER</TD>
					<TD align="center">NAMA DEALER</TD>
					<TD align="center">TANGGAL</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 50px" align="center">1</TD>
					<TD style="WIDTH: 205px">100001</TD>
					<TD>SUMATERA BERLIAN MOTORS, PT</TD>
					<TD>15/10/2001</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 50px" align="center">2</TD>
					<TD style="WIDTH: 205px">100002</TD>
					<TD>KERINCI PERMATA MOTORS, PT</TD>
					<TD>30/6/2004</TD>
				</TR>
			</table>
			&nbsp;&nbsp;
			<table cellSpacing="0" cellPadding="5" width="100%" border="1">
				<tr>
					<th style="WIDTH: 197px; COLOR: white" width="197" bgColor="#cc3333">
						Operator
					</th>
					<th style="WIDTH: 306px; COLOR: white" width="306" bgColor="#cc3333">
						Deskripsi&nbsp;
					</th>
					<th style="COLOR: white" bgColor="#cc3333">
						Sample&nbsp;
					</th>
				</tr>
				<TR>
					<TD style="WIDTH: 197px">And</TD>
					<TD style="WIDTH: 306px">Pada operator AND memungkinkan adanya beberapa kondisi. 
						Semua kondisi yang dipisahkan oleh AND harus terpenuhi</TD>
					<TD>
						<P>[KODE DEALER]&nbsp;= 100001 AND [KODE DEALER]&nbsp;= 100002</P>
						<P>Hasil : tidak ada data, karena kedua kondisi tidak terpenuhi</P>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 197px">Or</TD>
					<TD style="WIDTH: 306px">Pada operator&nbsp;OR memungkinkan adanya beberapa 
						kondisi. Salah satu dari kondisi yang dipisahkan oleh OR terpenuhi</TD>
					<TD>
						<P>[KODE DEALER]&nbsp;= 100001 OR [KODE DEALER]&nbsp;= 100002</P>
						<P>Hasil : data no. 1 dan 2, karena kedua kondisi terpenuhi semua</P>
					</TD>
				</TR>
				<tr>
					<td style="WIDTH: 197px">sama dengan ( = )
					</td>
					<td style="WIDTH: 306px">
						Memeriksa apakah nilai dari dua operand adalah sama atau tidak, jika ya maka 
						kondisi menjadi benar.
					</td>
					<td>
						<P>[KODE DEALER] = 100001</P>
						<P>Hasil : data no. 1. Karena data dengan nilai [KODE DEALER] = 100001 hanya 1 
							record</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">tidak sama dengan ( &lt;&gt; )
					</td>
					<td style="WIDTH: 306px">Memeriksa apakah nilai dari dua operand adalah sama atau 
						tidak, jika nilai tidak sama maka kondisi menjadi benar.
					</td>
					<td>
						<P>[KODE DEALER] &lt;&gt; 100001</P>
						<P>Hasil :&nbsp;data yang ditampilkan selain data dengan [KODE DEALER] = 100001</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">sebagian sama dengan ( Like %XXX% )
					</td>
					<td style="WIDTH: 306px">
						Operator LIKE digunakan untuk membandingkan nilai terhadap nilai-nilai yang 
						sama dengan menggunakan operator wildcard.
					</td>
					<td>
						<P>[NAMA DEALER] LIKE %MATA%</P>
						<P>Hasil :&nbsp;data no.2. Karena [NAMA DEALER] yang ada kata 'MATA' adalah KERINCI 
							PERMATA MOTORS</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">diawali dengan ( Like XXX% )
					</td>
					<td style="WIDTH: 306px">
						Operator LIKE digunakan untuk membandingkan nilai terhadap nilai-nilai yang 
						diawali dengan karakter yang dimaksud.
					</td>
					<td>
						<P>[NAMA DEALER] LIKE KERINCI%</P>
						<P>Hasil :&nbsp;data no.2. Karena [NAMA DEALER] yang&nbsp;diawali kata 'KERINCI' 
							adalah KERINCI PERMATA MOTORS</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">diakhiri dengan ( Like %XX )
					</td>
					<td style="WIDTH: 306px">
						Operator LIKE digunakan untuk membandingkan nilai terhadap nilai-nilai yang 
						diakhiri dengan karakter yang dimaksud.
					</td>
					<td>
						<P>[KODE DEALER] &lt;&gt; 100001</P>
						<P>Hasil :&nbsp;data yang ditampilkan selain data dengan [KODE DEALER] = 100001</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">lebih besar dari ( &gt; )
					</td>
					<td style="WIDTH: 306px">
						Cek jika nilai operan kiri lebih besar dari nilai operan kanan, jika ya maka 
						kondisi menjadi benar.
					</td>
					<td>
						<P>[TANGGAL] &gt; 1/1/2004</P>
						<P>Hasil :&nbsp;data no 2.</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">lebih kecil dari ( &lt; )
					</td>
					<td style="WIDTH: 306px">Cek jika nilai operan kiri kurang dari nilai operan kanan, 
						jika ya maka kondisi menjadi benar.
					</td>
					<td>
						<P>[TANGGAL]&nbsp;&lt; 1/1/2004</P>
						<P>Hasil :&nbsp;data no. 1.</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">lebih besar / sama dengan ( &gt;= )
					</td>
					<td style="WIDTH: 306px">
						Cek jika nilai operan kiri lebih besar dari atau sama dengan nilai operan 
						kanan, jika ya maka kondisi menjadi benar.
					</td>
					<td>
						<P>[TANGGAL] &gt; 1/1/2004</P>
						<P>Hasil :&nbsp;data no 2.</P>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 197px">lebih kecil / sama dengan ( &lt;= )
					</td>
					<td style="WIDTH: 306px">Cek jika nilai operan kiri kurang dari atau sama dengan 
						nilai operan kanan, jika ya maka kondisi menjadi benar.
					</td>
					<td>
						<P>[TANGGAL]&nbsp;&lt; 1/1/2004</P>
						<P>Hasil :&nbsp;data no. 1.</P>
					</td>
				</tr>
				<TR>
					<TD style="WIDTH: 197px">terdiri dari ( In )</TD>
					<TD style="WIDTH: 306px">Operator IN digunakan untuk membandingkan nilai ke daftar 
						nilai literal yang telah ditetapkan.</TD>
					<TD>
						<P>[KODE DEALER]&nbsp;IN 100001,100002</P>
						<P>Hasil : data no. 1 dan 2, karena
						</P>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 197px">tidak terdiri dari ( Not In )</TD>
					<TD style="WIDTH: 306px">Operator NOT membalikkan makna dari operator logis dengan 
						yang digunakan.</TD>
					<TD>
						<P>[KODE DEALER]&nbsp;NOT IN 100001,100002</P>
						<P>Hasil : tidak ada data, karena tidak ada data <STRONG>selain </STRONG>100001,100002</P>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
