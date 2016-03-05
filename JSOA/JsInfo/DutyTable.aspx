<%@ Page Title="" Language="C#" MasterPageFile="~/JsInfo/JsInfo_Master.master" AutoEventWireup="true" CodeFile="DutyTable.aspx.cs" Inherits="JsInfo_DutyTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="JsInfo_ContentPlaceHolder" Runat="Server">
   <div>
     <table>
        <tr>
            <td>星期</td>
            <td>星期一</td>
            <td>星期二</td>
            <td>星期三</td>
            <td>星期四</td>
            <td>星期五</td>
            <td>星期六</td>
            <td>星期天</td>
        </tr>
        <tr>
            <td>第一班(8:30-10:30)</td>
            <td>
                <asp:Label ID="Duty_1_1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_1_2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_1_3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_1_4" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_1_5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_1_6" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_1_7" runat="server"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td>第二班(10:30-13:00)</td>
            <td>
                <asp:Label ID="Duty_2_1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_2_2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_2_3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_2_4" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_2_5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_2_6" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_2_7" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>第三班(13:00-15:30)</td>
            <td>
                <asp:Label ID="Duty_3_1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_3_2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_3_3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_3_4" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_3_5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_3_6" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_3_7" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>第四班(15:30-18:30)</td>
            <td>
                <asp:Label ID="Duty_4_1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_4_2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_4_3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_4_4" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_4_5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_4_6" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_4_7" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>晚班(18:30-20:30)</td>
            <td>
                <asp:Label ID="Duty_5_1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_5_2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_5_3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_5_4" runat="server"></asp:Label>

            </td>
            <td>
                <asp:Label ID="Duty_5_5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_5_6" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Duty_5_7" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
   </div>
</asp:Content>

