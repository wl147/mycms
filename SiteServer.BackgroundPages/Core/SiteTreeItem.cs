using BaiRong.Core;
using SiteServer.BackgroundPages.Ajax;
using SiteServer.CMS.Model;
using SiteServer.CMS.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteServer.BackgroundPages.Core
{
   public class SiteTreeItem
    {
        private readonly string _iconFolderUrl;
        private readonly string _iconOpenedFolderUrl;
        private readonly string _iconEmptyUrl;
        private readonly string _iconMinusUrl;
        private readonly string _iconPlusUrl;
        private readonly string _iconAddSiteUrl;
        private readonly string _iconDeleteSiteUrl;

        private bool _enabled = true;
        private PublishmentSystemInfo _publishmentSystemInfo;
        private string _administratorName;
        private int _currentMainId;


        private SiteTreeItem()
        {
            var treeDirectoryUrl = SiteServerAssets.GetIconUrl("tree");
            _iconFolderUrl = PageUtils.Combine(treeDirectoryUrl, "folder.gif");
            _iconOpenedFolderUrl = PageUtils.Combine(treeDirectoryUrl, "openedfolder.gif");
            _iconEmptyUrl = PageUtils.Combine(treeDirectoryUrl, "empty.gif");
            _iconMinusUrl = PageUtils.Combine(treeDirectoryUrl, "minus.png");
            _iconPlusUrl = PageUtils.Combine(treeDirectoryUrl, "plus.png");
            _iconAddSiteUrl= PageUtils.Combine(treeDirectoryUrl, "addSite.png");
            _iconDeleteSiteUrl=PageUtils.Combine(treeDirectoryUrl, "deleteSite.png");
        }
        public static SiteTreeItem CreateInstance(PublishmentSystemInfo publishmentSystemInfo,int currentMainId)
        {
            return new SiteTreeItem
            {
                _publishmentSystemInfo = publishmentSystemInfo,
                _currentMainId = currentMainId
            };            
        }

        public  string GetItemHtml()
        {
            var htmlBuilder = new StringBuilder();
            var parentsCount = _publishmentSystemInfo.ParentsCount;
            for (var i = 0; i < parentsCount; i++)
            {
                htmlBuilder.Append($@"<img align=""absmiddle"" src=""{_iconEmptyUrl}"" />");
            }
            if (_publishmentSystemInfo.ChildrenCount > 0)
            {
                if (_publishmentSystemInfo.PublishmentSystemId == _currentMainId)
                {
                    htmlBuilder.Append(
                        $@"<img align=""absmiddle"" style=""cursor:pointer"" onClick=""displayChildren(this);"" isAjax=""false"" isOpen=""false"" id=""{_publishmentSystemInfo.PublishmentSystemId}"" src=""{_iconMinusUrl}"" />");
                }
                else
                {
                    htmlBuilder.Append(
                        $@"<img align=""absmiddle"" style=""cursor:pointer"" onClick=""displayChildren(this);"" isAjax=""true"" isOpen=""true"" id=""{_publishmentSystemInfo.PublishmentSystemId}"" src=""{_iconPlusUrl}"" />");
                }
            }
            else
            {
                htmlBuilder.Append($@"<img align=""absmiddle"" src=""{_iconEmptyUrl}"" />");
            }

            htmlBuilder.Append($@"&nbsp;<a href=""pagepublishmentsystemadd.aspx?publishmentSystemType=WCM"" target=""content"" title=""{_publishmentSystemInfo}""><img align=""absmiddle"" border=""0"" src=""{_iconFolderUrl}"" />{_publishmentSystemInfo.PublishmentSystemName}</a>&nbsp;");

            htmlBuilder.Append($@"&nbsp;<a href=""pagepublishmentsystemadd.aspx?publishmentSystemType=WCM"" target=""content"" title=""添加""><img align=""absmiddle"" border=""0"" src=""{_iconAddSiteUrl}"" /></a>&nbsp;");

            htmlBuilder.Append($@"&nbsp;<a href=""pagepublishmentsystemadd.aspx?publishmentSystemType=WCM"" target=""content"" title=""删除""><img align=""absmiddle"" border=""0"" src=""{_iconDeleteSiteUrl}"" /></a>&nbsp;");

            return htmlBuilder.ToString();
        }

        public static string GetScript(PublishmentSystemInfo publishmentSystemInfo, ELoadingType loadingType, NameValueCollection additional)
        {
            var script = @"
<script language=""JavaScript"">
function getTreeLevel(e) {
	var length = 0;
	if (!isNull(e)){
		if (e.tagName == 'TR') {
			length = parseInt(e.getAttribute('treeItemLevel'));
		}
	}
	return length;
}

function getTrElement(element){
	if (isNull(element)) return;
	for (element = element.parentNode;;){
		if (element != null && element.tagName == 'TR'){
			break;
		}else{
			element = element.parentNode;
		} 
	}
	return element;
}

function getImgClickableElementByTr(element){
	if (isNull(element) || element.tagName != 'TR') return;
	var img = null;
	if (!isNull(element.childNodes)){
		var imgCol = element.getElementsByTagName('IMG');
		if (!isNull(imgCol)){
			for (x=0;x<imgCol.length;x++){
				if (!isNull(imgCol.item(x).getAttribute('isOpen'))){
					img = imgCol.item(x);
					break;
				}
			}
		}
	}
	return img;
}

var weightedLink = null;

function fontWeightLink(element){
    if (weightedLink != null)
    {
        weightedLink.style.fontWeight = 'normal';
    }
    element.style.fontWeight = 'bold';
    weightedLink = element;
}

var completedNodeID = null;
function displayChildren(img){
	if (isNull(img)) return;

	var tr = getTrElement(img);

    var isToOpen = img.getAttribute('isOpen') == 'false';
    var isByAjax = img.getAttribute('isAjax') == 'true';
    var nodeID = img.getAttribute('id');

	if (!isNull(img) && img.getAttribute('isOpen') != null){
		if (img.getAttribute('isOpen') == 'false'){
			img.setAttribute('isOpen', 'true');
            img.setAttribute('src', '{iconMinusUrl}');
		}else{
            img.setAttribute('isOpen', 'false');
            img.setAttribute('src', '{iconPlusUrl}');
		}
	}

    if (isToOpen && isByAjax)
    {
        var div = document.createElement('div');
        div.innerHTML = ""<img align='absmiddle' border='0' src='{iconLoadingUrl}' /> 加载中，请稍候..."";
        img.parentNode.appendChild(div);
        $(div).addClass('loading');
        loadingChannels(tr, img, div, nodeID);
    }
    else
    {
        var level = getTreeLevel(tr);
    	
	    var collection = new Array();
	    var index = 0;

	    for ( var e = tr.nextSibling; !isNull(e) ; e = e.nextSibling) {
		    if (!isNull(e) && !isNull(e.tagName) && e.tagName == 'TR'){
		        var currentLevel = getTreeLevel(e);
		        if (currentLevel <= level) break;
		        if(e.style.display == '') {
			        e.style.display = 'none';
		        }else{
			        if (currentLevel != level + 1) continue;
			        e.style.display = '';
			        var imgClickable = getImgClickableElementByTr(e);
			        if (!isNull(imgClickable)){
				        if (!isNull(imgClickable.getAttribute('isOpen')) && imgClickable.getAttribute('isOpen') =='true'){
					        imgClickable.setAttribute('isOpen', 'false');
                            imgClickable.setAttribute('src', '{iconPlusUrl}');
					        collection[index] = imgClickable;
					        index++;
				        }
			        }
		        }
            }
	    }
    	
	    if (index > 0){
		    for (i=0;i<=index;i++){
			    displayChildren(collection[i]);
		    }
	    }
    }
}
";

            script += $@"
function loadingChannels(tr, img, div, nodeID){{
    var url = '{AjaxOtherService.GetGetLoadingSitesUrl()}';
    var pars = '{AjaxOtherService.GetGetLoadingChannelsParameters(publishmentSystemInfo.PublishmentSystemId, loadingType, additional)}&parentID=' + nodeID;

    jQuery.post(url, pars, function(data, textStatus)
    {{
        $($.parseHTML(data)).insertAfter($(tr));
        img.setAttribute('isAjax', 'false');
        img.parentNode.removeChild(div);
    }});
    completedNodeID = nodeID;
}}

function loadingChannelsOnLoad(paths){{
    if (paths && paths.length > 0){{
        var nodeIDs = paths.split(',');
        var nodeID = nodeIDs[0];
        var img = $('#' + nodeID);
        if (img.attr('isOpen') == 'false'){{
            displayChildren(img[0]);
            if (completedNodeID && completedNodeID == nodeID){{
                if (paths.indexOf(',') != -1){{
paths = paths.substring(paths.indexOf(',') + 1);
                    setTimeout(""loadingChannelsOnLoad('"" + paths + ""')"", 1000);
                }}
            }} 
        }}
    }}
}}
</script>
";

            var item = new SiteTreeItem();
            script = script.Replace("{iconEmptyUrl}", item._iconEmptyUrl);
            script = script.Replace("{iconFolderUrl}", item._iconFolderUrl);
            script = script.Replace("{iconMinusUrl}", item._iconMinusUrl);
            script = script.Replace("{iconOpenedFolderUrl}", item._iconOpenedFolderUrl);
            script = script.Replace("{iconPlusUrl}", item._iconPlusUrl);

            script = script.Replace("{iconLoadingUrl}", SiteServerAssets.GetIconUrl("loading.gif"));
            return script;
        }
    }
}
