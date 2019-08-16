using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.ViewModel
{
    class SatinfoName : INotifyPropertyChanged
    {
        private string satName;

      

        public event PropertyChangedEventHandler PropertyChanged;
        public string SatName
        {
        get { return satName; }
        set
        {
            satName = value;
            PropertyChanged(this, new PropertyChangedEventArgs("SatName"));
        }
    }

        public SatinfoName(string SatinfoName)
        {
            satName = SatinfoName;
        }
    }
}
