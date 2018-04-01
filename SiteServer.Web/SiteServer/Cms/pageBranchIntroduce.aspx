<%@ Page Language="C#" ValidateRequest="false" Inherits="SiteServer.BackgroundPages.Cms.PageBranchIntroduce" %>
<%@ Register TagPrefix="bairong" Namespace="SiteServer.BackgroundPages.Controls" Assembly="SiteServer.BackgroundPages" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <!--#include file="../inc/header.aspx"-->
</head>

<body>
    <!--#include file="../inc/openWindow.html"-->
    <form id="myForm" class="form-inline" enctype="multipart/form-data" runat="server">
        <asp:Literal ID="ltlBreadCrumb" runat="server" />
        <bairong:Alerts runat="server" />

        <script type="text/javascript" charset="utf-8" src="../assets/validate.js"></script>
        <script type="text/javascript" charset="utf-8" src="../assets/jquery/jquery.form.js"></script>
        <script src="../assets/jscolor/jscolor.js"></script>
        <script type="text/javascript" charset="utf-8" src="js/contentAdd.js"></script>

        <div class="popover popover-static">
            <h3 class="popover-title"><asp:Literal ID="LtlPageTitle" runat="server" /></h3>
            <div class="popover-content">
                </br>
                <table class="table table-fixed noborder" style="position: relative; top: -30px;">
                     <asp:PlaceHolder ID="PhSpecial" Visible="true" runat="server">
                        <tr>
                            <td>介绍内容：</td>
                            <td colspan="3">
                                 <asp:TextBox ID="Content" TextMode="MultiLine" Rows="20"   Width="600" runat="server" ></asp:TextBox>
                            </td>
                        </tr>
                    </asp:PlaceHolder>                   

                </table>

                <hr />
                <table class="table noborder">
                    <tr>
                        <td class="center">
                            <asp:Button class="btn btn-primary" itemIndex="1" ID="BtnSubmit" Text="确 定" OnClick="Submit_OnClick" runat="server" />
                            <input class="btn" type="button" onclick="location.href='#    ';return false;" value="返 回" />

                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </form>
</body>
</html>
