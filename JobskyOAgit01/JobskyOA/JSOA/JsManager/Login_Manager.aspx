<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_Manager.aspx.cs" Inherits="JSOA_JsManager_Login_Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Jobsky-OA-管理员登录</title>
    <link href="../CSS/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="login">
        <asp:Login ID="Login_Manager" runat="server" OnAuthenticate="Login_Authenticate_Manager">
            <LayoutTemplate>
                <table style="width:100%">
                  <tr>
                    <td class="login_Image">
                        <asp:ImageButton ID="ButtonManager" runat="server" ImageUrl="~/Image/login/Login_Manager.jpg" OnClick="ClickButton_Manager" />
                    </td>
                      <td class="login_Image">
                        <asp:ImageButton ID="ButtonEmployee" runat="server" ImageUrl="~/Image/login/Login_Jobskyer.jpg" OnClick="ClickButton_Employee" />
                    </td>
                  </tr>
                    <tr class="userid">
                        <td class="auto-style1">
                           <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">学号:</asp:Label>
                        </td>
                        <td class="auto-style3">
                           <asp:TextBox ID="UserName" runat="server" style="margin-left: 7px;border:none;" Width="181px" Height="25px"></asp:TextBox>
                        </td>
                        <asp:RegularExpressionValidator ID="UserNameRequired" runat="server" ForeColor="Red" Text="用户名不能为空！" ControlToValidate="UserName">*</asp:RegularExpressionValidator>
                    </tr>
                    <tr class="userid">
                         <td class="auto-style1">
                             <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码:</asp:Label>
                         </td>
                         <td class="auto-style3"  >
                              <asp:TextBox ID="Password" runat="server" TextMode="Password" style="margin-left: 7px;border:none;" Width="181px" Height="25px"></asp:TextBox>
                         </td>
                        <asp:RegularExpressionValidator ID="PasswordRequired" runat="server" ForeColor="Red" Text="密码不能为空！" ControlToValidate="Password">*</asp:RegularExpressionValidator>
                    </tr>
                    <td colspan="2">
                        <asp:CheckBox ID="RememberMe" runat="server" Text="记住密码" />
                    </td>
                    <div style="width:72%;margin-top:20px;border-bottom-left-radius:10px;border-bottom-right-radius:10px;">    
                      <asp:ImageButton ID="btnLogin" runat="server" CommandName ="Login" OnClick="btnLogin_Click1" ValidationGroup="Login1" />
                  </div>
                </table>       
            </LayoutTemplate>       
        </asp:Login>
            
    </div>
    </form>
</body>
</html>
