<%@ Page Title="" Language="C#" MasterPageFile="~/JsManager/Manager_Master.master" AutoEventWireup="true" CodeFile="Announce.aspx.cs" Inherits="JsManager_Announce" %>

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
                <h3>公告</h3>
            </div>
        </div>
        <div id="AnnounceList">
            <table>
                <tr>
                    <td>
                        <asp:Repeater ID="AnnounceRepeater" runat="server" OnItemCommand="AnnounceRepeater_ItemCommand" >
                            
                            <ItemTemplate>
                                
                                <tr style="width:100%;height:20px">
                                    <h2>
                                        <asp:Label ID="AnnouceTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"notTitle" %>' CssClass="notTitle"></asp:Label>
                                    </h2>
                                </tr>
                                <tr style="width:100%;height:80px">
                                    <h3>
                                        <asp:Label ID="AnnounceContent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"notContent" %>' CssClass="notContent"></asp:Label>
                                    </h3>
                                </tr>
                                <tr style="width:100%;height:15px">
                                    <h5>
                                        <asp:Label ID="Announcer" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobskyerID" %>' CssClass="right-bottom"></asp:Label>
                                    </h5>
                                    <h5>
                                        <asp:Label ID="AnnounceTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"notTime" %>' CssClass="right-bottom"></asp:Label>
                                    </h5>
                                </tr>
                            </ItemTemplate>
                           
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float:right">
            <asp:ImageButton ID="AnnounceWrite" runat="server" ImageUrl="~/Image/Button/AnnounceWrite-02.png" OnClick="ImgbtnAnnounce_Click" />
        </div>
    </div>
</asp:Content>

