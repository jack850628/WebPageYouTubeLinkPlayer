# WebPageYouTubeLinkPlayer
抓取網頁上的YouTube連結並自動依序播放
![Alt text](https://raw.githubusercontent.com/jack850628/WebPageYouTubeLinkPlayer/master/demo.jpg)

使用C#中webClient.DownloadStringTaskAsync函數抓取網頁內容，再用正規表示法抓取網頁中YouTube連結，
然後再用別人提供的免費Web API取得YouTube影片內容，再用C#中提供的Windows Media Player進行播放。
