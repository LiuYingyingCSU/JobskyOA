<%@ Page Title="" Language="C#" MasterPageFile="~/JsInfo/JsInfo_Master.master" AutoEventWireup="true" CodeFile="ManagerDutyTime.aspx.cs" Inherits="JsManager_TimeTable_Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="JsInfo_ContentPlaceHolder" Runat="Server">
    <div id="set_dutyTime">
        <asp:DropDownList ID="chooseName" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="chooseWeekday" runat="server">
            <asp:ListItem Value="2016-02-01">周一</asp:ListItem>
            <asp:ListItem Value="2016-02-02">周二</asp:ListItem>
            <asp:ListItem Value="2016-02-03">周三</asp:ListItem>
            <asp:ListItem Value="2016-02-04">周四</asp:ListItem>
            <asp:ListItem Value="2016-02-05">周五</asp:ListItem>
            <asp:ListItem Value="2016-02-06">周六</asp:ListItem>
            <asp:ListItem Value="2016-02-07">周日</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="chooseSignInTime" runat="server">
            <asp:ListItem Value=" 08:30:00"> 08:30:00</asp:ListItem>
            <asp:ListItem Value=" 10:30:00"> 10:30:00</asp:ListItem>
            <asp:ListItem Value=" 13:00:00"> 13:00:00</asp:ListItem>
            <asp:ListItem Value=" 15:30:00"> 15:30:00</asp:ListItem>
            <asp:ListItem Value=" 18:30:00"> 18:30:00</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="chooseSignOutTime" runat="server">
            <asp:ListItem Value=" 10:30:00"> 10:30:00</asp:ListItem>
            <asp:ListItem Value=" 13:00:00"> 13:00:00</asp:ListItem>
            <asp:ListItem Value=" 15:30:00"> 15:30:00</asp:ListItem>
            <asp:ListItem Value=" 18:30:00"> 18:30:00</asp:ListItem>
            <asp:ListItem Value=" 20:30:00"> 20:30:00</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="confirm" Text="确定添加" runat="server" OnClick="confirm_Click" />
        <asp:Label ID="Sign_Result" runat="server"></asp:Label>
    </div>

    <div id="TimeList">
        <asp:Repeater ID="TimeList_Manager" runat="server" OnItemCommand="TimeList_Manager_ItemCommand">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:200px">姓名</th>
                        <th style="width:200px">上班时间</th>
                        <th style="width:200px">下班时间</th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width:200px">
                            <asp:Label ID="name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="Sign_In" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"dutyInTime") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="Sign_Out" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"dutyOutTime") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Button ID="Delect_button" runat="server" CommandArgument='<%#Eval("jobskyerID") %>' CommandName="delect" Text="删除" Visible="true" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Button ID="ToDutyTable" runat ="server" Text="值班表" OnClick="ToDutyTable_Click" />

</asp:Content>

