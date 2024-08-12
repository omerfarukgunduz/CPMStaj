﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using static Portal.Web.Apis.LinkedinApi.OnlyText.Body;

namespace Portal.Web.Apis.LinkedinApi.OnlyText
{
    public class MainProgramOnlyTextLinkedin
    {
        public static async Task RunLinkedInOnlyTextShareAsync(string Token,string Text)
        {
            LinkedinURLText URLS = new LinkedinURLText
            {
                accessToken = Token,//Your accsess Token

                Text = Text, //Content Text
                contentType = "application/json",

            };

            using (var profile = new HttpClient())
            {
                profile.DefaultRequestHeaders.Add("Authorization", "Bearer " + URLS.accessToken);
                var response = profile.GetAsync("https://api.linkedin.com/v2/userinfo").Result;

                var respContent = response.Content.ReadAsStringAsync().Result;

                JToken token = JObject.Parse(respContent);
                string profileId = (string)token["sub"];
                if (!string.IsNullOrEmpty(profileId))
                {
                    using (var client = new HttpClient())
                    {
                        LinkedinPostImageShareRequest request = new LinkedinPostImageShareRequest
                        {
                            author = "urn:li:person:" + profileId,
                            lifecycleState = "PUBLISHED",
                            specificContent = new Specificcontent
                            {
                                comlinkedinugcShareContent = new ComLinkedinUgcSharecontent
                                {
                                    shareCommentary = new Sharecommentary
                                    {
                                        text = URLS.Text,
                                    },
                                    shareMediaCategory = "NONE"
                                }
                            },
                            visibility = new Visibility
                            {
                                comlinkedinugcMemberNetworkVisibility = "PUBLIC"
                            }
                        };

                        var reqShareString = JsonConvert.SerializeObject(request);
                        StringContent contentShare = new StringContent(reqShareString, Encoding.UTF8, URLS.contentType);
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + URLS.accessToken);
                        var responseShare = profile.PostAsync("https://api.linkedin.com/v2/ugcPosts", contentShare).Result;
                        if (responseShare != null && responseShare.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Paylaşım Yapıldı");
                        }
                        else
                        {
                            Console.WriteLine(responseShare.StatusCode);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(response.StatusCode.ToString());
                }

            }
        }
    }
}
