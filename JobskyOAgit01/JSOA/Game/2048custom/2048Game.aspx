<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="2048Game.aspx.cs" Inherits="Game_2048custom_2048Game" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../JS/jquery-1.11.1.min.js"></script>
    <link rel="stylesheet" type="text/css" href="2048.css" />

    <script type="text/javascript" src="support2048.js"></script>
    <script type="text/javascript" src="showanimation2048.js"></script>
    <script type="text/javascript" src="main2048.js"></script>
    <style type="text/css">
        .detail-block{width:300px;height:420px;text-align:center;position:fixed; z-index:100;top:100px;left:20px;border:2px solid;}
        .detail-block ur{}
    <meta charset="UTF-8" />
        .detail-block ur li{margin-top:10px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_ContentPlaceHolder" Runat="Server">
    <div>
        
        <header style="margin-left:500px">
        <p>score : <span id="score">0</span></p>
            
        <h3>2048 Jobskyers私人定制版----<asp:Label ID="name" runat="server" Text=""></asp:Label>(<asp:Label ID="lblusername" runat="server" Text=""></asp:Label>)</h3>
            
        <a href="javascript:newgame();" id="newgamebutton">New Game</a>
        <p><span id="msg" style="color:red"></span></p>
        </header>
        
    <div id="grid-container">
        <div class="grid-cell" id="grid-cell-0-0"></div>
        <div class="grid-cell" id="grid-cell-0-1"></div>
        <div class="grid-cell" id="grid-cell-0-2"></div>
        <div class="grid-cell" id="grid-cell-0-3"></div>

        <div class="grid-cell" id="grid-cell-1-0"></div>
        <div class="grid-cell" id="grid-cell-1-1"></div>
        <div class="grid-cell" id="grid-cell-1-2"></div>
        <div class="grid-cell" id="grid-cell-1-3"></div>

        <div class="grid-cell" id="grid-cell-2-0"></div>
        <div class="grid-cell" id="grid-cell-2-1"></div>
        <div class="grid-cell" id="grid-cell-2-2"></div>
        <div class="grid-cell" id="grid-cell-2-3"></div>

        <div class="grid-cell" id="grid-cell-3-0"></div>
        <div class="grid-cell" id="grid-cell-3-1"></div>
        <div class="grid-cell" id="grid-cell-3-2"></div>
        <div class="grid-cell" id="grid-cell-3-3"></div>
    </div>
    
        <div class="detail-block">
            <ur>
                <li><a href="../ScoreRank.aspx" target="_self">查看排行榜</a></li>
                <li>请使用键盘上的上、下、左、右的按键进行游戏。</li>
                <li>小白</li>
                <li>实习生</li>
                <li>程序猿</li>
                <li>项目经理</li>
                <li>架构师</li>
                <li>技术经理</li>
                <li>高级经理</li>
                <li>技术总监</li>
                <li>副总裁</li>
                <li>CTO</li>
                <li>总裁</li>
                <li>想知道后面的角色吗……来吧！！！</li>
            </ur>
        </div>
    </div>
</asp:Content>

