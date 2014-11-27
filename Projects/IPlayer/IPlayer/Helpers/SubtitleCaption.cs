//Prabakaran.A 
//MS Software Engineering (2011-2016) 
//VIT University - Chennai Campus 
//Tamilnadu State 
//India - 600 048 
//www.facebook.com/prabakaran1993 
using System;
using System.Linq;
using Windows.UI.Xaml.Media;

namespace IPlayer.Helpers
{
    using Microsoft.PlayerFramework;

    public static class SubtitleCaption
    {

        public static void ConvertSrtToTimlineMarker(string srt, MediaPlayer me)
        {

            TimelineMarker timelineMarker = new TimelineMarker();
            var pattern = new string[1] {"\r\n\r\n"};
            var shortPattern = new string[1] { "\r\n" };
            var timeSplitter = new string[1] { " --> " };
            var srtSplit = srt.Split(pattern, StringSplitOptions.None);

            foreach (string s in srtSplit)
            {
                try
                {
                    var lines = s.Split(shortPattern, StringSplitOptions.None);
                    var timeStrings = lines[1].Split(timeSplitter, StringSplitOptions.None);
                    var start = timeStrings[0];
                    var end = timeStrings[1];
                    var text = String.Join(Environment.NewLine, lines.Skip(2));

                    TimelineMarker tlm = new TimelineMarker();
                    tlm.Time = TimeSpan.Parse(start.Replace(',', '.'));
                    tlm.Text = text;
                    me.Markers.Add(tlm);
                    TimelineMarker tlEnd = new TimelineMarker();
                    tlEnd.Time = TimeSpan.Parse(end.Replace(',', '.'));
                    tlEnd.Text = "";
                    me.Markers.Add(tlEnd);
                }
                catch { }
            }
        }


        ////public static void SimulateSubTitles(MediaElement mediaElement)
        ////{
        ////    var medLength = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        ////     Add a 3 second caption of text every 5 seconds.
        ////    for (double secs = 0; secs <= medLength; secs += 5)
        ////    {
        ////        mediaElement.Markers.Add(new TimelineMarker { Text = "Caption Time: " + secs.ToString(), Time = TimeSpan.FromSeconds(secs) });
        ////        mediaElement.Markers.Add(new TimelineMarker { Text = "", Time = TimeSpan.FromSeconds(secs+3) });
        ////    }

        ////}

    }

    
}
//Prabakaran.A 
//MS Software Engineering (2011-2016) 
//VIT University - Chennai Campus 
//Tamilnadu State 
//India - 600 048 
//www.facebook.com/prabakaran1993 