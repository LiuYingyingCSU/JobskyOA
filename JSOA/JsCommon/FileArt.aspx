<%@ Page Title="" Language="C#" MasterPageFile="~/JsCommon/FileDownSubMaster.master" AutoEventWireup="true" CodeFile="FileArt.aspx.cs" Inherits="JsCommon_FileArt" %>
<%@ MasterType VirtualPath="~/JsCommon/FileDownSubMaster.master" %>
<%@ Register TagName="fileRepeater" TagPrefix="uc1"  Src="~/JsCommon/fileRepeater.ascx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="SubContent_ContentPlaceHolder" Runat="Server">
    <uc1:fileRepeater ID="fileRpt" runat="server" />
    </asp:Content>

