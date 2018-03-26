<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageContentPushOut" %>
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

  <script type="text/javascript">
  $(document).ready(function()
  {
    loopRows(document.getElementById('contents'), function(cur){ cur.onclick = chkSelect; });
    $(".popover-hover").popover({trigger:'hover',html:true});
  });
  </script>

  <div class="well well-small">
      <h5>首页>资料审核>推出管理</h5>
    <div id="contentSearch" style="display:block;margin-top:10px;">
       内容分类：
      <asp:DropDownList ID="ChannelCategory" class="input-medium" runat="server"> </asp:DropDownList>
    </div>
  </div>

  <table id="contents" class="table table-bordered table-hover">
    <tr class="info thead">
      <td>ID </td>
      <td>内容标题 </td>     
      <td>类型 </td>
      <td>推送类型 </td>
      <td>目标机构 </td>
      <td>操作时间 </td>
      <td>审核状态 </td>    
    </tr>
    <asp:Repeater ID="rptContents" runat="server">
      <itemtemplate>
        <tr>
          <td>
            <asp:Literal ID="ltlID" runat="server"></asp:Literal>
          </td>
          <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
          <td class="center" nowrap>
            <asp:Literal ID="ltlCategory" runat="server"></asp:Literal>
          </td
           <td class="center" nowrap>
            <asp:Literal ID="ltlPushCategory" runat="server"></asp:Literal>
          </td>
          <td class="center" nowrap>
            <asp:Literal ID="ltlGoal" runat="server"></asp:Literal>
          </td>
          <td class="center">
            <asp:Literal ID="ltlOperationTime" runat="server"></asp:Literal>
          </td>
          <asp:Literal ID="ltlState" runat="server"></asp:Literal>
        </tr>
      </itemtemplate>
    </asp:Repeater>
  </table>

  <bairong:sqlPager id="spContents" runat="server" class="table table-pager" />

</form>
</body>
</html>
