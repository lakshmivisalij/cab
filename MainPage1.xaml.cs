using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI;
using MapTest.Common;
using Windows.UI.Popups;
using System.Diagnostics;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage1 : Page
    {
        public Library Library = new Library();
        public string distance;
        Database db;
        public object usrnm;
       
        string DriverName;
        string dristr;
        string path;
        SQLite.Net.SQLiteConnection conn;
        //  object distance;
        public MainPage1()
        {
            this.InitializeComponent();
            db = new Database();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDataBase.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

        }
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Geopoint point = await Library.Position();
                DependencyObject marker = Library.Marker();
                Display.Children.Add(marker);
                Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(marker, point);
                Windows.UI.Xaml.Controls.Maps.MapControl.SetNormalizedAnchorPoint(marker, new Point(0.5, 0.5));
                Display.ZoomLevel = 16;
                Display.Center = point;
            }
            catch
            {
                MessageDialog md = new MessageDialog("Please Ensure You Have Internet Connection");
                md.ShowAsync();
            }



        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.GetType().ToString() == "int")
            {
                var query2 = conn.Table<TransactionsTable>().Where(t => ((t.ID == Convert.ToInt16(usrnm.ToString()))));
                foreach(var item in query2)
                {
                    usrnm = item.UserName;
                }
                Debug.WriteLine(usrnm);
                Debug.WriteLine(query2);
                conn.Execute("UPDATE TransactionsTable SET UserName = ? Where ID= ?", usrnm, Convert.ToInt32( e.Parameter.ToString()));

            }
            else
            usrnm = e.Parameter.ToString();
        }

          
        
      
       private async void GetRouteAndDirections(object sender, TappedRoutedEventArgs e)
        {
            if(tbx1.Text =="" || tbx2.Text =="")
            {
                MessageDialog md1 = new MessageDialog("Please Enter Source and Destination!");
                await md1.ShowAsync();
            }
            var startAddressToGeocode = tbx1.Text;
            var endAddressToGeocode = tbx2.Text;
        

            var startLocationString = startAddressToGeocode;
            var endLocationString = endAddressToGeocode;
            //    List<string> startResults = new List<string>(); startResults.Add(startLocationString); rs.Add("Blue"); colors.Add("Green");
           
            var startResults = await MapLocationFinder.FindLocationsAsync(startLocationString, null);
            var endResults = await MapLocationFinder.FindLocationsAsync(endLocationString, null);
            var lat = 0.0;//= 28.6;
            var lon = 0.0;// 77.209;
            var endlat= 0.0;//= 28.6;
            var endlon = 0.0;// 77.209;

            
          if (startResults.Status == MapLocationFinderStatus.Success)
            {
                try
                {
                    lat = startResults.Locations[0].Point.Position.Latitude;
                    lon = startResults.Locations[0].Point.Position.Longitude;
                }
                catch
                {
                    MessageDialog md = new MessageDialog("Please Enter a Valid Source");
                    await md.ShowAsync();
                }
                 
             } 
            

            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = lat;
            startLocation.Longitude = lon;
            //   Geopoint hintPoint = new Geopoint(startLocation);
            //    Geopoint pt = new Geopoint()
           

            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(startAddressToGeocode,null);
            Geopoint startPoint = new Geopoint(startLocation);


           /* //check

            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.643;
            queryHint.Longitude = -122.131;
            Geopoint hintPoint = new Geopoint(queryHint);

            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeoCode, hintPoint, 3);
            */

            if (endResults.Status == MapLocationFinderStatus.Success)
            {
                try
                {
                    endlat = endResults.Locations[0].Point.Position.Latitude;
                    endlon = endResults.Locations[0].Point.Position.Longitude;
                }
                catch
                {
                    MessageDialog md = new MessageDialog("Please Enter a Valid Destination");
                    await md.ShowAsync();
                }
            }
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = endlat;
            endLocation.Longitude = endlon;
            Geopoint endHintPoint = new Geopoint(endLocation);
            MapLocationFinderResult endResult = await MapLocationFinder.FindLocationsAsync(endAddressToGeocode, null);
            Geopoint endPoint = new Geopoint(endLocation);



            /*  // End at the city of Seattle, Washington.
              BasicGeoposition endLocation = new BasicGeoposition();
              endLocation.Latitude = 28.6139;
              endLocation.Longitude = 70.2090;
              Geopoint endPoint = new Geopoint(endLocation);
              */

            // Get the route between the points.
            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);

          if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Display summary info about the route.
                distance = (routeResult.Route.LengthInMeters / 1000).ToString();
                tbOutputText.Inlines.Add(new Run()
                {
                    Text = "Total estimated time (minutes) = "
                        + routeResult.Route.EstimatedDuration.TotalMinutes.ToString()
                });
                tbOutputText.Inlines.Add(new LineBreak());
                tbOutputText.Inlines.Add(new Run()
                {
                    Text = "Total length (kilometers) = "
                        + (routeResult.Route.LengthInMeters / 1000).ToString()
                });
                tbOutputText.Inlines.Add(new LineBreak());
                tbOutputText.Inlines.Add(new LineBreak());
              
                // Display the directions.
                tbOutputText.Inlines.Add(new Run()
                {
                    Text = "DIRECTIONS"
                });
                tbOutputText.Inlines.Add(new LineBreak());

                foreach (MapRouteLeg leg in routeResult.Route.Legs)
                {
                    foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                    {
                        tbOutputText.Inlines.Add(new Run()
                        {
                            Text = maneuver.InstructionText
                        });
                        tbOutputText.Inlines.Add(new LineBreak());
                    }
                }
            }
            else
            {
                tbOutputText.Text =
                    "Please Give Proper Source and Destination: " + routeResult.Status.ToString();
            }
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                Display.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await Display.TrySetViewBoundsAsync(
                    routeResult.Route.BoundingBox,
                    null,
                    Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }

        }

        private async void btn1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MessageDialog showDialog = new MessageDialog("");
            try
            {
                int code = db.Transact(new Common.TransactionsTable()
                {
                    UserName = usrnm.ToString().Trim(),
                    Source = tbx1.Text.Trim(),
                    Destination = tbx2.Text.Trim(),
                    Date = System.DateTime.Today.ToString().Trim(),
                    CarType = "Yet To Book",
                    CabID = "AP 0H 1234",
                    DriverId = "ABCD",
                    DriverName = "Driver Name",
                    Fare = "000",
                    Distance = distance.ToString()
                });

                if (code == -1)
                {
                    showDialog.Content = "Transaction Failed!";
                    await showDialog.ShowAsync();
                }
                else
                {
                    showDialog.Content = "Please Proceed further to Book Your Cab and Driver!";
                    await showDialog.ShowAsync();
                    this.Frame.Navigate(typeof(BookCab), usrnm);
                }
                
            }
            catch (Exception ex)
            {
                showDialog.Content = "Please Get Route and Directions First!";
                await showDialog.ShowAsync();
            }

        }
    }
}
