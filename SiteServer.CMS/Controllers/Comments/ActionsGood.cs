﻿using BaiRong.Core;

namespace SiteServer.CMS.Controllers.Comments
{
    public class ActionsGood
    {
        public const string Route = "stl/comments/{siteId}/{channelId}/{contentId}/actions/good";

        public static string GetUrl(string apiUrl, int siteId, int channelId, int contentId)
        {
            return PageUtils.Combine(apiUrl, Route
                .Replace("{siteId}", siteId.ToString())
                .Replace("{channelId}", channelId.ToString())
                .Replace("{contentId}", contentId.ToString()));
        }
    }
}