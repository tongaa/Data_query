using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.ViewModel
{
    public class RegisterDisplayParaInfo:INotifyPropertyChanged
    {
        private string displayParaInfo;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public string DisplayParaInfo
            {
                get { return displayParaInfo; }
                set
                {
                displayParaInfo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DisplayParaInfo"));
            }
        }

        public RegisterDisplayParaInfo(string DisplayParaInfo)
        {
            displayParaInfo = DisplayParaInfo;
        }



    }
}
