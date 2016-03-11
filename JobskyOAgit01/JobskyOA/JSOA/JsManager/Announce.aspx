<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Announce.aspx.cs" Inherits="JSOA_JsManager_Announce" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>公告</title>
    <script type="text/javascript" charset="utf-8" src="../Plugins/UEditor-utf8-net/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../Plugins/UEditor-utf8-net/ueditor.all.min.js"> </script>
    <script src="../Plugins/UEditor-utf8-net/lang/zh-cn/zh-cn.js"></script>
    <link rel="Announce" href="Announce.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>   
        <span class="label-title">发布人：</span>
        <asp:TextBox ID="Annoucer" runat="server"></asp:TextBox><br />

        <span class="label-title">发布时间：</span>
        <asp:TextBox ID="Announce_Time" runat="server"></asp:TextBox><br />

        <span class="label-title">附件上传：</span>
        <asp:FileUpload ID="FileUpload" runat="server" />
        &nbsp;&nbsp;
        <asp:Button ID="FileUpload1" runat="server" Text="上传" Onclick="Upload_Click"/><br />  
        <!--<asp:Label ID="Label1" runat="server" Text="" Style="color:blue;"></asp:Label><br /> -->

        <span class="label-title">公告标题：</span>
        <asp:TextBox ID="Announce_Totle" runat="server"></asp:TextBox><br />

        <textarea class="middle" runat="server" ></textarea>    
        <div runat="server">
            <asp:HyperLink ID="lnkFrist" runat="server">第一页：</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="lnkPrev" runat="server">上一页</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="lnkNext" runat="server">下一页</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="lnkEnd" runat="server">最后一页</asp:HyperLink>&nbsp;&nbsp;
        </div>
        <script type="text/javascript">
            var ue = UE.getEditor('content');//得到编辑区的内容
        </script> 
    </div>
    </form>
</body>
</html>
