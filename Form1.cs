using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WebPageYouTubeLinkPlayer
{
    public partial class Form1 : Form
    {
        //static string API_URL = "https://uploadbeta.com/api/video/?cached&video=+";
        static string API_URL = "https://steakovercooked.com/api/video/?cached&lang=en&hash=9ba2e3febe606996f51ce04c1d6ce289&video=";
        public Form1()
        {
            InitializeComponent();
            WMP.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(WMP_PlayStateChange);//將AxWindowsMediaPlayer加入狀態監聽器
            //status = Status.OK;
            //MEP = new MediaEndPlay(_MediaEndPlay);
            progressBar1.Visible = false;
            StatusView.Text = "就緒";
            GetYouTubeUrl.form = this;
        }

        //enum Status {OK,LODING}//播放器所有狀態
        //Status status = new Status();//播放器狀態

        List<VideoURL> PlayList = new List<VideoURL>();
        LinkedHashSet<VideoURL> LHS = new LinkedHashSet<VideoURL>();
        int Index = 0;
        //MediaEndPlay MEP;

        private class VideoURL
        {
            public string URL;//影片路徑
            public bool isChanged;//判斷影片路徑是否已經經過轉換
            public override bool Equals(object obj)
            {
                if (obj is VideoURL)
                    return this.URL == (obj as VideoURL).URL;
                else
                    return false;
            }
            public override int GetHashCode()
            {
                return URL.GetHashCode();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(KURL.Text,"(http[s]?|HTTP[S]?)://((www|WWW)\\.)?[\\w]+\\.[\\w]+\\.?([\\w-+&;=?/]+[\\.]?)*", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("必須是網址", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //string urlAddress = "https://uploadbeta.com/api/video/?cached&video=https://www.youtube.com/v/pgwWsTqeIx0";
            //VideoPlay(urlAddress);
            Enabled_Flase();
            Clear();
            StatusView.Text = "讀取中...";
            KYouTubeGet(KURL.Text);
        }
        static WebClient webClient = new WebClient();
        private async void KYouTubeGet(string url)//取得YouTube連結
        {
            progressBar1.Visible = true;
            string data = await webClient.DownloadStringTaskAsync(url);
            //string[] bataline = data.Split('\r');
            // Regex regex = new Regex("href=\"(https://www.youtube.com/watch\\?v=|https://youtu.be/)[\\w-+&;=\\.]+\"", RegexOptions.IgnoreCase);//建立查找連結的匹配字串
            GetYouTubeUrl.data = data;
            /*Task T = new Task(GetYouTubeUrl.getYouTubeUrl);
            T.Start();*/
            //await Task.Run(() => GetYouTubeUrl.getYouTubeUrl());//.NET 4.5 的 Task 類別新增了靜態方法 Run。
            new Thread(GetYouTubeUrl.getYouTubeUrl).Start();
        }
        private class GetYouTubeUrl
        {
            static public Form1 form;
            static public string data;
            delegate void OkFunction();
            static OkFunction OF = OK;
            static public void getYouTubeUrl()
            {
                Regex regex = new Regex("(http[s]?://www.youtube.com/(watch\\?v=|embed/)|http[s]?://youtu.be/)[\\w-+&;=\\.\\?]+(\\s|\")?", RegexOptions.IgnoreCase);//建立查找連結的匹配字串
                int count = 0;
                //foreach (string l in bataline)
                //{
                //Console.WriteLine(l);
                Match match = regex.Match(data/*l*/);//進行匹配
                while (match.Success)//將所有符合的連結儲存
                {
                    String Video = match.Value;
                    //Console.WriteLine("Log:"+Video.Length);
                    if (Regex.IsMatch(Video[Video.Length - 1].ToString(), "(\\s|\")"))
                        Video = Video.Substring(0, Video.Length - 1);//去除結尾的 "
                    VideoURL VVURL = new VideoURL() { URL = Video, isChanged = false };
                    if (Regex.IsMatch(VVURL.URL, "https://youtu.be/[\\w-+&;=\\.]+", RegexOptions.IgnoreCase))
                        form.KYouTubeGetReTry(VVURL);
                    form.LHS.Add(VVURL);
                    Console.WriteLine("Log:" + Video);
                    count++;
                    match=match.NextMatch();
                }
                //}
                form.PlayList = form.LHS.ToList();
                form.LHS.Clear();
                Console.WriteLine("Log:END 共{0}首", count);
                form.Invoke(OF);
            }
            static void OK()//抓取YouTube網址完成後以主執行續執行
            {
                foreach (VideoURL item in form.PlayList)
                    form.PlaysList.Items.Add(item.URL);
                form.progressBar1.Visible = false;
                if (form.PlayList.Count > 0)
                    form.PlaysList.SetSelected(form.Index, true);
                else
                {
                    form.StatusView.Text = string.Format("完成，但是找不到任何的YouTube連結");
                    form.Enabled_True();
                }
            }
        }

        /*
         *用於https://youtu.be這種縮短過的網址，需要取兩次才會取到影片路徑 
         */
        private void KYouTubeGetReTry(VideoURL url)
        {
            string data = webClient.DownloadString(API_URL + url.URL);
            dynamic json = JToken.Parse(data);
            url.URL = json.url;;
        }
        /**
         * 播放影片
         * url 影片路徑
         * */
        private async void VideoPlay(VideoURL url)
        {
            /*//因為當影片播完時，下一首是已經播過的影片時，從WMP_PlayStateChange的case 1切換下一首時就會載入失敗直接STOP，接著如果下一首又是已經播過的也一樣會載入失敗，然後狀態跳成就緒，原因他媽的完全不知道，只能忍痛放棄不需要一直請求伺服器的做法了。
             * if (!url.isChanged)
            {
                url.isChanged = true;
                WebClient webClient = new WebClient();
                string data = await webClient.DownloadStringTaskAsync("https://uploadbeta.com/api/video/?cached&video=+" + url.URL);
                dynamic json = JToken.Parse(data);
                url.URL = json.url;
            }
            WMP.URL=url.URL;*/
            //status = Status.OK;
            WebClient webClient = new WebClient();
            string data = await webClient.DownloadStringTaskAsync(API_URL + url.URL);
            dynamic json = JToken.Parse(data);
            WMP.URL = json.url;
            Enabled_True();
            StatusView.Text = string.Format("完成，共{0}首", PlayList.Count);
        }

        private void Clear()
        {
            Index = 0;
            PlaysList.Items.Clear();
            PlayList.Clear();
            WMP.URL = "";
        }

        private void Enabled_True()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            PlaysList.Enabled = true;
        }

        private void Enabled_Flase()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            PlaysList.Enabled = false;
        }

        private void PlaysList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (status == Status.LODING)
            //  return;
            //status = Status.LODING;
            Enabled_Flase();
            StatusView.Text = "載入中...";
            Index = PlaysList.SelectedIndex;
            VideoPlay(PlayList[Index]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Index>0)
                PlaysList.SetSelected(Index-1, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Index < PlayList.Count-1)
                PlaysList.SetSelected(Index+1, true);
        }

        private void WMP_Enter(object sender, EventArgs e){}
        /**
         *AxWindowsMediaPlayer狀態監聽
         * 說明:https://msdn.microsoft.com/zh-tw/library/windows/desktop/dd562460(v=vs.85).aspx
         */
        //bool MediaEnded = false;
        private void WMP_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState)
            {
                 case 1://播放停止
                    Console.WriteLine("下一首");
                    if (Index < PlayList.Count - 1)
                        //if (!MediaEnded)
                            PlaysList.SetSelected(Index + 1, true);
                        //else
                         //   this.Invoke(MEP);
                    break;
                case 3:
                    Console.WriteLine("播放");
                    break;
                case 8:
                    Console.WriteLine("播完了");
                    //MediaEnded = true;
                    //WMP.currentPlaylist.clear();
                    break;
            }
        }
        /*delegate void MediaEndPlay();
        void _MediaEndPlay()
        {
            Thread.Sleep(5000);
            MediaEnded = false;
            if (Index < PlayList.Count - 1)
                PlaysList.SetSelected(Index + 1, true);
        }*/
    }
}
