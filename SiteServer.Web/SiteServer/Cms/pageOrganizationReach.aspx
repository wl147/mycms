<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageOrganizationReach" %>
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
    <div>组织报道<div></br>
    <div id="contentSearch" style="display:block;margin-top:10px;">
     选择社区: <asp:TextBox width="400" class="input-medium" ID="Keyword"  runat="server" />
      <asp:Button class="btn" OnClick="Search_OnClick" ID="Search" Text="查询" runat="server" /></br><br>
      <h4>请输入社区名称或地址点击查询按钮，在搜索结果中选中社区所在行，点击 <asp:Button class="btn" OnClick="Sumbmit_OnClick" ID="btnSubmintInfo" Text="确定" runat="server" /></h4><br>
    </div>
  </div>

  <table id="contents" class="table table-bordered table-hover">
    <tr class="info thead">
      <td width="50">ID</td>
      <td width="300">名称</td>
      <td width="200">类别</td>
      <td width="200">地址</td>
      <td width="50">选择</td>     
    </tr>
    <asp:Repeater ID="rptContents" runat="server">
      <itemtemplate>
        <tr>
          <td>
            <asp:Literal ID="ltlID" runat="server"></asp:Literal>
          </td>
          <td>
            <asp:Literal ID="ltlPublishmentSystemName" runat="server"></asp:Literal>
          </td>
          <td>
            <asp:Literal ID="ltlCategory" runat="server"></asp:Literal>
          </td>
          <td>
            <asp:Literal ID="ltlAddress" runat="server"></asp:Literal>
          </td> 
          <td class="center">
            <input type="checkbox" name="ContentIDCollection" value='<%#DataBinder.Eval(Container.DataItem, "ID")%>' />
          </td>
        </tr>
      </itemtemplate>
    </asp:Repeater>
  </table>

  <bairong:sqlPager id="spContents" runat="server" class="table table-pager" />

</form>
</body>
</html>
