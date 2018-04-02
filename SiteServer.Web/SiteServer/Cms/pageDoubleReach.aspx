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
      <asp:DropDownList ID="ReachCategory" AutoPostBack="true" OnSelectedIndexChanged="ChannelCategory_SelectedIndexChanged"  class="input-medium" runat="server"> </asp:DropDownList>
      报道社区:
      <asp:DropDownList ID="SearchType" class="input-medium" runat="server"> </asp:DropDownList>  
    </div>
  </div>
 <asp:PlaceHolder id="phPersonal" runat="server" Visible="false"> 
  <table id="contents" class="table table-bordered table-hover">
    <tr class="info thead">
       <td width="20">
        <input type="checkbox" onClick="selectRows(document.getElementById('contents'), this.checked);">
      </td>
      <td width="50">ID</td>
      <td width="50">姓名</td>
         <td width="50">手机号码</td>
         <td width="50">所属机构</td>
         <td width="50">报道社区</td>
         <td width="50">报道时间</td>
         <td width="50">参与活动数</td>
         <td width="50">累计积分</td>
         <td width="50">操作</td>     
    </tr>
    <asp:Repeater ID="rptPersonal" runat="server">
      <itemtemplate>
        <tr>
          <td class="center">
            <input type="checkbox" name="ContentIDCollection" value='<%#DataBinder.Eval(Container.DataItem, "ID")%>' />
          </td>
          <td>
            <asp:Literal ID="ltlPersonalID" runat="server"></asp:Literal>
          </td>
          <td>
          <asp:Literal ID="ltlPersonalName" runat="server"></asp:Literal>
          </td>
          <td class="center">
            <asp:Literal ID="ltlPersonalMobile" runat="server"></asp:Literal>
          </td>
          <td class="center">
            <asp:Literal ID="ltlPersonalCommunity" runat="server"></asp:Literal>
          </td>
          <td class="center">
            <asp:Literal ID="ltlPersonalReachCommunity" runat="server"></asp:Literal>
          </td>
         <td>
          <asp:Literal ID="ltlPersonalReachTime" runat="server"></asp:Literal>
         </td>
         <td>
          <asp:Literal ID="ltlPersonalActivityCount" runat="server"></asp:Literal>
         </td>
         <td>
          <asp:Literal ID="ltlPersonalIntegral" runat="server"></asp:Literal>
         </td>
         <td>
          <asp:Literal ID="ltlPersonalOperation" runat="server"></asp:Literal>
         </td>
          
        </tr>
      </itemtemplate>
    </asp:Repeater>
  </table>
  <bairong:sqlPager id="spContentsPersonal" runat="server" class="table table-pager" />
</asp:PlaceHolder>
 <asp:PlaceHolder id="phOrganization" runat="server" Visible="false"> 
  <table id="contents" class="table table-bordered table-hover">
    <tr class="info thead">
      <td width="20">
        <input type="checkbox" onClick="selectRows(document.getElementById('contents'), this.checked);">
      </td>
      <td width="50">ID</td>
      <td width="50">组织名称</td>
      <td width="50">负责人姓名</td>
      <td width="50">电话</td>
      <td width="50">报道社区</td>
      <td width="50">报道时间</td>
      <td width="50">参与活动数</td>   
    </tr>
    <asp:Repeater ID="rpOrganization" runat="server">
      <itemtemplate>
       <tr>
          <td class="center">
            <input type="checkbox" name="ContentIDCollection" value='<%#DataBinder.Eval(Container.DataItem, "ID")%>' />
          </td>
          <td>
            <asp:Literal ID="ltlOrganizationID" runat="server"></asp:Literal>
          </td>
          <td>
          <asp:Literal ID="ltlOrganizationName" runat="server"></asp:Literal>
          </td>
          <td>
          <asp:Literal ID="ltlOrganizationChargeName" runat="server"></asp:Literal>
          </td>
          <td class="center">
            <asp:Literal ID="ltlOrganizationMobile" runat="server"></asp:Literal>
          </td>
          <td class="center">
            <asp:Literal ID="ltlOrganizationReachCommunity" runat="server"></asp:Literal>
          </td>
         <td>
          <asp:Literal ID="ltlOrganizationReachTime" runat="server"></asp:Literal>
         </td>
         <td>
          <asp:Literal ID="ltlOrganizationActivityCount" runat="server"></asp:Literal>
         </td>
        </tr>
        </tr>
      </itemtemplate>
    </asp:Repeater>
  </table>
  <bairong:sqlPager id="spOrganization" runat="server" class="table table-pager" />
</asp:PlaceHolder>
</form>
</body>
</html>
