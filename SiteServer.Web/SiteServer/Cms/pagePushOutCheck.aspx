<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PagePushOutCheck" %>

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
        <asp:Literal ID="ltlBreadCrumb" runat="server" />
        <bairong:Alerts runat="server" />

        <table class="table table-bordered table-hover">
            <tr class="info thead">
                <td width="20"><input onclick="_checkFormAll(this.checked)" type="checkbox" /></td>
                 <td style="display:none" width="60">ID</td>
                 <td width="100">申请人手机号码</td>
                 <td width="100">姓名</td>
                 <td width="80">转入机构</td>
                 <td width="80">申请时间</td>
                 <td width="100">转出审核状态</td>
                 <td width="100">转入审核状态</td>
                 <td style="display:none" width="100">操作</td>
                
            </tr>
            <asp:Repeater ID="RptContents" runat="server">
                <ItemTemplate>
                    <tr>
                         <td class="center">
                            <asp:Literal ID="ltlSelect" runat="server"></asp:Literal>
                        </td>
                        <td class="center" style="display:none">
                            <asp:Literal ID="ltlID"  runat="server"></asp:Literal>
                        </td>
                        <td class="center">
                            <asp:Literal ID="ltlMobilePhone" runat="server"></asp:Literal>
                        </td>
                        <td class="center">
                            <asp:Literal ID="ltlUserName" runat="server"></asp:Literal>
                        </td>
                        <td class="center">
                            <asp:Literal ID="ltlTransformIn" runat="server"></asp:Literal>
                        </td>
                        <td class="center">
                            <asp:Literal ID="ltlApplyDate" runat="server"></asp:Literal>
                        </td>
                        <td class="center">
                            <asp:Literal ID="ltlOutCheckState" runat="server"></asp:Literal>
                        </td>
                        <td class="center">
                            <asp:Literal ID="ltlInCheckState" runat="server"></asp:Literal>
                        </td>
                        <td style="display:none" class="center">
                            <asp:HyperLink ID="hlEditLink" Text="编辑" runat="server"></asp:HyperLink>
                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

        <bairong:SqlCountPager ID="SpContents" runat="server" class="table table-pager" />

        <ul class="breadcrumb breadcrumb-button">
            <asp:Button class="btn btn-success" ID="BtnCheckYes" Text="审核" runat="server" />
            <asp:Button class="btn btn-success" ID="BtnCheckNo" Text="驳回" runat="server" />
        </ul>

    </form>
</body>

</html>
