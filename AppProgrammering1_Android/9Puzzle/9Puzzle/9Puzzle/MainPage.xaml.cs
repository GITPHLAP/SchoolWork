using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _9Puzzle
{
    public partial class MainPage : ContentPage
    {
        //Solver: https://alexyuisingwu.github.io/sliding-puzzle-solver/ OR https://github.com/YahyaAlaaMassoud/Sliding-Puzzle-A-Star-Solver

        private List<string> imageList = new List<string>();
        private static Random random = new Random();
        private static string firstChar = "H";
        private static int tiles = 9;
        private static int rowsNColumns = (int)Math.Sqrt(tiles);

        public MainPage()
        {
            SetImageList();
            InitializeComponent();
            Shuffle();
        }

        /// <summary>
        /// Copy the image to datapackage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            //var test = puzzleGrid;
            Image image = (sender as Element).Parent as Image;
            e.Data.Properties.Add("Source", image);
        }

        /// <summary>
        /// Copy the image to the new tile and remove it from the old
        /// <br/>
        /// <see cref="IsSolved"/>
        /// <br/>
        /// <seealso cref="Won"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDrop(object sender, DropEventArgs e)
        {
            Image thisImage = (sender as Element).Parent as Image;

            Image oldImage = (Image)e.Data.Properties["Source"];

            thisImage.Source = oldImage.Source;

            oldImage.Source = ImageSource.FromFile(null);

            if (IsSolved())
            {
                Won();
            }
        }

        /// <summary>
        /// According to 
        /// <seealso cref="CheckIfMoveIsValid(Image, Image)"/>
        /// its determ what happend when drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
        {
            Image newImage = (sender as Element).Parent as Image;
            Image oldImage = (Image)e.Data.Properties["Source"];


            if (((FileImageSource)newImage.Source).File == null && CheckIfMoveIsValid(oldImage, newImage))
            {
                e.AcceptedOperation = DataPackageOperation.Copy;
                return;
            }

            e.AcceptedOperation = DataPackageOperation.None;

        }
        
        /// <summary>
        /// Check if the move is valid 
        /// </summary>
        /// <param name="oldImage">Tile we want to move</param>
        /// <param name="newImage">The new position for the tile</param>
        /// <returns> true or false</returns>
        private bool CheckIfMoveIsValid(Image oldImage, Image newImage)
        {
            int nImgColumn = Grid.GetColumn(newImage);
            int nImgRow = Grid.GetRow(newImage);

            int oImgColumn = Grid.GetColumn(oldImage);
            int oImgRow = Grid.GetRow(oldImage);

            int difColumns = nImgColumn - oImgColumn;
            int difRows = nImgRow - oImgRow;

            if ((difColumns == 0 && difRows == 1 || difRows == - 1) || //if columns havent change and then rows have change with 1 or -1
                (difRows == 0 && difColumns == 1 || difColumns == -1)) //if rows havent change and then columns have change with 1 or -1
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// This method shuffle the tiles in board using:
        /// <br/>
        /// <see cref="GetRandomSolvableIndexes"/>
        /// <br/>
        /// <seealso cref="AddRandomImagesToGrid(int[])"/>
        /// </summary>
        private void Shuffle()
        {
            puzzleGrid.Children.Clear();

            int[] randomImgIndexes = GetRandomSolvableIndexes();

            AddRandomImagesToGrid(randomImgIndexes);
        }

        /// <summary>
        /// This method add images to the grid with the indexes from <paramref name="randomImgIndexes"/>
        /// </summary>
        /// <param name="randomImgIndexes">An array with random indexes for image list </param>
        private void AddRandomImagesToGrid(int[] randomImgIndexes)
        {
            int index = 0;
            for (int i = 0; i < rowsNColumns; i++)
            {
                for (int j = 0; j < rowsNColumns; j++)
                {
                    //Drag and drop gesture
                    DragGestureRecognizer dragGesture = new DragGestureRecognizer();
                    dragGesture.DragStarting += OnDragStarting;

                    DropGestureRecognizer dropGesture = new DropGestureRecognizer();
                    dropGesture.Drop += OnDrop;
                    dropGesture.DragOver += DropGestureRecognizer_DragOver;


                    Image image = new Image
                    {
                        //if its the last index the image should be empty 
                        Source = ImageSource.FromFile(randomImgIndexes[index] == 8 ? null : imageList[randomImgIndexes[index]]) 
                    };

                    //Add the image to the puzzle grid
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    image.GestureRecognizers.Add(dragGesture);
                    image.GestureRecognizers.Add(dropGesture);

                    puzzleGrid.Children.Add(image);
                    index++;
                }
            }
        }

        /// <summary>
        /// Check if the board is solvable
        /// </summary>
        /// <param name="arr">1D array over the board</param>
        /// <returns>true or false</returns>
        private bool IsSolveable(int[] arr)
        {
            //When puzzle is 3*3 its just needed to know if the inversions is even 
            
            int inversions = 0;

            for (int i = 0; i < rowsNColumns*rowsNColumns-1; i++)
            {
                for (int j = i+1; j < rowsNColumns * rowsNColumns; j++)
                {
                    if ( arr[i]>arr[j])
                    {
                        inversions++;
                    }
                }
            }

            return inversions % 2 == 0;
        }

        /// <summary>
        /// This create the puzzleboard grid as a square
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            puzzleGrid.WidthRequest = width;
            puzzleGrid.HeightRequest = width;
        }

        /// <summary>
        /// Get an array with random indexes which is solvable in order:
        /// <br/>
        /// 1 2 3
        /// <br/>
        /// 4 5 6
        /// </summary>
        /// <returns>Int array with solvable board</returns>
        private int[] GetRandomSolvableIndexes()
        {
            List<int> randomIndexesArr = new List<int>();
            int randomIndex;
            do
            {
                randomIndexesArr.Clear();

                List<int> indexes = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

                for (int i = 0; i < rowsNColumns* rowsNColumns; i++)
                {
                    indexes.Remove(randomIndex = indexes[random.Next(indexes.Count)]);
                    randomIndexesArr.Add(randomIndex);
                }


            } while (!IsSolveable(randomIndexesArr.ToArray()));
            return randomIndexesArr.ToArray();
        }

        /// <summary>
        /// Assign the global imageList with the images
        /// <br/>
        /// Pattern for image is a letter and then a number and then .png
        /// e.g. H1.png
        /// </summary>
        private void SetImageList()
        {
            for (int i = 1; i <= tiles; i++)
            {
                imageList.Add($"{firstChar}{i}.png");
            }
        }

        private void RestartBtn_Clicked(object sender, EventArgs e)
        {
            Shuffle();
        }

        /// <summary>
        /// Set the missing tiles image source to the missing image piece
        /// </summary>
        private void Won()
        {
            //Get the last image element in board
            Image img9 = (Image)puzzleGrid.Children[tiles - 1];
            img9.Source = ImageSource.FromFile(imageList[tiles - 1]);
        }

        /// <summary>
        /// Check if the puzzle is solved
        /// </summary>
        /// <returns></returns>
        private bool IsSolved()
        {
            for (int i = 0; i < puzzleGrid.Children.Count; i++)
            {
                if (((Image)puzzleGrid.Children[i]).Source.ToString() != $"File: {imageList[i]}")
                {
                    return false;
                }
            }
            return true;
        }
    }
}
