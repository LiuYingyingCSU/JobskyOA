<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="JsCommon_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../CSS/Login.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            
        <asp:Login ID="Login1" runat="server">
            <LayoutTemplate>
                <table  style="border-collapse:collapse;">
                    <tr>
                        <td  >
                            <table >
                                <div class="login_top">
                                    <p>登录</p>
                                </div>
                                <tr class="userid">
                                    <td >
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">学号:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="必须填写“用户名”。" ToolTip="必须填写“用户名”。" ValidationGroup="Login1">用户名不能为空</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr class="userid">
                                    <td  >
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码:</asp:Label>
                                    </td>
                                    <td  >
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="必须填写“密码”。" ToolTip="必须填写“密码”。" ValidationGroup="Login1">密码不能为空</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" >
                          <asp:CheckBox ID="RememberMe" runat="server" Text="下次记住我。" />
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="2" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" Text="用户名或密码错误"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                       <td> 
                                           <asp:ImageButton ID="btnLogin" runat="server" CommandName ="Login" OnClick="btnLogin_Click1" ValidationGroup="Login1" />
                                       </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
            </div>
    </form>
</body>
</html>
