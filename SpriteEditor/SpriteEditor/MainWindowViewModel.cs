//MainWindowViewModel.cs

//By C.A.Bullock

//This class is using the following:
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpriteEditor {

    //MainWindowViewModel
    class MainWindowViewModel : INotifyPropertyChanged {

        //For when a property is changed.
        public event PropertyChangedEventHandler PropertyChanged;

        //SpriteList is set to ObjservableCollection sprite and sprite list collection changed is updated.
        public MainWindowViewModel() {

            SpriteList = new ObservableCollection<Sprite>();
            SpriteList.CollectionChanged += SpriteList_CollectionChanged;

        }

        public ObservableCollection<Sprite> SpriteList {

            get;
            private set;

        }

        //For when a property has been changed.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {

            if (PropertyChanged != null) {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }

        }

        //Used in order to notify of a change to the property of SpriteList.
        private void SpriteList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {

            NotifyPropertyChanged("SpriteList");

        }
    }
}
