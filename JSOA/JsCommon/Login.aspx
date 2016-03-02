<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="JsCommon_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../CSS/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            <div class="login_top"> 登录
           </div>
                       <table style="width:100%" >
                                <tr class="userid">
                                    <td class="auto-style1">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">学号:</asp:Label>
                                    </td>
                                    <td class="auto-style3">
                                        <asp:TextBox ID="UserName" runat="server" style="margin-left: 7px;border:none;" Width="181px" Height="25px"></asp:TextBox>
                                    </td>
                                    <td>    
                                        <asp:Label ID="lblUser" runat="server" Text="用户名不能为空"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="userid">
                                    <td class="auto-style1">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密码:</asp:Label>
                                    </td>
                                    <td class="auto-style3"  >
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" style="margin-left: 7px;border:none;" Width="181px" Height="25px"></asp:TextBox>
                                    </td>
                                    <td>     
                                          <asp:Label ID="lblPwd" runat="server" Text="密码不能为空"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" >
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="下次记住我。" />
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="2" style="color:Red;">
                                        <asp:Label ID="lblFailText" runat="server" Text="用户名或密码错误" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                
                            </table>
                    <div style="width:72%;margin-top:20px;border-bottom-left-radius:10px;border-bottom-right-radius:10px;">    
                      <asp:ImageButton ID="btnLogin" runat="server" CommandName ="Login" OnClick="btnLogin_Click1" ValidationGroup="Login1" />
                  </div>
            </div>
    </form>
</body>
</html>
