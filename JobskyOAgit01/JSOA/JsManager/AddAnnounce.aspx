<%@ Page Title="" Language="C#" MasterPageFile="~/JsManager/Manager_Master.master" AutoEventWireup="true" CodeFile="AddAnnounce.aspx.cs" Inherits="JsManager_AddAnnounce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Manager_ContentPlaceHolder" Runat="Server">
    <link href="../CSS/Announce.css" rel="stylesheet" />
    <div style="height: 503px">
        <div class="announce-label">
            <asp:ImageButton ID="ImgbtnAnnounce" runat="server" ImageUrl="~/Image/Button/Activity-01.png" OnClick="ImgbtnAnnounce_Click"/>
        </div>
        <div class="announce-bar">
            <div class="announce-word">
                <h3>发布公告</h3>
            </div>
        </div>
    </div>

</asp:Content>

