using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TareaTablas {
	public partial class ViewController : UIViewController, IUITableViewDelegate,IUITableViewDataSource {

        int numVeces = 1;
        List<string> lstOperaciones;
		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            TableView.DataSource = this;
            TableView.Delegate = this;
            lstOperaciones= new List<string>();
            lstOperaciones.Add("0 * 0 = 0");
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}


		#region Data Source

		[Export ("numberOfSectionsInTableView:")]
		public nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}
		public nint RowsInSection (UITableView tableView, nint section)
		{
            return numVeces;
		}

		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("TableCell", indexPath);
            cell.TextLabel.Text = lstOperaciones[indexPath.Row];

			return cell;
		}

		#endregion

		partial void BtnMostrar_Click (NSObject sender)
		{
            InvokeOnMainThread(() => {
                showActionSheet();
            });
            
           
		}
		void showActionSheet ()
		{
            
            lstOperaciones.Clear();
			var alert = UIAlertController.Create (null, null, UIAlertControllerStyle.ActionSheet);
			for (int i = 0; i < 20; i++) {
                alert.AddAction (UIAlertAction.Create ($"{i}", UIAlertActionStyle.Default, PrepareOperation));
			}
			alert.AddAction (UIAlertAction.Create ("Cancelar", UIAlertActionStyle.Cancel, null));
			PresentViewController (alert, true, null);
		}
        void PrepareOperation (UIAlertAction obj){
            var alert = UIAlertController.Create("", "Para continuar inserte el numero a multiplicar, si se inserta un numero invalido, se tomara como un 0.", UIAlertControllerStyle.Alert);

            UITextField field = new UITextField();
            alert.AddTextField((textField) => {
                alert.AddAction(UIAlertAction.Create($"OK", UIAlertActionStyle.Default, delegate {
                    
                    try
                    {
                        numVeces = int.Parse(obj.Title);
                        var numeroTextfield =  int.Parse(alert.TextFields[0].Text);
                        var res = 0;
                        for (int i = 0; i < numVeces; i++)
                        {
                            res = i* numeroTextfield;
                            lstOperaciones.Add($"{i} * {numeroTextfield} = {res}");
                        }
                        TableView.ReloadData();

                    }
                    catch (Exception ex)
                    {
                        for (int i = 0; i < numVeces; i++)
                        {
                            lstOperaciones.Add($"{i} * 0 = 0");

                        }
                        TableView.ReloadData();
                    }

                }));
                field = textField;

                field.Placeholder = "ejemplo: 1";
                field.Text = "";
                field.AutocorrectionType = UITextAutocorrectionType.No;
                field.KeyboardType = UIKeyboardType.Default;
                field.ReturnKeyType = UIReturnKeyType.Done;
                field.ClearButtonMode = UITextFieldViewMode.WhileEditing;

            });
            PresentViewController(alert, true, null);
		}
	}
}
