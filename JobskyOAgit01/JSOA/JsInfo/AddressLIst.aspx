<%@ Page Title="" Language="C#" MasterPageFile="~/JsInfo/JsInfo_Master.master" AutoEventWireup="true" CodeFile="AddressLIst.aspx.cs" Inherits="JsInfo_AddressLIst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="JsInfo_ContentPlaceHolder" Runat="Server">
    <div>
        <asp:TextBox ID="Search_jobskyer_Box" runat="server"></asp:TextBox>
    </div>
    <div>
        <h3>办公室</h3>
    <asp:Repeater ID="AddressList_Repeater_Office" runat="server">
        <HeaderTemplate>
            <table>
                 <tr>
                     <th style="width:200px">
                         <asp:Label ID="label10" runat="server" Text="姓名"></asp:Label>
                     </th>
                     <th style="width:200px">
                         <asp:Label ID="Label11" runat="server" Text="联系方式"></asp:Label>
                     </th>
                 </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table>
                <tr>
                    <td style="width:200px;align-content:center">
                        <asp:Label ID="label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName") %>' ></asp:Label>
                    </td>
                    <td style="width:200px;align-content:center">
                        <asp:Label ID="label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobPhone") %>'></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    </div>
    <br /><br />
    <div>
        <h3>程序组</h3>
        <asp:Repeater ID="AddressList_Repeater_SoftWare" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:200px">
                            <asp:Label ID="label14" runat="server" Text="姓名"></asp:Label>
                        </th>
                        <th style="width:200px">
                            <asp:Label ID="label15" runat="server" Text="联系方式"></asp:Label>
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width:200px">
                            <asp:Label ID="label16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobPhone") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br /><br />
    <div>
        <h3>网络组</h3>
        <asp:Repeater ID="AddressList_Repeater_Internet" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:200px">
                            <asp:Label ID="label18" runat="server" Text="姓名"></asp:Label>
                        </th>
                        <th style="width:200px">
                            <asp:Label ID="label19" runat="server" Text="联系方式"></asp:Label>
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width:200px">
                            <asp:Label ID="label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobPhone") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br /><br />
    <div>
        <h3>美工组</h3>
        <asp:Repeater ID="AddressList_Repeater_Art" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:200px" >
                            <asp:Label ID="label22" runat="server" Text="姓名"></asp:Label>
                        </th>
                        <th style="width:200px">
                            <asp:Label ID="label23" runat="server" Text="联系方式"></asp:Label>
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width:200px">
                            <asp:Label ID="label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName") %>'></asp:Label>
                        </td>
                        <td style="width:200px">
                            <asp:Label ID="label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobPhone") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <br /><br />
    <div>
        <h3>记者团</h3>
        <asp:Repeater ID="AddressList_Repeater_Reporter" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th style="width:200px">
                            <asp:Label ID="label26" runat="server" Text="姓名"></asp:Label>
                        </th>
                        <th style="width:200px">
                            <asp:Label ID="label27" runat="server" Text="联系方式"></asp:Label>
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width:200px">
                            <asp:Label ID="label28" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobName") %>'></asp:Label>
                        </td>
                        <td style="width:200px"> 
                            <asp:Label ID="label29" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"jobPhone") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

