<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Sys.PagePublishmentSystemDetails" EnableViewState="false" %>
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

        <asp:DataGrid ID="dgContents" ShowHeader="true" AutoGenerateColumns="false" HeaderStyle-CssClass="info thead" CssClass="table table-bordered table-hover" GridLines="none" runat="server">
            <Columns>
                <asp:TemplateColumn HeaderText="机构名称">
                    <ItemTemplate>
                        <asp:Literal ID="ltlPublishmentSystemName" runat="server"></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="地址">
                    <ItemTemplate>
                        <asp:Literal ID="ltlPublishmentSystemAdress" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="类型">
                    <ItemTemplate>
                        <asp:Literal ID="ltlPublishmentSystemType" runat="server"></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle Width="110" CssClass="center" />
                </asp:TemplateColumn>
                
                <asp:TemplateColumn HeaderText="操作">
                    <ItemTemplate>
                        <asp:Literal ID="ltlOperation" runat="server"></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle Width="70" HorizontalAlign="left" />
                </asp:TemplateColumn>                                    
               
              
            </Columns>
        </asp:DataGrid>

    </form>
</body>
</html>
