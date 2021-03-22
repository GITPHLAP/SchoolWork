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
using ConsoleBaggageHandlingSystem;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CreateFlightScheduleAndReservations;

namespace BaggageHandlingSystem
{
    public static class Helper
    {

        public static T FindChild<T>(this DependencyObject parent, string childName)
       where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var childType = child as T;
                if (childType == null)
                {
                    foundChild = Helper.FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }

    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO: Try to make ObservableCollection


        //TODO: Create PopupWindow

        SimulationManager manager = new SimulationManager();
        public MainWindow()
        {
            InitializeComponent();

            // Create Reservation and FlightSchedules 
            FileCreater.CreateBothFiles();


            manager.StartSimulation();

            manager.UpdateGates += Manager_UpdateGates;

            SplitterluggageListView.ItemsSource = new List<SortingSystem>() { manager.SortingSystem };


        }
        void ClosingApplication(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(1);
        }

        private void Manager_UpdateGates(object sender, EventArgs e)
        {
            //Encapsulat so when UI have time then its do it
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                SplitterluggageListView.Items.Refresh();
                GatesPanel.Items.Refresh();
                GateTabControl.Items.Refresh();

            }));
        }

        private void ShowSchedule_btn_Click(object sender, RoutedEventArgs e)
        {
            SimulationManager.Desks[0].MyList.Add(new Desk.BoxViewModel
                {
                    Brush = "Pink"
                });


        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    Method();
        //    var test = Helper.FindChild<ListView>((DependencyObject)sender, "gateListView");
        }

        private void gateListView_MouseEnter(object sender, MouseEventArgs e)
        {
            //((ListView)sender).ItemsSource = SimulationManager.Flightplans;
        }

        
        public static class VisualTreeHelperExtensions
        {
            

            public static T FindAncestor<T>(DependencyObject dependencyObject)
                where T : class
            {
                DependencyObject target = dependencyObject;
                do
                {
                    target = VisualTreeHelper.GetParent(target);
                }
                while (target != null && !(target is T));
                return target as T;
            }
        }
    }
}
