<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageStudyRecord" %>
<%@ Register TagPrefix="bairong" Namespace="SiteServer.BackgroundPages.Controls" Assembly="SiteServer.BackgroundPages" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<!--#include file="../inc/header.aspx"-->
</head>

<body>
<!--#include file="../inc/openWindow.html"-->
<form class="form-inline" runat="server">
  <asp:Literal id="ltlBreadCrumb" runat="server" />
  <bairong:alerts runat="server" />

  <script type="text/javascript" >
  $(document).ready(function(){
    loopRows(document.getElementById('contents'), function(cur){ cur.onclick = chkSelect; });
    $(".popover-hover").popover({trigger:'hover',html:true});
  });
  </script>


  <table id="contents" class="table table-bordered table-hover">
    <tr class="info thead">
      <td>序号</td>
      <td>姓名</td> 
      <td>学习状态</td>
      <td>最近一次学习时间</td>          
    </tr>
    <asp:Repeater ID="RptContents" runat="server">
      <itemtemplate>
        <tr>
          <td>
            <asp:Literal ID="tbId" runat="server"></asp:Literal>
          </td>
           <td>
            <asp:Literal ID="tbName" runat="server"></asp:Literal>
          </td> 
           <td>
            <asp:Literal ID="tbState" runat="server"></asp:Literal>
          </td> <td>
            <asp:Literal ID="tbLastStudyTime" runat="server"></asp:Literal>
          </td>
        </tr>
      </itemtemplate>
    </asp:Repeater>
  </table>
  <bairong:sqlPager id="SpContents" runat="server" class="table table-pager" />

  <hr />
   <table class="table noborder">
      <tr>
         <td class="center">
             <input class="btn" type="button" onclick="window.history.back(-1);" value="返 回" />
             <br>
         </td>
      </tr>
  </table>
</form>
</body>
</html>
