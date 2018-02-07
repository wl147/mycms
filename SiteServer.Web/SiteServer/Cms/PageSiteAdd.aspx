<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageSiteAdd" EnableViewState="false" %>
<%@ Register TagPrefix="bairong" Namespace="SiteServer.BackgroundPages.Controls" Assembly="SiteServer.BackgroundPages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form class="form-inline" runat="server">
  <asp:Literal id="ltlBreadCrumb" runat="server" />
  <bairong:alerts runat="server" />

  <div class="popover popover-static">
    <h3 class="popover-title">新增组织机构</h3>
    <div class="popover-content">

      <table class="table noborder table-hover">
        <tr>
          <td width="160">地区：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="PublishmentSystemArea" runat="server"/></td>
        </tr>
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
         <tr>
          <td width="160">概况：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="BasicFacts" runat="server"/></td>
        </tr>
         <tr>
          <td width="160">特色：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="Characteristic" runat="server"/></td>
        </tr>
          <tr>
          <td width="160">超级管理账号：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="AdministratorAccount" runat="server"/></td>
        </tr>
         <tr>
          <td width="160">超级管理密码：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="AdministratorPassWord" runat="server"/></td>
        </tr>
        <tr>
          <td>超级管理角色：</td>
          <td>
            <asp:DropDownList ID="AdministratorRoles" runat="server"></asp:DropDownList>
          </td>
        </tr>
    </table>

      <hr />
      <table class="table noborder">
        <tr>
          <td class="center">
            <asp:Button class="btn btn-primary" id="Submit" text="添加" OnClick="Submit_OnClick" runat="server"/>
            <input type="button" class="btn" value="返 回" onClick="javascript:location.href='pagePublishmentSystem.aspx';" />
          </td>
        </tr>
      </table>

    </div>
  </div>

</form>
</body>
</html>
