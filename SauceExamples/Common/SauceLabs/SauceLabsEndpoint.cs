﻿using System;

namespace Common.SauceLabs
{
    public class SauceLabsEndpoint
    {
        public static string SauceUsWestDomain = "ondemand.us-west-1.saucelabs.com/wd/hub";
        public string SauceHubUrl => "https://ondemand.saucelabs.com/wd/hub";
        public Uri SauceHubUri => new Uri(SauceHubUrl);
        public Uri UsWestHubUri => new Uri($"https://{SauceUsWestDomain}");

        public string HeadlessSeleniumUrl => "https://ondemand.us-east-1.saucelabs.com/wd/hub";

        public string HeadlessRestApiUrl => "https://us-east-1.saucelabs.com/rest/v1";
    }
}