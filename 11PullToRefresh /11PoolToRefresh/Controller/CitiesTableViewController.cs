// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using PoolToRefresh.Models;
using System.Collections.Generic;

namespace PoolToRefresh
{
	public partial class CitiesTableViewController : UITableViewController {

        Dictionary<string, List<string>> listCiudades = CitiesManager.SharedInstance.GetDefaultCities();
        List<string> keys = new List<string>();
		#region Constructor
		public CitiesTableViewController (IntPtr handle) : base (handle)
		{
		}
		#endregion

		#region Controller Life Cycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            Console.WriteLine(listCiudades.Count);
            foreach (var key in listCiudades.Keys)
            {
                keys.Add(key);
            }
        }
		#endregion

		#region TableView Data Source
		public override nint NumberOfSections (UITableView tableView)
		{
            return listCiudades.Count;
		}
		public override string TitleForHeader(UITableView tableView, nint section)
		{
            return keys[(int)section];
		}
		public override nint RowsInSection (UITableView tableView, nint section)
		{
            return listCiudades[keys[(int)section]].Count;
		}

		
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

           // tableView.RegisterClassForCellReuse(typeof(UITableViewCell), "TableCell");
            var cell = tableView.DequeueReusableCell("TableCell", indexPath);
            cell.TextLabel.Text = $"{listCiudades[keys[indexPath.Section]][indexPath.Row]}";
            return cell;
        }
		#endregion
	}
}
