<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JsManagerMain.aspx.cs" Inherits="JsManager_JsManagerMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
    <div>
        <asp:ImageButton ID="Duty_Count" runat="server" OnClick="Duty_Count_Click" />
    </div>
</asp:Content>

