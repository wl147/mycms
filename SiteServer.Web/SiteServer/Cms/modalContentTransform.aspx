<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.ModalContentTransform" Trace="false" %>
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
        <asp:Button ID="btnSubmit" UseSubmitBehavior="false" OnClick="Submit_OnClick" runat="server" Style="display: none" />
        <bairong:Alerts runat="server"></bairong:Alerts>

        <table class="table table-noborder table-hover">
            <tr>
                <td width="120">当前组织机构：</td>
                <td>
                    <asp:Literal ID="ltlCurrentOrganizationName" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>报道组织机构：</td>
                <td>
                    <asp:Literal ID="ltlReachOrganizationName" runat="server"></asp:Literal></td>
            </tr>          
        </table>

    </form>
</body>
</html>
