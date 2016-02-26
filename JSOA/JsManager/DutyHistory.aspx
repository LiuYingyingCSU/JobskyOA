<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DutyHistory.aspx.cs" Inherits="admin_History" %>

<%--<%@ Register src="../../DutyHistoryRepeater.ascx" tagname="DutyHistoryRepeater" tagprefix="uc1" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
       .Repeater1
       {
           
       }
       
    </style>
    <script language="javascript" type="text/javascript">
    function judgeNumber(code)
    {
        if(code>=48&&code<=57)
        {
        return true;
        }
        else
        {
        return false;
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Repeater1">
    <table><tr><td>
        <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <table width="500px">
                <tr>
                    <td>学号</td><td>值班人</td><td>上班时间</td><td>下班时间</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"JOBSKYER_ID") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SIGNNAME") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabUpTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MY_SIGN_IN_TIME_WEEK_NOW") %>'></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabOutTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MY_SIGN_OUT_TIME_WEEK_NOW") %>'></asp:Label>
            </td>                      
        </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
        </asp:Repeater>  
        </td>
        <td>
        <asp:Repeater ID="Repeater2" runat="server" onitemcommand="Repeater2_ItemCommand1">
         <HeaderTemplate>
            <table width="200px">
                <tr>
                    <td>被代班人</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
        <tr>
            <td>
                <asp:Label ID="LabReplaceName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"REPLACENAME") %>'></asp:Label>
            </td>
            <td> 
                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript:return confirm('确定删除此条记录吗？');" Text="删除" CommandArgument='<%#Eval("SIGN_RECORD_ID") %>' CommandName="delete" BorderWidth="0"></asp:LinkButton>
            </td>                              
        </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
        </asp:Repeater>
        </td></tr></table>
         </div>
        <asp:Label ID="Label2" runat="server" Text="请输入要查找值班人的学号："></asp:Label><asp:TextBox ID="SearchPeople" runat="server" onkeypress="return judgeNumber(event.keyCode);"></asp:TextBox>
        <asp:Button ID="Search" runat="server" Text="查找" onclick="Search_Click" />
        <asp:Button ID="SearchAll" runat="server" Text="查看所有" onclick="SearchAll_Click" /><br />
    <asp:Label ID="Label1" runat="server" Text="请输入要删除值班人的学号："></asp:Label><asp:TextBox ID="AimDelete"
        runat="server" onkeypress="return judgeNumber(event.keyCode);"></asp:TextBox><asp:Button ID="AimDelete_btn" runat="server" 
        Text="删除" OnClientClick="javascript:return confirm('确定删除该值班人所有记录吗？');" onclick="AimDelete_btn_Click" />
   <%-- <uc1:DutyHistoryRepeater ID="DutyHistoryRepeater1" runat="server" />--%>
    </form>
</body>
</html>