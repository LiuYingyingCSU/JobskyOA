<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="JsCommon_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        学号：<asp:TextBox ID="txtJobskyID" runat="server"></asp:TextBox>
    
    </div>
        密码：<asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnLogin" runat="server" Text="登录" Width="146px" OnClick="btnLogin_Click" />
    </form>
</body>
</html>
