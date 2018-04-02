<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageReachDetails" %>
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
  <div class="well well-small">
    <div>组织报道<div></br>
    <div id="contentSearch" style="display:block;margin-top:10px;">
     <h4>当前报道社区: <asp:Literal ID="ltlCommunity" runat="server"></asp:Literal>，点击 <asp:Literal ID="ltlContentButtons" runat="server"></asp:Literal></h4></br>
     <h4>报道时间:<asp:Literal ID="ltlTime" runat="server"></asp:Literal></h4>
    </div>
  </div>

  <%--<table id="contents" class="table table-bordered table-hover">
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
            <input type="checkbox" name="ContentIDCollection" value='<%#DataBinder.Eval(Container.DataItem, "PublishmentSystemId")%>' />
          </td>
        </tr>
      </itemtemplate>
    </asp:Repeater>
  </table>

  <bairong:sqlPager id="spContents" runat="server" class="table table-pager" />--%>

</form>
</body>
</html>
