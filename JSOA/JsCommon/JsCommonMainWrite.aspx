<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JsCommonMainWrite.aspx.cs" Inherits="JsCommon_JsCommonMainWrite" %>
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
            <div style="width:55%;height:16px;margin-top:5px;position:relative;border-top-left-radius:10px; border-top-right-radius:10px;">
                <div style="position:absolute; text-align:center;margin-left:120px;margin-top:2px;background-color:yellowgreen;color:black">
                    <h4>书写我的精彩*_*</h4>
                </div>
            </div>
            <div id="AddAphorism"> 
                <table>
                    <asp:TextBox ID="AddAphorismText" runat="server" TextMode="MultiLine" Width="450px" Height="360px" BackColor="#ccccff" OnTextChanged="AddAphorismText_TextChanged"></asp:TextBox>                   
                </table>
                <div style="float:right">
                   <asp:ImageButton ID="SubmitAphorism" runat="server" ImageUrl="~/Image/JsCommonMain/Notice-01.png" OnClientClick="javascript:return confirm('确定添加？');" OnClick="SubmitAnnounce_Click" />
                </div>
            </div>
        </div>

    </div>
</asp:Content>

