<%@ Page Title="" Language="C#" MasterPageFile="~/JsManager/Manager_Master.master" AutoEventWireup="true" CodeFile="DutyRecord.aspx.cs" Inherits="JsManager_DutyRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Manager_ContentPlaceHolder" Runat="Server">
    <div id="DutyRecord">
        <table>
            <tr>
                <td>
        <asp:Repeater ID="DutyRecord_repeater" runat ="server" OnItemCommand="DutyRecord_repeater_ItemCommand">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:200px">学号</th>
                        <th style="width:200px">值班人</th>
                        <th style="width:200px">签到时间</th>
                        <th style="width:200px">签退时间</th>
                    </tr>                
            </HeaderTemplate>
            <ItemTemplate>
                
                    <tr>
                        <td style="width:200px">
                            <asp:Label ID="Dutyer_ID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobskyerID") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="duty_name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="duty_in" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MyDutyInTime") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="duty_out" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MyDutyOutTime") %>'></asp:Label>
                        </td>
                    </tr>             
            </ItemTemplate>
            <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
                    </td>
                <td>
                    <asp:Repeater ID="Duty_state" runat="server" OnItemCommand="Duty_state_ItemCommand">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th style="width:200px">签到状况</th>
                                    <th style="width:200px">签退状况</th>
                                </tr>                         
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width:100px">
                                    <asp:Label ID="in_state" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"flag1") %>'></asp:Label>
                                </td>
                                <td style="width:100px">
                                    <asp:Label ID="out_state" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"flag2")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></table></FooterTemplate>
                    </asp:Repeater>
                </td>  
                <asp:Repeater ID="Replace_Name" runat="server" OnItemCommand="Replace_Name_ItemCommand">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <th style="width:200px">被代班人</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="replace_name" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"toName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="Delete" runat="server" CommandArgument='<%#Eval("jobskyerID") %>' CommandName="delete" Text="删除" Visible="true" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                </asp:Repeater>                
            </tr>
        </table>
    </div>
    <div>
        <asp:DropDownList ID="name_dropList" runat="server" OnSelectedIndexChanged="name_dropList_SelectedIndexChanged" >
           
        </asp:DropDownList>
    </div>
    <div>
        <asp:Button ID="Search" runat="server" Text="检索" OnClick="Search_Click" />
    </div>
    <br /><br />
    <div>
        <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click"/>
    </div>
    <br />
    <div>
        <asp:Button ID="toDutyRecord" runat="server" Text="查看值班表" OnClick="toDutyRecord_Click" />&nbsp&nbsp
        <asp:Button ID="toManagerDutyTime" runat="server" Text="设置值班表" OnClick="toManagerDutyTime_Click" />
    </div>                                              
</asp:Content>