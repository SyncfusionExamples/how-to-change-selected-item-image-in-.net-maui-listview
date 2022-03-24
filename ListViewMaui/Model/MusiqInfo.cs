using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class Musiqnfo : INotifyPropertyChanged
    {
        #region Fields

        private string songTitle;
        private string songAuther;
        private string songSize;
        private bool isSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates song title. 
        /// </summary>
        public string SongTitle
        {
            get
            {
                return songTitle;
            }
            set
            {
                songTitle = value;
                this.RaisePropertyChanged("SongTitle");
            }
        }

        /// <summary>
        /// Gets or sets the value that indicates the song auther.
        /// </summary>
        public string SongAuther
        {
            get
            {
                return songAuther;
            }
            set
            {
                songAuther = value;
                this.RaisePropertyChanged("SongAuther");
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates song size. 
        /// </summary>
        public string SongSize
        {
            get
            {
                return songSize;
            }
            set
            {
                songSize = value;
                this.RaisePropertyChanged("SongSize");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
