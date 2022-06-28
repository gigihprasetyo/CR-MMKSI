<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:variable name="ImageDir" select="'images/'" />
	<!-- Get the Nodeset for the screen which is currently selected -->
	<xsl:variable name="SelectedId" select="//Menu[Selected=1]" />
	<xsl:template match="text()" />
	<!-- Start parsing the whole XML -->
	<xsl:template match="MenuScreen">
		<input type="hidden" name="imageDir" id="imageDir">
			<xsl:attribute name="value">
				<xsl:value-of select="$ImageDir" />
			</xsl:attribute>
		</input>
		<!-- Call RecursiveLoop function to get the value of Top menu for currently selected Menu Item -->
		<div id="divTree" name="divTree">
			<table class="menuTable">
				<xsl:for-each select="Menu[ParentMenuId=0]">
					<xsl:variable name="vParentMenuId" select="child::AppMenuId" />
					<!-- If the Top menu is the selected node then do not paint Child Menus -->
					<xsl:choose>
						<xsl:when test="((count(//Menu[ParentMenuId = $vParentMenuId]) > 0) and ($SelectedId/AppMenuId != $vParentMenuId) ) ">
							<tr></tr>
							<tr>
								<td class="menuImage">
									<xsl:choose>
										<xsl:when test="child::SelectedParent=1">
											<input type="hidden" name="SEL_TOP" id="SEL_TOP">
												<xsl:attribute name="value">
													<xsl:value-of select="$vParentMenuId" />
												</xsl:attribute>
											</input>
											<img onclick="menuLeftImageClick(this, '')">
												<xsl:attribute name="src">
													<xsl:value-of select="$ImageDir" />
													<xsl:text disable-output-escaping="yes">collapse.gif</xsl:text>
												</xsl:attribute>
											</img>
										</xsl:when>
										<xsl:otherwise>
											<img onclick="menuLeftImageClick(this, '')">
												<xsl:attribute name="src">
													<xsl:value-of select="$ImageDir" />
													<xsl:text disable-output-escaping="yes">expand.gif</xsl:text>
												</xsl:attribute>
											</img>
										</xsl:otherwise>
									</xsl:choose>
								</td>
								<td onclick='menuClicked(this)' class="topLevelMenu">
									<xsl:attribute name="id">pmwc<xsl:value-of select="child::AppMenuId" /></xsl:attribute>
									<xsl:value-of select="MenuName" />
								</td>
							</tr>
							<tr>
								<td></td>
								<td>
									<!--Nested Elements-->
									<table>
										<!--If this menu is not selected parent then make table display = none-->
										<xsl:if test="child::SelectedParent=0">
											<xsl:attribute name="style">display:none</xsl:attribute>
										</xsl:if>
										<xsl:attribute name="id">TOPTABLE<xsl:value-of select="child::AppMenuId" /></xsl:attribute>
										<xsl:for-each select="//Menu[ParentMenuId = $vParentMenuId]">
											<xsl:call-template name="DynNestedMenu">
												<xsl:with-param name="counter" select="1" />
											</xsl:call-template>
										</xsl:for-each>
									</table>
								</td>
							</tr>
						</xsl:when>
						<xsl:otherwise>
							<tr>
								<td class="menuImage">
									<img>
										<xsl:attribute name="src">
											<xsl:value-of select="$ImageDir" />
											<xsl:text disable-output-escaping="yes">dot.gif</xsl:text>
										</xsl:attribute>
									</img>
								</td>
								<td onclick='menuClicked(this)' class="menuSelected1">
									<xsl:attribute name="id">pmnc<xsl:value-of select="child::AppMenuId" /></xsl:attribute>
									<xsl:value-of select="MenuName" />
								</td>
							</tr>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</table>
		</div>
	</xsl:template>
	<xsl:template name="DynNestedMenu">
		<xsl:param name="counter" />
		<xsl:variable name="NestedMenuId" select="child::AppMenuId" />
		<xsl:choose>
			<xsl:when test="child::SelectedParent=1">
				<!-- Paint Child Menus only if the selected Menu is this or one of its child -->
				<xsl:choose>
					<xsl:when test="((count(//Menu[ParentMenuId = $NestedMenuId]) > 0) and ($SelectedId/AppMenuId != $NestedMenuId))">
						<tr>
							<td class="menuImage">
								<img onclick="menuLeftImageClick(this, '')">
									<xsl:attribute name="src">
										<xsl:value-of select="$ImageDir" />
										<xsl:text disable-output-escaping="yes">collapse.gif</xsl:text>
									</xsl:attribute>
								</img>
							</td>
							<td onclick='menuClicked(this)' class="menuItem2">
								<xsl:attribute name="id">cm<xsl:value-of select="counter" />wc<xsl:value-of select="child::AppMenuId" /></xsl:attribute>
								<a>
									<xsl:attribute name="href">
										<xsl:value-of select="ResolvedUrl" />
									</xsl:attribute>
									<xsl:value-of select="MenuName" />
								</a>
							</td>
						</tr>
						<tr>
							<td></td>
							<td>
								<!--Sub Nested Elements-->
								<table>
									<!--If this menu is not selected parent then make table display = none-->
									<xsl:if test="child::SelectedParent=0">
										<xsl:attribute name="style">display:none</xsl:attribute>
									</xsl:if>
									<xsl:for-each select="//Menu[ParentMenuId = $NestedMenuId]">
										<xsl:call-template name="DynNestedMenu">
											<xsl:with-param name="counter" select="$counter + 1" />
										</xsl:call-template>
									</xsl:for-each>
								</table>
							</td>
						</tr>
					</xsl:when>
					<xsl:otherwise>
						<tr>
							<td class="menuImage">
								<img>
									<xsl:attribute name="src">
										<xsl:value-of select="$ImageDir" />
										<xsl:text disable-output-escaping="yes">dot.gif</xsl:text>
									</xsl:attribute>
								</img>
							</td>
							<td onclick='menuClicked(this)' class="menuSelected2">
								<xsl:attribute name="id">cm<xsl:value-of select="counter" />nc<xsl:value-of select="child::AppMenuId" /></xsl:attribute>
								<a>
									<xsl:attribute name="href">
										<xsl:value-of select="ResolvedUrl" />
									</xsl:attribute>
									<xsl:value-of select="MenuName" />
								</a>
							</td>
						</tr>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<tr>
					<td class="menuImage">
						<img>
							<xsl:attribute name="src">
								<xsl:value-of select="$ImageDir" />
								<xsl:text disable-output-escaping="yes">dot.gif</xsl:text>
							</xsl:attribute>
						</img>
					</td>
					<xsl:choose>
						<xsl:when test="$SelectedId/AppMenuId = child::AppMenuId">
							<td onclick='menuClicked(this)' class="menuSelected2">
								<xsl:attribute name="id">cm<xsl:value-of select="counter" />nc<xsl:value-of select="child::AppMenuId" /></xsl:attribute>
								<a>
									<xsl:attribute name="href">
										<xsl:value-of select="ResolvedUrl" />
									</xsl:attribute>
									<xsl:value-of select="MenuName" />
								</a>
							</td>
						</xsl:when>
						<xsl:otherwise>
							<td onclick='menuClicked(this)' class="menuItem2">
								<xsl:attribute name="id">cm<xsl:value-of select="counter" />nc<xsl:value-of select="child::AppMenuId" /></xsl:attribute>
								<a>
									<xsl:attribute name="href">
										<xsl:value-of select="ResolvedUrl" />
									</xsl:attribute>
									<xsl:value-of select="MenuName" />
								</a>
							</td>
						</xsl:otherwise>
					</xsl:choose>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>