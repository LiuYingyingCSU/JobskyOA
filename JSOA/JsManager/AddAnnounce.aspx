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
        <div>
            <asp:Label ID="AnnounceTitleLabel" runat="server" Text="请输入标题："></asp:Label>
            <asp:TextBox ID="AddAnnounceTitle" runat="server" OnTextChanged="AddAnnounceTitle_TextChanged"></asp:TextBox><br />
            <asp:Label ID="AnnounceContentLabel" runat="server" Text="请输入内容："></asp:Label>
            <br />
            <div id="AddContent"> 
                <table>
                    <asp:TextBox ID="AddContentText" runat="server" TextMode="MultiLine" Width="800px" Height="330px" BackColor="#ccccff" OnTextChanged="AddContentText_TextChanged"></asp:TextBox>
                    
                </table>
                <div style="float:right">
                   <asp:ImageButton ID="SubmitAnnounce" runat="server" ImageUrl="~/Image/JsCommonMain/Notice-01.png" OnClientClick="javascript:return confirm('确定添加？');" OnClick="SubmitAnnounce_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

