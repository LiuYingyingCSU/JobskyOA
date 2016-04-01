<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Change.aspx.cs" Inherits="Change_Change" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
    <a href="../JsCommon/JsCommonMain.aspx" title="点击返回"><span class="back" >>>></span></a>
    <div class="chg_left">
        <img src="<%=imgUrl %>" style="width:300px;height:300px;margin-bottom:20px;margin-top:20px;" />
        <asp:FileUpload ID="imageUp" runat="server" CssClass="textBox" />
        <asp:Button ID="btnImgUp" runat="server" Text="更换" OnClick="btnImgUp_Click" CssClass="textBox" />
        <br />
        <asp:Label ID="lblFail" runat="server" Text="lblFail" ClientIDMode="Predictable" Visible="False" ForeColor="Red"></asp:Label>
    </div>
    <div class="chg_left">
        <br />
        <br />
        <br />
        <br />
        <span style="font-size:20px;">请输入原密码：</span><asp:TextBox ID="tbPwd" runat="server" BorderColor="#00CCFF" BorderStyle="Ridge" Height="30px" Width="150px" CssClass="textBox"></asp:TextBox>
        <br />
        <br />
        <span style="font-size:20px;">请输入新密码：</span><asp:TextBox ID="tbNewPwd" runat="server" BorderColor="#00CCFF" BorderStyle="Groove" Height="30px" Width="150px" CssClass="textBox"></asp:TextBox>
        <br />
        <br />
        <span style="font-size:20px;">请确认新密码：</span><asp:TextBox ID="tbConfirmPwd" runat="server" BorderColor="#00CCFF" BorderStyle="Inset" Height="30px" Width="150px" CssClass="textBox"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" BackColor="White" BorderColor="#00CCFF" BorderStyle="Solid" CssClass="textBox" Font-Italic="False" ForeColor="Black" Text="提交修改" OnClick="btnSubmit_Click" />
        <br />
        <br />
        <br />
        <asp:Label ID="lblError" runat="server" Text="Label" Visible="False"></asp:Label>

    </div>
</asp:Content>

