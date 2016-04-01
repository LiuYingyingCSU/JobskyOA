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
            <div >  
                <asp:ImageButton ID="Manage" runat="server" ImageUrl="~/Image/Login/Manage.PNG" OnClick="Manage_Click" />
                <asp:ImageButton ID="Common" runat="server" ImageUrl="~/Image/Login/Common.PNG" OnClick="Common_Click" CssClass="bordr" />
                <div style="font-family:SimHei;white-space:pre;"> 管理员                    员工</div>
           </div>
                       <table style="width:100%" >
                                <tr class="userid">
                                    <td class="auto-style1">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">学号:</asp:Label>
                                    </td>
                                    <td class="auto-style3">
                                        <asp:TextBox ID="UserName" runat="server" style="margin-left: 7px;border:none;" Width="220px" Height="25px"></asp:TextBox>
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
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" style="margin-left: 7px;border:none;" Width="220px" Height="25px"></asp:TextBox>
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
                                        <asp:Label ID="lblFailText" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                
                            </table>
                    <div class="login_top">
                        <asp:ImageButton ID="btnLogin" runat="server" Height="30px" ImageUrl="~/Image/Login/SignIn-01.png" OnClick="btnLogin_Click" Width="280px" BorderStyle="None" />
&nbsp;</div>
            </div>
    </form>
</body>
</html>
