<%@ Page Language="C#" Inherits="SiteServer.BackgroundPages.BasePageCms" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <script type="text/javascript" src="../assets/jquery/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../assets/main.js"></script>
    <script type="text/javascript">

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
</head>
<frameset id="frame" framespacing="0" border="false" cols="180,*" frameborder="0" scrolling="yes">
	<frame name="tree" scrolling="auto" marginwidth="0"  style="display:none" marginheight="0" width="0" src="siteTree.aspx?PublishmentSystemID=<%=PublishmentSystemId%>&RightPageURL=pageContent.aspx" >
   
	<frame name="content" scrolling="auto" marginwidth="0" marginheight="0" src="../pageBlank.html">
</frameset>
<noframes>
<body>
<p>This page uses frames, but your browser doesn't support them.</p>
</body>
</noframes>
</html>

