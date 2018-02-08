<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PagesiteContentEdit" EnableViewState="false" %>
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

  <div class="popover popover-static">
    <h3 class="popover-title">本组织机构基本信息</h3>
    <div class="popover-content">

      <table class="table noborder table-hover">      
        <tr>
          <td width="160">机构名称：</td>
          <td>
            <asp:TextBox Columns="25" MaxLength="50" id="PublishmentSystemName" runat="server"/>
            <asp:RequiredFieldValidator
              ControlToValidate="PublishmentSystemName"
              errorMessage=" *" foreColor="red"
              Display="Dynamic"
              runat="server"/>
            <asp:RegularExpressionValidator
              runat="server"
              ControlToValidate="PublishmentSystemName"
              ValidationExpression="[^']+"
              errorMessage=" *" foreColor="red"
              Display="Dynamic" />
          </td>
        </tr>
        <tr>
          <td>机构类型：</td>
          <td>
            <asp:DropDownList ID="PublishmentSystemType" runat="server"></asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td>机构分类：</td>
          <td>
            <asp:DropDownList ID="PublishmentSystemCategory" runat="server"></asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td width="160">联系电话：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="TelePhone" runat="server"/></td>
        </tr>
        <tr>
          <td width="160">详细地址：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="Address" runat="server"/></td>
        </tr>
       <%--  <tr>
          <td width="160">概况：</td>
          <td colspan="2"><bairong:TextEditorControl ID="BasicFactsContent" runat="server"></bairong:TextEditorControl></td>
        </tr>
         <tr>
          <td width="160">特色：</td>
          <td colspan="2"><bairong:TextEditorControl ID="CharacteristicContent" runat="server"></bairong:TextEditorControl></td>
        </tr>--%>
        
    </table>

      <hr />
      <table class="table noborder">
        <tr>
          <td class="center">
            <asp:Button class="btn btn-primary" id="Submit" text="修 改" runat="server"/>
            <input type="button" class="btn" value="返 回" onClick="javascript:location.href='pagePublishmentSystem.aspx';" />
          </td>
        </tr>
      </table>

    </div>
  </div>

</form>
</body>
</html>
