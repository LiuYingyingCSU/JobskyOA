<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JsCommonMain.aspx.cs" Inherits="JsCommon_JsCommonMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
    <div style="float:left">
        <asp:ImageButton ID="ImgbtnProfile" runat="server"/>
        <br />
        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
        <br />
        <asp:Label ID="lblAcademy" runat="server" Text="Academy"></asp:Label>
        <br />
        <asp:Label ID="lblPosition" runat="server" Text="Position"></asp:Label>
    </div>
    <div style="float:left">
        &nbsp;<asp:Label ID="lblCount" runat="server" Text="个人积分"></asp:Label>
&nbsp;<asp:Label ID="lblRank" runat="server" Text="积分排名"></asp:Label>
        <br />
        所有人员排名</div>
</asp:Content>

