<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.Cms.PageSiteAdd" %>
<%@ Register TagPrefix="bairong" Namespace="SiteServer.BackgroundPages.Controls" Assembly="SiteServer.BackgroundPages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
     <script type="text/javascript" language="javascript">
    jQuery.fn.center = function () {
        this.css("position", "absolute");
        var t = ($(window).height() - this.height() - 150) / 2;
        if (t <= 0) t = 10;
        var top = t + $(window).scrollTop();
        if (top < 0) top = $(window).height() >= this.height() ? 10 : 0;
        this.css("top", top + "px");
        var left = ($(window).width() - this.width()) / 2 + $(window).scrollLeft();
        if ($(window).width() <= this.width() + 20) left = 0;
        this.css("margin-left", "0");
        this.css("left", left + "px");
        return this;
    }
    function openWindow(title, url, width, height, isCloseOnly) {
        if (width == '0') width = $(window).width() - 40;
        if (height == '0') height = $(window).height() - 60;
        if (!width) width = 450;
        if (!height) height = 350;
        $('#openWindowModal h3').html(title);
        $('#openWindowBtn').show();
        if (isCloseOnly == 'true') $('#openWindowBtn').hide();
        $('#openWindowIFrame').attr('src', url);
        $('#openWindowModal').width(width);
        $('#openWindowModal .modal-body').css('max-height', '9999px');
        $('#openWindowModal').height(height);
        $('#openWindowModal .modal-body').height(height - 110);
        $('#openWindowIFrame').height(height - 120);
        $('#openWindowModal').center();
        //$("body").eq(0).css("overflow","hidden");
        $('#openWindowModal').modal({ keyboard: true });
        return false;
    }
    function closeWindow() {
        $('#openWindowModal').modal('hide');
    }
    $(document).ready(function () {
        $('#openWindowBtn').click(function (e) {
            //$('#openWindowBtn').button('loading');
            var UE = document.getElementById("openWindowIFrame").contentWindow.UE;
            if (UE) {
                $.each(UE.instants, function (index, editor) {
                    editor.sync();
                });
            }
            if ($('#openWindowIFrame').contents().find("#btnSubmit").length > 0) {
                $('#openWindowIFrame').contents().find("#btnSubmit").click();
            } else {
                $('#openWindowIFrame').contents().find("form").submit();
            }
        });
        $('#openWindowModal').bind('hidden', function () {
            //$("body").eq(0).css("overflow","scroll");
            $('#openWindowIFrame').attr('src', '');
            //$('#openWindowBtn').button('reset');
        });
    });

    function openTips(tips, type, isCloseOnly, btnValue, btnClick) {
        $('#alertType').removeClass();
        if (!type) type = "info";
        if (type == "success") {
            $('#alertType').addClass('alert alert-success');
        } else if (type == "error") {
            $('#alertType').addClass('alert alert-error');
        } else if (type == "info") {
            $('#alertType').addClass('alert alert-info');
        } else if (type == "warn") {
            $('#alertType').addClass('alert alert-block');
        }
        $('#alertType').html(tips);
        $('#openTipsModal').modal();

        //提示确认操作
        $('#openTipsBtn').show();
        if (isCloseOnly == undefined) isCloseOnly = "true";
        if (isCloseOnly == "true") $('#openTipsBtn').hide();
        if (!!btnValue) $("#openTipsBtn").html(btnValue);

        $("#openTipsBtn").click(function (e) {
            if (!!btnClick) {
                eval("(" + btnClick + "())");
            }
            $("#Submit").attr("onclick", "").click();
        });
    }
    function showTips(tips, type) {
        $('.alert').hide();
        $('#alert').removeClass();
        if (!type) type = "info";
        if (type == "success") {
            $('#alert').addClass('alert alert-success');
        } else if (type == "error") {
            $('#alert').addClass('alert alert-error');
        } else if (type == "info") {
            $('#alert').addClass('alert alert-info');
        } else if (type == "warn") {
            $('#alert').addClass('alert alert-block');
        }
        $('#alertMessage').html(tips);
        $('#alert').show();
    }
</script>
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
        <%--<tr>
          <td>机构类型：</td>
          <td>
            <asp:DropDownList ID="PublishmentSystemType" runat="server"></asp:DropDownList>
          </td>
        </tr>--%>
        <tr>
          <td>机构类型：</td>
             <td colspan="3">
                <asp:RadioButtonList CssClass="checkboxlist" ID="CblPublishmentSystemType" RepeatDirection="Horizontal" class="noborder" RepeatColumns="5" runat="server" />
             </td>
        </tr>
       <%-- <tr>
          <td>机构分类：</td>
          <td>
            <asp:DropDownList ID="PublishmentSystemCategory" runat="server"></asp:DropDownList>
          </td>
        </tr>--%>
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
            <td>社区文化展示图片：</td>
            <td>
              <asp:TextBox ID="ImageUrl" MaxLength="50" size="45" runat="server" />   
              <asp:Button ID="UploadImage" class="btn" Text="上传" runat="server"></asp:Button>
             </td>
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
