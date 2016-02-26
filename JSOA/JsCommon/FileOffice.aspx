<%@ Page Title="" Language="C#" MasterPageFile="~/JsCommon/FileDownSubMaster.master" AutoEventWireup="true" CodeFile="FileOffice.aspx.cs" Inherits="JsCommon_FileOffice" %>

<%@ Register TagName="fileRepeater" TagPrefix="uc1"  Src="~/JsCommon/fileRepeater.ascx"%>
<asp:Content ID="Content1" ContentPlaceHolderID="SubContent_ContentPlaceHolder" Runat="Server">
   <uc1:fileRepeater ID="fileRpt" runat="server" />
</asp:Content>

