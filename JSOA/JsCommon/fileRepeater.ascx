<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fileRepeater.ascx.cs" Inherits="JsCommon_fileRepeater" %>

<div>
    <div>
        &nbsp;<asp:FileUpload ID="fileUp" runat="server" />
        <asp:Button ID="btnFileUp" runat="server" Text="上传" OnClick="btnFileUp_Click" />
        <asp:Label ID="lblMessage" runat="server" Text="lblMessage"></asp:Label>
        <asp:Label ID="lblDelMess" runat="server" Text="Label"></asp:Label>
    </div>
    <div>
        <div class="tableHeader">
            <p class="tableHead">
                文件名称</p>
            <p class="tableHead">
                上传者</p>
            <p class="tableHead">
                上传时间</p>
            <p class="tableHead">
                下载</p>
            <p class="tableHead">
                删除</p>
        </div>
        <div class="tableBody">    
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound" >
            <ItemTemplate>
                    <div class="tableContent">
                        <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("fileName")%>'></asp:Label></div>
                    <div class="tableContent">
                        <asp:Label ID="lblName" runat="server" Text='<%# GetJobName(Eval("jobskyerID").ToString()) %>'></asp:Label></div>
                    <div class="tableContent">
                        <asp:Label ID="lblSex" runat="server" Text='<%#Eval("fileUpTime")%>'></asp:Label></div>
                    <div class="tableContent">
                        <a href='<%#GetPath(Eval("fileName").ToString())%>' style="color: #FFFFFF">下载</a>
                    </div>
                    <div class="tableContent">
                        <asp:LinkButton ID="lbtnDelete" CommandName="lbtnDelete" runat="server" CommandArgument='<%#Eval("fileName") %>'
                            OnClientClick="javascript:return confirm('确认删除此文件吗？')" Text="删除" ForeColor="White"></asp:LinkButton></div> 
                         
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
                &nbsp;<asp:LinkButton ID="lbtnFirst" runat="server" onclick="lbtnFirst_Click" ForeColor="White">首页</asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="lbtnUp" runat="server" onclick="lbtnUp_Click" ForeColor="White">上一页</asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="lbtnDown" runat="server" onclick="lbtnDown_Click" ForeColor="White">下一页</asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="lbtnLast" runat="server" onclick="lbtnLast_Click" ForeColor="White">尾页</asp:LinkButton> 
                &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="80px">
                </asp:DropDownList> 
                <asp:LinkButton ID="lbtnGo" runat="server"  BackColor="LightBlue"
                BorderWidth="0px" BorderColor="Blue" onclick="lbtnGo_Click" style="width: 20px">Go</asp:LinkButton>
                    </div>
            </div>
        </div>
    </div>
        
