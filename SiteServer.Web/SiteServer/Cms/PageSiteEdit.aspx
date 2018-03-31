<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageSiteEdit" %>
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
    <h3 class="popover-title">修改组织机构</h3>
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
             <td colspan="3">
                <asp:RadioButtonList CssClass="checkboxlist" ID="CblPublishmentSystemType" RepeatDirection="Horizontal" class="noborder" RepeatColumns="5" runat="server" />
             </td>
        </tr>     
         <tr>
          <td>机构分类：</td>
             <td colspan="3">
                <asp:RadioButtonList CssClass="checkboxlist" ID="CblPublishmentSystemCategory" RepeatDirection="Horizontal" class="noborder" RepeatColumns="5" runat="server" />
             </td>
        </tr>
        <tr>
          <td width="160">联系电话：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="TelePhone" runat="server"/></td>
        </tr>
           <tr>
          <td width="160">社区文化展示图片：</td>
          <td><asp:TextBox Columns="25" MaxLength="50" id="ImageUrl" runat="server"/></td>
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
            <asp:Repeater ID="rptContents" runat="server" onitemdatabound="rptContents_ItemDataBound">
              <itemtemplate>
                <tr>
                  <td>
                    <asp:HiddenField ID="hidNodeId" Value='<%#Eval("NodeId") %>' runat="server" />
                    <asp:HiddenField ID="hidName" Value='<%#Eval("NodeName") %>' runat="server" />
                    <asp:HiddenField ID="hidActionType" Value='<%#Eval("ChannelPermissions") %>' runat="server" />
                    <%#Eval("NodeName")%>
                  </td>
                  <td>
                    <%--<asp:CheckBoxList ID="ChannelPermissions" RepeatColumns="7" RepeatDirection="Horizontal" class="checkboxlist" runat="server"></asp:CheckBoxList>       --%>
                    <asp:CheckBoxList ID="cblActionType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="cbllist"></asp:CheckBoxList>  
                  </td>          
               </tr>
              </itemtemplate>
           </asp:Repeater>
      <asp:Repeater ID="rptWebSite" runat="server" onitemdatabound="rptWebSite_ItemDataBound">
      <itemtemplate>
        <tr>
          <td>
            <asp:HiddenField ID="hidPermission" Value='<%#((BaiRong.Core.Configuration.PermissionConfig)(Container.DataItem)).Name %>' runat="server" />           
            <%#((BaiRong.Core.Configuration.PermissionConfig)(Container.DataItem)).Text%>
          </td>
          <td>
            <asp:CheckBoxList ID="cblWebSiteType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="cbllist"></asp:CheckBoxList>  
          </td>          
        </tr>
      </itemtemplate>
    </asp:Repeater>
          </td>         
        </tr>
    </table>

      <hr />
      <table class="table noborder">
        <tr>
          <td class="center">
            <asp:Button class="btn btn-primary" id="Submit" text="修 改" OnClick="Submit_OnClick" runat="server"/>
           <%-- <input type="button" class="btn" value="返 回" onClick="javascript:location.href='pagePublishmentSystem.aspx?PublishmentSystemId=<%#ParentId%>';" />--%>
              <input type="button" class="btn" value="返 回" onClick="window.location.history.back(-1);" />
          </td>
        </tr>
      </table>

    </div>
  </div>

</form>
</body>
</html>
