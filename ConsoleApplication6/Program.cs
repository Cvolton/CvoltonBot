using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Net.Http;
using System.IO;
using VideoLibrary;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.Net;
using YoutubeExtractor;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start();
        }

        private DiscordClient _client;

        public void Start()
        {
            _client = new DiscordClient(x =>
            {
                x.AppName = "CvoltonBot";
                x.AppUrl = "http://cvoltongdps.altervista.org";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });

            var token = "MjcxNzQzMDc3NDYxMzkzNDA5.C2vTwA.pyoj1Riz3ddmNxxTAxGkL-DuoLU";

            CreateCommands();

            _client.ExecuteAndWait(async() =>{
                await _client.Connect(token,TokenType.Bot);
            });
        }

        public void CreateCommands()
        {
            var cservice = _client.GetService<CommandService>();

            cservice.CreateCommand("level")
                .Description("Shows informations about a level")
                .Parameter("level", ParameterType.Unparsed)
                .Do(async (a) =>
                {
                    using (var client = new HttpClient())
                    {
                        var values = new Dictionary<string, string>
                    {
                        { "type", "0" },
                        {"diff", "-"},
                        {"len", "-" },
                        { "str", a.GetArg("level") }
                    };

                        var content = new FormUrlEncodedContent(values);

                        var response = await client.PostAsync("http://cvoltongdps.altervista.org/getGJLevels20.php", content);

                        var responseString = await response.Content.ReadAsStringAsync();

                        string[] stuff = responseString.Split('#');
                        string[] authorinfo = stuff[1].Split(':');
                        string[] levelinfo = stuff[0].Split('|');
                        string[] level = levelinfo[0].Split(':');
                        string song = "";
                        string difficulty = "";

                        if (level[45] == "0")
                        {
                            if (level[15] == "0")
                            {
                                song = "Stereo Madness by ForeverBound";
                            }
                            if (level[15] == "1")
                            {
                                song = "Back on Track by DJVI";
                            }
                            if (level[15] == "2")
                            {
                                song = "Polargeist by Step";
                            }
                            if (level[15] == "3")
                            {
                                song = "Dry Out by DJVI";
                            }
                            if (level[15] == "4")
                            {
                                song = "Base after Base by DJVI";
                            }
                            if (level[15] == "5")
                            {
                                song = "Can't Let Go by DJVI";
                            }
                            if (level[15] == "6")
                            {
                                song = "Jumper by Waterflame";
                            }
                            if (level[15] == "7")
                            {
                                song = "Time Machine by Waterflame";
                            }

                            if (level[15] == "8")
                            {
                                song = "Cycles by DJVI";
                            }
                            if (level[15] == "9")
                            {
                                song = "xStep by DJVI";
                            }
                            if (level[15] == "10")
                            {
                                song = "Clutterfunk by Waterflame";
                            }
                            if (level[15] == "11")
                            {
                                song = "Theory of Everything by DJ-Nate";
                            }
                            if (level[15] == "12")
                            {
                                song = "Electroman Adventures by Waterflame";
                            }
                            if (level[15] == "13")
                            {
                                song = "Club Step by DJ-Nate";
                            }
                            if (level[15] == "14")
                            {
                                song = "Electrodynamix by DJ-Nate";
                            }
                            if (level[15] == "15")
                            {
                                song = "Hexagon Force by Waterflame";
                            }
                            if (level[15] == "16")
                            {
                                song = "Blast Processing by Waterflame";
                            }
                            if (level[15] == "17")
                            {
                                song = "Theory of Everything 2 by DJ-Nate & F-777";
                            }
                            if (level[15] == "18")
                            {
                                song = "Geometrical Dominator by Waterflame";
                            }
                            if (level[15] == "19")
                            {
                                song = "Deadlocked by F-777";
                            }
                            if (level[15] == "20")
                            {
                                song = "Fingerbang by MDK";
                            }
                        }
                        else
                        {
                            var values2 = new Dictionary<string, string>
                       {
                             { "songID", level[45] }
                        };

                            var content2 = new FormUrlEncodedContent(values2);

                            var response2 = await client.PostAsync("http://cvoltongdps.altervista.org/getGJSongInfo.php", content2);

                            var songinfo = await response2.Content.ReadAsStringAsync();

                            songinfo = songinfo.Replace("~", "");

                            string[] songstuff = songinfo.Split('|');

                            song = songstuff[3] + " by " + songstuff[7];
                        }
                        if (level[11] == "0")
                        {
                            difficulty = "N/A";
                        }
                        if (level[11] == "10")
                        {
                            difficulty = "Easy";
                        }
                        if (level[11] == "20")
                        {
                            difficulty = "Normal";
                        }
                        if (level[11] == "30")
                        {
                            difficulty = "Hard";
                        }
                        if (level[11] == "40")
                        {
                            difficulty = "Harder";
                        }
                        if (level[11] == "50")
                        {
                            difficulty = "Insane";
                        }
                        if (level[21] == "1")
                        {
                            difficulty = "Demon";
                        }
                        if (level[25] == "1")
                        {
                            difficulty = "Auto";
                        }
                        await a.Channel.SendMessage("**NAME:** " + level[3] + Environment.NewLine + "**ID: **" + level[1] + Environment.NewLine + "**Author: **" + authorinfo[1] + Environment.NewLine + "**Song: **" + song + Environment.NewLine + "**Difficulty: **" + difficulty + " " + level[27] + "\\*" + Environment.NewLine + "**Downloads: **" + level[13] + Environment.NewLine + "**Likes: **" + level[19]);
                    }
                });
            cservice.CreateCommand("isup")
                 .Do(async (a) =>
                 {
                    await a.Channel.SendMessage("CvoltonBot is ONLINE!");
                  });
            cservice.CreateCommand("links")
                 .Do(async (a) =>
                 {
                     await a.Channel.SendMessage(System.IO.File.ReadAllText(@"D:\dropbox\dropbox\links.txt"));
                 });
            cservice.CreateCommand("download")
                 .Do(async (a) =>
                 {
                     await a.Channel.SendMessage(System.IO.File.ReadAllText(@"D:\dropbox\dropbox\latest.txt"));
                 });
            cservice.CreateCommand("songlist")
                .Parameter("page", ParameterType.Unparsed)
                .Do(async (a) =>
                {
                    await a.Channel.SendMessage(htmlGet("http://cvoltongdps.altervista.org/tools/songListBot.php?page="+a.GetArg("page")));
                });
            cservice.CreateCommand("whorated")
                .Parameter("lv", ParameterType.Unparsed)
                .Do(async (a) =>
                {
                    await a.Channel.SendMessage(htmlGet("http://cvoltongdps.altervista.org/tools/whoRatedBot.php?level=" + a.GetArg("lv")));
                });
            cservice.CreateCommand("player")
                .Description("Shows player's statistics")
                .Parameter("player", ParameterType.Unparsed)
                .Do(async (a) =>
                {
                    await a.Channel.SendMessage(htmlGet("http://cvoltongdps.altervista.org/tools/playerStatsBot.php?player=" + a.GetArg("player")));
                });
            cservice.CreateCommand("top")
                .Description("An alternative to the in-game leaderboards")
                .Parameter("type", ParameterType.Optional)
                .Parameter("page", ParameterType.Optional)
                .Do(async (a) =>
                {
                    await a.Channel.SendMessage(htmlGet("http://cvoltongdps.altervista.org/tools/leaderboardsBot.php?type=" + a.GetArg("type")+"&page="+a.GetArg("page")));
                });
            cservice.CreateCommand("mods")
                .Description("Shows mod statistics")
                .Do(async (a) =>
                {
                    await a.Channel.SendMessage(htmlGet("http://cvoltongdps.altervista.org/tools/modActionsBot.php"));
                });
            cservice.CreateCommand("21files")
                .Description("Displays a message about generateHash.php")
                .Parameter("page", ParameterType.Unparsed)
                .Do(async (a) =>
                {
                    await a.Channel.SendMessage("generateHash.php (the one required file for 2.1 GDPSes) will eventually be published 'soon'");
                });
            cservice.CreateCommand("songreup")
                .Description("Queues a YouTube song for reuploading onto CvoltonGDPS")
                .Parameter("song", ParameterType.Unparsed)
                .Do(async (a) =>
                {
                    string link = a.GetArg("song");
                    await a.Channel.SendMessage(VidDwnld(link));
                    
                });
            cservice.CreateCommand("time")
                .Description("Shows current date and time for Cvolton (UTC+1)")
                .Do(async (a) =>
                {
                    await a.Channel.SendMessage(DateTime.Now.ToString());

                });
            cservice.CreateCommand("debuglvl")
                .Description("Shows the raw unprocessed information about a level")
                .Parameter("level", ParameterType.Unparsed)
                .Do(async (a) =>
                {
                    using (var client = new HttpClient())
                    {
                        var values = new Dictionary<string, string>
                    {
                                    { "type", "0" },
                                    {"diff", "-"},
                                    {"len", "-" },
                                    { "str", a.GetArg("level") }
                    };

                        var content = new FormUrlEncodedContent(values);

                        var response = await client.PostAsync("http://cvoltongdps.altervista.org/getGJLevels20.php", content);

                        var responseString = await response.Content.ReadAsStringAsync();
            
                        await a.Channel.SendMessage(responseString);
                    }
                });
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{ e.Severity}] [{e.Source}] {e.Message}");
        }

        public string VidDwnld(string link)
        {
            try
            {
                var youTube = YouTube.Default; // starting point for YouTube actions
                var video = youTube.GetVideo(link); // gets a Video object with info about the video
                string filename = @"D:\dropbox\Dropbox\autogdsong\" + video.FullName;
                File.WriteAllBytes(filename, video.GetBytes());
                /*IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
                VideoInfo video = videoInfos
                .First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }
                var videoDownloader = new VideoDownloader(video, Path.Combine(@"D:\dropbox\Dropbox\autogdsong\", video.Title + video.VideoExtension));
                videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
                videoDownloader.Execute();*/
                return "Song queued for reupload: " + link;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return "Queuing the song failed...";
            }
        }

        public string htmlGet(string website)
        {
            string urlAddress = website;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
                return data;
            }
            return "";
        }
    }
}
