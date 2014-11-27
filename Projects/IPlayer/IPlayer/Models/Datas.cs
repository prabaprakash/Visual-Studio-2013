using System;
using System.ComponentModel;
using Windows.Storage;

namespace IPlayer.Models
{
    public class playlist : INotifyPropertyChanged
    {
        public String _displayname;
        public String _contenttype;
        public String _datacreated;
        public Uri _thumburi;
        public StorageFile _mediafile;
        public String _album;
        ////public playlist(string name,string type,string date)
        ////{
        ////    this._displayname = name;
        ////    this._datacreated = date;
        ////    this._contenttype = type;
        ////}
        public String Album
        {
            get { return _album; }
            set { _album = value; }
        }
        public StorageFile MediaFile
        {
            get { return _mediafile; }
            set { _mediafile = value; }
        }
        public Uri ThumbUri
        {
            get
            {
                return _thumburi;
            }
            set
            {
                _thumburi = value;
            }
        }
        public String DisplayName
        {
            get { return _displayname; }
            set
            {
                _displayname = value;
                // fireproperychanged(DisplayName);
            }
        }
        public String ContentType
        {
            get { return _contenttype; }
            set
            {
                _contenttype = value;
                //  fireproperychanged(ContentType);
            }
        }
        public String DateCreated
        {
            get { return _datacreated; }
            set
            {
                _datacreated = value;
                // fireproperychanged(DateCreated);
            }
        }

        //public string _date;
        //public string Date
        //{
        //    get { return _date; }
        //    set
        //    {
        //        _date = MediaFile.DateCreated.ToString();
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        void fireproperychanged(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }


    }
}
