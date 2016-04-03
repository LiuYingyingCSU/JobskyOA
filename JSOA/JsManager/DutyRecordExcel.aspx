<%@ Page Title="" Language="C#" MasterPageFile="~/JsManager/Manager_Master.master" AutoEventWireup="true" CodeFile="DutyRecordExcel.aspx.cs" Inherits="JsManager_DutyRecordExcel" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Manager_ContentPlaceHolder" Runat="Server">
    <asp:GridView ID="DutyRecordGridView" runat="server" AutoGenerateColumns="false" CellPadding="3" BackColor="#ffffcc" BorderColor="#666666" BorderStyle="None" BorderWidth="1px" Font-Size="12px" OnSelectedIndexChanged="DutyRecordGridView_SelectedIndexChanged" OnRowDeleted="DutyRecordGridView_RowDeleted">
        
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <Columns>
            <asp:BoundField DataField="jobskyerID" HeaderText="学号" ReadOnly="true" />
            <asp:BoundField DataField="jobName" HeaderText="值班人" />
            <asp:BoundField DataField="dutyInTime" HeaderText="签到时间" />
            <asp:BoundField DataField="dutyOutTime" HeaderText="签退时间" />
            <asp:BoundField DataField="flag0" HeaderText="签到状况" />
            <asp:BoundField DataField="flag1" HeaderText="签退状况" />
            <asp:BoundField DataField="jobName" HeaderText="被代班人" />
            <asp:CommandField HeaderText="删除" ShowDeleteButton="true" />
        </Columns>
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="middle"/>
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <div>
        <asp:Button ID="toDutyRecord" runat="server" Text="查看值班表" OnClick="toDutyRecord_Click" />&nbsp&nbsp
        <asp:Button ID="toManagerDutyTime" runat="server" Text="设置值班表" OnClick="toManagerDutyTime_Click" />               
        <div style="float:right">
            <asp:Button ID="FromExcel" runat="server" OnClick="FromExcel_Click" Text="导入" />&nbsp;&nbsp;
            <asp:Button ID="ToExcel" runat="server" OnClick="ToExcel_Click" Text="导出" />
        </div>
    </div>  
    <div>
        <webdiyer:AspNetPager ID="DutyRecordAspNetPager" runat="server" CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条" FirstPageText="首页" LastPageText="尾页" LayoutType="Table" NextPageText="下一页" OnPageChanging="DutyRecordAspNetPager_PageChanging" PageIndexBoxType="DropDownList" PagingButtonLayoutType="Span" PrevPageText="上一页" ShowCustomInfoSection="Left" ShowPageIndexBox="Always" SubmitButtonText="Go" PageSize="7" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">

        </webdiyer:AspNetPager>
    </div>
</asp:Content>

