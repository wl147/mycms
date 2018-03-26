<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageDoubleReach" %>
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
    <div id="contentSearch" style="display:block;margin-top:10px;">
       报道类型:
      <asp:DropDownList ID="ReachCategory" AutoPostBack="true" OnSelectedIndexChanged="ChannelCategory_SelectedIndexChanged" class="input-medium" runat="server"> </asp:DropDownList>
       报道社区:
      <asp:DropDownList ID="SearchType" class="input-medium" runat="server"> </asp:DropDownList>  
    </div>
  </div>

  <table id="contents" class="table table-bordered table-hover">
    <tr class="info thead">
      <td width="250">ID</td>
      <td width="50">姓名</td>
         <td width="50">手机号码</td>
         <td width="50">所属机构</td>
         <td width="50">报道社区</td>
         <td width="50">报道时间</td>
         <td width="50">参与活动数</td>
         <td width="50">累计积分</td>
         <td width="50">操作</td>     
    </tr>
    <asp:Repeater ID="rptContents" runat="server">
      <itemtemplate>
        <tr>
          <td>
            <asp:Literal ID="ID" runat="server"></asp:Literal>
          </td>
             <td>
            <asp:Literal ID="ltlCategory" runat="server"></asp:Literal>
          </td>
          <asp:Literal ID="ltlColumnItemRows" runat="server"></asp:Literal>
          <td class="center" nowrap>
            <asp:Literal ID="ltlItemStatus" runat="server"></asp:Literal>
          </td>
          <td class="center">
            <asp:Literal ID="ltlItemEditUrl" runat="server"></asp:Literal>
          </td>
          <asp:Literal ID="ltlCommandItemRows" runat="server"></asp:Literal>
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
