<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
    xmlns:iis="http://schemas.microsoft.com/wix/IIsExtension"
    xmlns:util="http://schemas.microsoft.com/wix/UtilExtension"
    exclude-result-prefixes="wix" >
  <xsl:output method="xml" indent="yes"/>
  <xsl:strip-space elements="*"/>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="/*">
    <xsl:copy>
      <xsl:copy-of select="document('')/xsl:stylesheet/namespace::*[not((local-name() = 'xsl') or (local-name() = 'wix'))]"/>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="/wix:Wix/wix:Fragment/wix:DirectoryRef/wix:Component[1]">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
      <iis:WebSite  Id="{../@Id}"
                    Description="{../@Id}"
                    Directory="{../@Id}">
        <iis:WebAddress Id="{../@Id}" Port="{concat('[',../@Id,'.Port',']')}"/>
        <iis:WebApplication Id="{../@Id}"
                            Name="{../@Id}"
                            WebAppPool="AppPool"/>
        <iis:WebDirProperties Id="{../@Id}"
                              AnonymousAccess ="yes"
                              Read="yes"
                              Execute="yes"
                              Script="yes"/>
      </iis:WebSite>
    </xsl:copy>
  </xsl:template>


  <xsl:template match="/wix:Wix/wix:Fragment/wix:DirectoryRef/wix:Component[child::wix:File[contains(@Source,'Web.config')]]">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
      <util:XmlConfig Id="{concat(child::wix:File[contains(@Source,'Web.config')]/@Id, '_cfg1')}"
                      Action="create"
                      File="{concat('[#',child::wix:File[contains(@Source,'Web.config')]/@Id,']')}"
                      ElementPath="/configuration"
                      VerifyPath="/configuration/appSettings"
                      Name="appSettings"
                      Node="element"
                      On="install"
                      Sequence="1"/>
      <util:XmlConfig Id="{concat(child::wix:File[contains(@Source,'Web.config')]/@Id, '_cfg2')}"
                      Action="delete"
                      File="{concat('[#',child::wix:File[contains(@Source,'Web.config')]/@Id,']')}"
                      ElementPath="/configuration/appSettings"
                      VerifyPath="/configuration/appSettings/add[\[]@key='XERPConfigPath'[\]]"
                      Node="element"
                      On="install"
                      Sequence="2"/>
      <util:XmlConfig Id="{concat(child::wix:File[contains(@Source,'Web.config')]/@Id, '_cfg3')}"
                      Action="create"
                      File="{concat('[#',child::wix:File[contains(@Source,'Web.config')]/@Id,']')}"
                      ElementPath="/configuration/appSettings"
                      VerifyPath="/configuration/appSettings/add[\[]@key='XERPConfigPath'[\]]"
                      Name="add"
                      Node="element"
                      On="install"
                      Sequence="3"/>
      <util:XmlConfig Id="{concat(child::wix:File[contains(@Source,'Web.config')]/@Id, '_cfg4')}"
                      File="{concat('[#',child::wix:File[contains(@Source,'Web.config')]/@Id,']')}"
                      ElementId="{concat(child::wix:File[contains(@Source,'Web.config')]/@Id, '_cfg3')}"
                      Name="key"
                      Value="XERPConfigPath"
                      Sequence="4"/>
      <util:XmlConfig Id="{concat(child::wix:File[contains(@Source,'Web.config')]/@Id, '_cfg5')}"
                      File="{concat('[#',child::wix:File[contains(@Source,'Web.config')]/@Id,']')}"
                      ElementId="{concat(child::wix:File[contains(@Source,'Web.config')]/@Id, '_cfg3')}"
                      Name="value"
                      Value="[INSTALLFOLDER]Config\XERPServerConfig.xml"
                      Sequence="5"/>
     
    </xsl:copy>
  </xsl:template>
  
      
</xsl:stylesheet>
