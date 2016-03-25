<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JsCommonMain.aspx.cs" Inherits="JsCommon_JsCommonMain" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
    <div style="height: 503px" >
    <div class="JCM_left">
        
        <div class="JCM_pro"><a href="../Change/Change.aspx" title="点击更换头像">
            <img src="<%=imageUrl %>" id="ImgProfile" style="width: 220px; height: 226px;" /></a></div>
        <div class="JCM_info">姓名：<asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></div>
        <div class="JCM_info">院系：<asp:Label ID="lblAcademy" runat="server" Text="Academy"></asp:Label></div>
        <div class="JCM_info">组别：<asp:Label ID="lblPosition" runat="server" Text="Position"></asp:Label></div>
    </div>
    <div class="JCM_right">
        <div>近日公告</div>
        <div class="notice">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <p class="aboutNotice">
                        <asp:Label ID="lblTime" runat="server" Text='<%#Eval("notTime")%>'></asp:Label>
                    </p>
                    <p class="aboutNotice">
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("notTitle")%>'></asp:Label>
                    </p>
                    <p class="noticeDetail">
                        <asp:Label ID="lblContent" runat="server" Text='<%#Eval("notContent")%>'></asp:Label>
                    </p>
                    <p class="aboutNotice">
                        <asp:Label ID="lbljobName" runat="server" Text='<%#GetJobName(Eval("jobskyerID").ToString())%>' ></asp:Label>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        </div>
</div>
</asp:Content>

