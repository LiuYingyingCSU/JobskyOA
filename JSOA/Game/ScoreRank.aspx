<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ScoreRank.aspx.cs" Inherits="Game_ScoreRank01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
    <h1>游戏排行</h1>
    <asp:DropDownList ID="ScoreRankList" runat ="server" BackColor="#00ccff" OnSelectedIndexChanged="ScoreRankList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:Repeater ID="ScoreRank" runat="server" Visible="false">
        <HeaderTemplate>
            <table style="margin:10px; padding:10px; border:1px solid seagreen;" border="1">
                <tr>
                    <th style="width:200px">
                        <asp:Label ID="label01" runat="server" Text="名字"></asp:Label>
                    </th>
                    <th style="width:200px">
                        <asp:Label ID="label02" runat="server" Text="学号"></asp:Label>
                    </th>
                    <th style="width:200px">
                        <asp:Label ID="label03" runat="server" Text="得分"></asp:Label>
                    </th>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table style="margin:10px; padding:10px; border:1px solid seagreen;" border="1">
                <tr>
                    <th style="width:200px">
                        <asp:Label ID="label04" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName")%>'></asp:Label>
                    </th>
                    <th style="width:200px">
                        <asp:Label ID="label05" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobskyerID")%>'></asp:Label>
                    </th>
                    <th style="width:200px">
                        <asp:Label ID="label06" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"gamScore") %>'></asp:Label>
                    </th>
                </tr>
            </table> 
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

