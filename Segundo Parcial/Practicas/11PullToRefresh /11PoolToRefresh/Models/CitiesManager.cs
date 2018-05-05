using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PoolToRefresh.Models {
    public class CitiesManager
    {
        #region Singleton
        //Just for NET 4+
        static readonly Lazy<CitiesManager> lazy = new Lazy<CitiesManager>(() => new CitiesManager());
        public static CitiesManager SharedInstance { get => lazy.Value; }
        #endregion

        #region Class Variables

        HttpClient httpClient;
        Dictionary<string, List<string>> cities;

        #endregion

        #region Events

        public event EventHandler<CitiesEventArgs> CitiesFetched;
        public event EventHandler<EventArgs> FetchCitiesFailed;

        #endregion

        #region Constructors
        //Only the singleton is allowed to instantiate this class
        CitiesManager()
        {
            httpClient = new HttpClient();
        }
        #endregion

        #region Public Functionality
        public Dictionary<string, List<string>> GetDefaultCities()
        {
            var citiesJson = File.ReadAllText("citites-incomplete.json");
             var objJSON = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
            return objJSON;


        }
        public void FetchCities()
        {
            System.Threading.Tasks.Task.Factory.StartNew(FetchCitiesAsync);

            async Task FetchCitiesAsync()
            {
                try
                {
                    if (CitiesFetched == null)
                        return;

                    var citiesJson = await httpClient.GetStringAsync("https://www.dropbox.com/s/0adq8yw6vd5r7bj/cities.sjon?dl=0");
                    cities = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);



                    var e = new CitiesEventArgs(cities);
                    CitiesFetched(this, e);


                    //Notify the controller that data is available.

                    //1.Events (Events/Delegate)
                    //2. Notifications (NSNotificationCenter)
                    //3. (Just in view controller) Unwind Segues
                }
                catch (Exception ex)
                {
                    if (FetchCitiesFailed == null)
                        return;

                    FetchCitiesFailed(this, new EventArgs());

                    //Notify the controller that something went wrong.
                    //1.Events (Events/Delegate)
                    //2. Notifications (NSNotificationCenter)
                    //3. (Just in view controller) Unwind Segues
                }
            }
        }
        #endregion


    }
    public class CitiesEventArgs : EventArgs
    {
        public Dictionary<string, List<string>> Cities { get; private set; }

        public CitiesEventArgs(Dictionary<string, List<string>> cities)
        {
            Cities = cities;

        }
    }

}
