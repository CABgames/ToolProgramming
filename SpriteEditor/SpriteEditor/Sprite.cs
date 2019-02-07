//Sprite.cs

//Uses the following:
using System.Windows.Media.Imaging;

//Sprite class is essential for managing and getting various components of the sprites within the Sprite.

namespace SpriteEditor {
    class Sprite {

        //Get and set sprite directory.
        public string Directory {
            get;
            set;
        }

        //Get and set sprite name.
        public string Name {
            get;
            set;
        }

        //Get and set sprite image.
        public WriteableBitmap Image {
            get;
            set;
        }

        //Get and set sprite X axis.
        public double X {
            get;
            set;
        }

        //Get and set sprite Y axis.
        public double Y {
            get;
            set;
        }
    }
}
