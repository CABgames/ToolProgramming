//MainWindow.xaml.cs

//By C.A.Bullock

//This class is using the following:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace SpriteEditor {
    /// This class is responsible for a number of tasks in the sprite editor such as the editing, saving, and opening tools.
    public partial class MainWindow : Window {

        //The sprite editable image panel.
        WriteableBitmap backBuffer;
        //public int for the sizeValue of the brush.
        public int sizeValue = 10;
        //Private bool for if the mouse is down.
        private bool bMouseIsDown;
        private MainWindowViewModel viewModel = new MainWindowViewModel();
        private Sprite currentLayer;
        currentTool selectedTool = currentTool.PENCILTOOL;
        currentColor selectedColor = currentColor.BLACK;

        //Enum for the different tool states 
        public enum currentTool {

            SELECTAREA,
            FILLTOOL,
            PENCILTOOL,
            ERASERTOOL

        }

        //Enum for the different color states which can be used in the program.
        public enum currentColor {

            BLACK,
            GREY,
            DARKGREY,
            LIGHTBLUE,
            BLUE,
            RED,
            DARKRED,
            PALEVIOLETRED,
            PURPLE,
            ORANGE,
            DARKORANGE,
            LIGHTGREEN,
            GREEN,
            CORAL,
            FLORALWHITE,
            ORANGERED

        }



        public MainWindow() {

            InitializeComponent();
            this.DataContext = viewModel;

            Loaded += delegate {

                backBuffer = BitmapFactory.New((int)imgBorder.ActualWidth, (int)imgBorder.ActualHeight);

                UpdateBitmap();

            };
        }

        //This method is used to update sprites adding them to the backBuffer where they are displayable and editable by users and also displays them as layers.
        private void UpdateBitmap() {

            backBuffer.Clear(Colors.Transparent);

            //Foreach of the sprites in the viewModel spriteList.
            foreach(Sprite sprite in viewModel.SpriteList) {

                if (sprite.Image != null) {

                    backBuffer.Blit(
                        new Rect(sprite.X, sprite.Y, sprite.Image.Width, sprite.Image.Height),
                        sprite.Image,
                        new Rect(0, 0, sprite.Image.Width, sprite.Image.Height),
                        WriteableBitmapExtensions.BlendMode.None

                    );

                }
            }

            MainImage.Source = backBuffer;

        }

        //When mouse is up mouseIsDown is set to false.
        private void MainImage_MouseUp(object sender, MouseButtonEventArgs e) {

            bMouseIsDown = false;

        }

        //When mouse is down variable mouseIsDown is set to true.
        private void MainImage_MouseDown(object sender, MouseButtonEventArgs e) {

            bMouseIsDown = true;

        }
   
        //This is method used for things which can happen on the sprite editing image area.
        public void MainImage_MouseMove(object sender, MouseEventArgs e) {


            Point mousePosition = e.GetPosition(MainImage);
            mousePositionX.Text = " X " + Math.Round(mousePosition.X).ToString();
            mousePositionY.Text = " Y " +Math.Round(mousePosition.Y).ToString();
            CurrentSize.Text = "Current draw scale is " + sizeValue.ToString();
            //CurrentColor.Text = "Current color is " + selectedColor.ToString;
    

            //If the mouse isn't down then leave this method.
            if (!bMouseIsDown) {

                return;

            }
            
            switch (selectedTool) {

                //This case is used for when selecting and moving a layer around the editable sprite screen.
                case currentTool.SELECTAREA:
                    {

                        double TemporaryX;
                        double TemporaryY;

                        //If the currentLayer selected is not null.
                        if (currentLayer != null) {

                            //Move the current layer to the image to the middle of the users position.
                            currentLayer.X = mousePosition.X - (currentLayer.Image.PixelWidth / 2);
                            currentLayer.Y = mousePosition.Y - (currentLayer.Image.PixelHeight / 2);
                            TemporaryX = mousePosition.X;
                            TemporaryY = mousePosition.Y;

                        }
                        else {

                        }


                        //Goes to update bitmap.
                        UpdateBitmap();
                        break;

                    }

                //This case is used for when filling the editable sprite screen with a solid color.
                case currentTool.FILLTOOL: {

                        //Gets the X axis size based upon the draw size scale.
                        for (int x = ((int)mousePosition.X - 400); x < ((int)mousePosition.X + 400); x++) {

                            //Gets the Y axis size based upon the draw size scale.
                            for (int y = ((int)mousePosition.Y - 400); y < ((int)mousePosition.Y + 400); y++) {

                                //Switch statement for the various selected colors.
                                switch (selectedColor) {

                                    //Case for if the current color is set to black.
                                    case currentColor.BLACK: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Black);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to grey. 
                                    case currentColor.GREY:
                                        {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Gray);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to dark grey. 
                                    case currentColor.DARKGREY: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.DarkGray);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to light blue.
                                    case currentColor.LIGHTBLUE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.LightBlue);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to blue.
                                    case currentColor.BLUE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Blue);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to red.
                                    case currentColor.RED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Red);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to dark red.
                                    case currentColor.DARKRED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.DarkRed);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to pale violet red.
                                    case currentColor.PALEVIOLETRED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.PaleVioletRed);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to purple.
                                    case currentColor.PURPLE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Purple);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to orange.
                                    case currentColor.ORANGE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Orange);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to dark orange.
                                    case currentColor.DARKORANGE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.DarkOrange);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to light green.
                                    case currentColor.LIGHTGREEN: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400))
                                            {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.LightGreen);

                                            }
                                            else
                                            {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to green.
                                    case currentColor.GREEN: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Green);

                                            }
                                            else {

                                            }
                                            break;

                                        }

                                    //Case for if the current color is set to coral.
                                    case currentColor.CORAL: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Coral);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to floral white.
                                    case currentColor.FLORALWHITE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.FloralWhite);

                                            }
                                            else {

                                            }

                                            break;

                                        }

                                    //Case for if the current color is set to orange red. 
                                    case currentColor.ORANGERED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.OrangeRed);

                                            }
                                            else {



                                            }

                                            break;

                                        }
                                }
                            }
                        }

                        break;

                    }

                //This case is used for when the user wishes to draw upon the editable sprite screen, users can change the color and size of the pencil tool from other parts of the program which effect the pencil overall.
                case currentTool.PENCILTOOL: {

                        //Gets the X axis size based upon the draw size scale.
                        for (int x = ((int)mousePosition.X - sizeValue); x < ((int)mousePosition.X + sizeValue); x++ ) {

                            //Gets the Y axis size based upon the draw size scale.
                            for (int y = ((int)mousePosition.Y - sizeValue); y < ((int)mousePosition.Y + sizeValue); y++) {

                                //Switch statement for the various selected colors.
                                switch (selectedColor) {

                                    //Case for if the current color is set to black.
                                    case currentColor.BLACK: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int) x, (int) y, Colors.Black);

                                            }
                                            else {
                                                    
                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to grey. 
                                    case currentColor.GREY: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Gray);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to dark grey. 
                                    case currentColor.DARKGREY: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.DarkGray);

                                            }
                                            else {
                                                
                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to light blue.
                                    case currentColor.LIGHTBLUE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.LightBlue);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to blue.
                                    case currentColor.BLUE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Blue);

                                            }
                                            else {
      
                                            }

                                            break;
                                            
                                    }

                                    //Case for if the current color is set to red.
                                    case currentColor.RED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Red);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to dark red.
                                    case currentColor.DARKRED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.DarkRed);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to pale violet red.
                                    case currentColor.PALEVIOLETRED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.PaleVioletRed);

                                            }
                                            else {
                                                
                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to purple.
                                    case currentColor.PURPLE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Purple);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to orange.
                                    case currentColor.ORANGE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Orange);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to dark orange.
                                    case currentColor.DARKORANGE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.DarkOrange);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to light green.
                                    case currentColor.LIGHTGREEN: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.LightGreen);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to green.
                                    case currentColor.GREEN: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Green);

                                            }
                                            else {
                                                
                                            }
                                            break;

                                    }

                                    //Case for if the current color is set to coral.
                                    case currentColor.CORAL: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.Coral);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to floral white.
                                    case currentColor.FLORALWHITE: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)) {

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.FloralWhite);

                                            }
                                            else {

                                            }

                                            break;

                                    }

                                    //Case for if the current color is set to orange red. 
                                    case currentColor.ORANGERED: {

                                            //If cursor position is within the defined parameters.
                                            if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400)){

                                                //Draw in the defined location in the set color.
                                                backBuffer.SetPixel((int)x, (int)y, Colors.OrangeRed);

                                            }
                                            else{



                                            }

                                            break;

                                        }
                                }
                            }
                        }

                        break;

                }

                //This case is used for when erasing and removing specified visible editable parts on the sprite screen.
                case currentTool.ERASERTOOL: {

                        for (int x = ((int)mousePosition.X - sizeValue); x < ((int)mousePosition.X + sizeValue); x++)
                        {

                            for (int y = ((int)mousePosition.Y - sizeValue); y < ((int)mousePosition.Y + sizeValue); y++)
                            {
                               
                                if (!(x <= 0 || x >= 400 || y <= 0 || y >= 400))
                                {

                                    backBuffer.SetPixel((int)x, (int)y, Colors.Transparent);

                                }
                                else
                                {



                                }

                            }
                           
                        }

                        break;

                    }

            }


        }

        private static currentTool GetSELECTAREA()
        {
            return currentTool.SELECTAREA;
        }

        //This function displays an open image file dialog to the user to which the user can either choose a suitable image (which when added will become an image layer) or cancel and continue using the program as before.
        private void menuFile_Open(object sender, RoutedEventArgs e)
        {

            bool bResize = true;
            Sprite sprite = new Sprite();
            //Displays open file dialog to the user.
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Please select an image you wish to open";
            op.Filter = "The following formats are supported|*.jpg;*.jpeg;*.png;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "Bitmap file format (*.bmp)|*.bmp";

            //Converts the opened image to to one of the programs sprite image's, 
            if (op.ShowDialog() == true)
            {

                sprite.Image = BitmapFactory.ConvertToPbgra32Format(new BitmapImage(new Uri(op.FileName)));

            }

            //Sprites from the opened file.
            sprite.Directory = op.FileName;
            //This sets bitmapname to the Directory, without the directories, and without the extension.
            sprite.Name = op.FileName.Split(System.IO.Path.DirectorySeparatorChar).Last().Split('.').First();

            //This part of the code checks the image isn't blank and then if it is not blank but larger than the display image it will aim to scale it a fair bit smaller than the display image. 
            if (sprite.Image != null)
            {

                while (bResize == true)
                {

                    //If the sprite needs is too big and requires resizing do this.
                    if (sprite.Image.PixelWidth > (int)imgBorder.ActualWidth || sprite.Image.PixelHeight > (int)imgBorder.ActualHeight)
                    {

                        sprite.Image = sprite.Image.Resize((int)(sprite.Image.PixelWidth * 0.75f), (int)(sprite.Image.PixelHeight * 0.75), WriteableBitmapExtensions.Interpolation.NearestNeighbor);

                    }
                    //Otherwise do this.
                    else
                    {

                        bResize = false;

                    }

                }

                sprite.Image = sprite.Image.Resize((int)(sprite.Image.PixelWidth * 0.75f), (int)(sprite.Image.PixelHeight * 0.75f), WriteableBitmapExtensions.Interpolation.NearestNeighbor);

            }

            //Add new sprite to the sprite list and then update the bitmap.
            viewModel.SpriteList.Add(sprite);
            UpdateBitmap();

        }

        //When the users saves the file the following happens.
        private void menuFile_Save(object sender, RoutedEventArgs e)
        {

            //A save menu/dialog appears to the user requesting they save it and including option to name the file and choose save location however can only be saved as bmp image file.
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Please save your image";
            save.Filter = "Bitmap file format (*.bmp)|*.bmp";

            //Image which is save is a close of the current edited user image.
            if (save.ShowDialog() == true)
            {

                SaveBitmap(save.FileName, backBuffer.Clone());

            }

        }

        //When new clicked by user the sprite images in the SpriteList are cleared and the bitmap updated leaving it blank.
        private void menuFile_New(object sender, RoutedEventArgs e)
        {

            viewModel.SpriteList.Clear();
            UpdateBitmap();

        }

        //This simply exits the application completley.
        private void menuFile_Exit(object sender, RoutedEventArgs e)
        {

            Environment.Exit(0);

        }

        //This is the medthod which correctly saves the image.
        void SaveBitmap (string filename, BitmapSource image)
        {

            if (filename != string.Empty)
            {

                using (FileStream stream = new FileStream(filename, FileMode.Create)) 
                {

                    PngBitmapEncoder bitmap = new PngBitmapEncoder();
                    bitmap.Frames.Add(BitmapFrame.Create(image));
                    bitmap.Save(stream);

                }

            }

        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            
            if (!(sizeValue < 0)) {

                sizeValue = sizeValue -2;    

            }

        }

        private void btnTwo_Click(object sender, RoutedEventArgs e) {

            if (!(sizeValue > 50)) {

                sizeValue = sizeValue +2;

            }

        }

        private void btnThree_Click(object sender, RoutedEventArgs e) {

            selectedTool = currentTool.FILLTOOL;

        }

        void btnFour_Click(object sender, RoutedEventArgs e) {
            Console.Write("");
            selectedTool = currentTool.SELECTAREA;

        }

        private void btnFive_Click(object sender, RoutedEventArgs e) {

            selectedTool = currentTool.PENCILTOOL;

        }

        private void btnSix_Click(object sender, RoutedEventArgs e) {
            
            selectedTool = currentTool.ERASERTOOL;

        }

        //When this color is selected in the listbox in the program the current color becomes this color. 
        private void clrBlack_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.BLACK;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrGrey_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.GREY;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrDarkGrey_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.DARKRED;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrLightBlue_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.LIGHTBLUE;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrBlue_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.BLUE;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrRed_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.RED;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrDarkRed_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.DARKRED;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrPaleVioletRed_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.PALEVIOLETRED;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrPurple_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.PURPLE;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrOrange_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.ORANGE;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrDarkOrange_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.DARKORANGE;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrLightGreen_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.LIGHTGREEN ;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrGreen_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.GREEN;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrCoral_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.CORAL;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrFloralWhite_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.FLORALWHITE;

        }

        //When this color is selected in the listbox in the program the current color becomes this color.
        private void clrOrangeRed_click(object sender, RoutedEventArgs e) {

            selectedColor = currentColor.ORANGERED;

        }

        private void LayerView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {

            currentLayer = viewModel.SpriteList[LayerViewer.SelectedIndex];

        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {



        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {



        }

        private void lstSpriteSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



        }


        private void ListBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {

        }
    }

}
