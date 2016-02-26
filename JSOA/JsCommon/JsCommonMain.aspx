<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JsCommonMain.aspx.cs" Inherits="JsCommon_JsCommonMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
   <div class="middle">
    <div class="JCM_left">
        <div class="JCM_pro"><asp:ImageButton ID="ImgbtnProfile" runat="server" Height="226px" Width="220px"/></div>
        <div class="JCM_info">姓名：<asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></div>
        <div class="JCM_info">院系：<asp:Label ID="lblAcademy" runat="server" Text="Academy"></asp:Label></div>
        <div class="JCM_info">组别：<asp:Label ID="lblPosition" runat="server" Text="Position"></asp:Label></div>
    </div>
    <div class="JCM_right">
        &nbsp;<asp:Label ID="lblCount" runat="server" Text="个人积分"></asp:Label>
&nbsp;<asp:Label ID="lblRank" runat="server" Text="积分排名"></asp:Label>
        <br />
        所有人员排名</div>
</div>
</asp:Content>


