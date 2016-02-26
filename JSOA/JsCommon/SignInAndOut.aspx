<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SignInAndOut.aspx.cs" Inherits="JsCommon_SignInAndOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
    <div>
        <div class="sign">
     <asp:ImageButton ID="ImgbtnSignIn" runat="server" ImageUrl="~/Image/Sign/SignIn.PNG" OnClick="ImgbtnSignIn_Click" />
     <asp:DropDownList ID="DLSignInTo" runat="server">
        <asp:ListItem Selected="True" Value ="0">请选择被代班人：</asp:ListItem>
     </asp:DropDownList>
            <br />
        <asp:Label ID="lblMessage" runat="server" Text="提示信息"></asp:Label>
       </div>
        <div class="tableHeader">
            <p class="tableHead">
                值班人</p>
            <p class="tableHead">
                签到</p>
            <p class="tableHead">
                签退</p>
            <p class="tableHead">
                被代班人</p>
        </div>
        <div class="tableBody">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <p class="tableContent">
                        <asp:Label ID="lbljobName" runat="server" Text='<%#GetJobName(Eval("jobskyerID").ToString())%>'></asp:Label>
                    </p>
                    <p class="tableContent">
                        <asp:Label ID="lblSignIn" runat="server" Text='<%# Eval("dutyInTime")%>'></asp:Label>
                    </p>
                    <p class="tableContent">
                        <asp:Label ID="lblSignOut" runat="server" Text='<%#Eval("dutyOutTime")%>'></asp:Label>
                    </p>
                    <p class="tableContent">
                        <asp:Label ID="lblSignInto" runat="server" Text='<%#GetJobName(Eval("toJobskyerID").ToString())%>' ></asp:Label>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
            </div>
                <div class="bottom">
               <div class="page">
                     <asp:Label ID="lbNow" runat="server" Text="当前页："></asp:Label>
                <asp:Label ID="lbPage" runat="server" Text="1"></asp:Label>
                &nbsp;
                <asp:Label ID="lbAll" runat="server" Text="总页数："></asp:Label>
                <asp:Label ID="lbCount" runat="server" Text=""></asp:Label>
                &nbsp;<asp:LinkButton ID="lbtnFirst" runat="server" onclick="lbtnFirst_Click">首页</asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="lbtnUp" runat="server" onclick="lbtnUp_Click">上一页</asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="lbtnDown" runat="server" onclick="lbtnDown_Click">下一页</asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="lbtnLast" runat="server" onclick="lbtnLast_Click">尾页</asp:LinkButton> 
                &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="80px">
                </asp:DropDownList> 
                <asp:LinkButton ID="lbtnGo" runat="server"  BackColor="LightBlue"
                BorderWidth="0px" BorderColor="Blue" onclick="lbtnGo_Click" style="width: 20px">Go</asp:LinkButton>
                    </div>
            </div>
        </div>
        
    <br />
</asp:Content>

