using System;

using UIKit;
using Photos;
using Foundation;
using AVFoundation;

namespace PhotoPicker {
	public partial class ViewController : UIViewController,IUIImagePickerControllerDelegate {
		#region Variables

		UITapGestureRecognizer profileTapGesture;
		UITapGestureRecognizer coverTapGesture;


		#endregion
		#region Constructors
		protected ViewController (IntPtr handle) : base (handle)
		{

			// Note: this .ctor should not contain any initialization logic.
		}
		#endregion
		#region Controller Life Cycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitializeComponents ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#endregion

		#region User Interactions
		void showActionSheet (UITapGestureRecognizer gesture)
		{

			var alert = UIAlertController.Create (null, null, UIAlertControllerStyle.ActionSheet);
			alert.AddAction (UIAlertAction.Create ("Camera", UIAlertActionStyle.Default, TryOpenCamera ));
			alert.AddAction (UIAlertAction.Create ("Library", UIAlertActionStyle.Default, TryOpenPhotolibrary));
			alert.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));
			PresentViewController (alert, true, null);
		}



		#endregion
		#region Camera
		void TryOpenCamera (UIAlertAction obj)
		{
			if (!UIImagePickerController.IsSourceTypeAvailable (UIImagePickerControllerSourceType.Camera))
			{
				InvokeOnMainThread (() => {
					showMessage ("Not Available", "The resource is not available, because this is a simulator", NavigationController);
				});
				return;
			}

			CheckCameraAuthorizationStatus (AVCaptureDevice.GetAuthorizationStatus (AVMediaType.Video));

		}
		private void CheckCameraAuthorizationStatus (AVAuthorizationStatus authorizationStatus)
		{
			switch (authorizationStatus) {
			case AVAuthorizationStatus.NotDetermined:
				break;
			case AVAuthorizationStatus.Restricted:
				InvokeOnMainThread (() => {
                        showMessage ("Not Available", "The resource is not available, it is restricted", NavigationController);
				});
				break;
			case AVAuthorizationStatus.Denied:
				InvokeOnMainThread (() => {
                        showMessage ("Not Available", "The resource is not available, it is denied by you", NavigationController);
				});
				break;
			case AVAuthorizationStatus.Authorized:
				InvokeOnMainThread (() => {
					var imagePickerController = new UIImagePickerController {
						SourceType = UIImagePickerControllerSourceType.Camera, Delegate = this
					};
					PresentViewController (imagePickerController, true, null);
				});
				break;
			default:
				break;
			}
		}
		#endregion
		#region Photo Library
		void TryOpenPhotolibrary (UIAlertAction obj)
		{
			if (!UIImagePickerController.IsSourceTypeAvailable (UIImagePickerControllerSourceType.PhotoLibrary)) {
				return;
			}
			CheckPhotoLibraryAuthorizationStatus (PHPhotoLibrary.AuthorizationStatus);
		}

		private void CheckPhotoLibraryAuthorizationStatus (PHAuthorizationStatus authorizationStatus)
		{
			switch (authorizationStatus) {
			case PHAuthorizationStatus.NotDetermined:
				// Vamos a pedir permiso para acceder a la galeria
				PHPhotoLibrary.RequestAuthorization (CheckPhotoLibraryAuthorizationStatus);
				break;
			case PHAuthorizationStatus.Restricted:
				InvokeOnMainThread (() => {
                        showMessage ("Not Available", "The resource is not available, it is restricted", NavigationController);
				});

				//Mostrar mensaje de restringido
				break;
			case PHAuthorizationStatus.Denied:
				InvokeOnMainThread (() => {
                        showMessage ("Not Available", "The resource is not available, it is denied by you", NavigationController);
				});
				//Mostrar un mensaje diciendo que el usuario denego 
				break;
			case PHAuthorizationStatus.Authorized:
				InvokeOnMainThread (() => {
					var imagePickerController = new UIImagePickerController {
						SourceType = UIImagePickerControllerSourceType.PhotoLibrary,Delegate=this
					};
					PresentViewController (imagePickerController, true, null);
				});
				break;
			default:
				break;
			}
		}

		#endregion


		#region Camera

		#endregion
		#region Internal Functionality

		void InitializeComponents ()
		{
			lblEdit.Hidden = lblCover.Hidden = true;

			profileTapGesture = new UITapGestureRecognizer (showActionSheet) { Enabled =true };
			smallView.AddGestureRecognizer (profileTapGesture);

			coverTapGesture = new UITapGestureRecognizer (showActionSheet) { Enabled = true };
			bigView.AddGestureRecognizer (coverTapGesture);
		}

		void showMessage(string title, string message, UIViewController fromViewController)
		{
			var alert = UIAlertController.Create (title, message, UIAlertControllerStyle.Alert);
			alert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
			fromViewController.PresentViewController(alert,true,null);
		}


		#endregion

		#region UIImage Picker Controller Delegate

		[Export ("imagePickerController:didFinishPickingMediaWithInfo:")]
		public void FinishedPickingMedia (UIImagePickerController picker, NSDictionary info)
		{
			var image = info [UIImagePickerController.OriginalImage] as UIImage;
			imgSmall.Image = image;
			picker.DismissViewController (true,null);
		}

		[Export ("imagePickerControllerDidCancel:")]
		public void Canceled (UIImagePickerController picker)
		{
			picker.DismissViewController (true, null);
		}


		#endregion
	}
}
