// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PhotoPicker
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIView bigView { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem btnEdit { get; set; }

		[Outlet]
		UIKit.UIImageView imgBig { get; set; }

		[Outlet]
		UIKit.UIImageView imgSmall { get; set; }

		[Outlet]
		UIKit.UILabel lblCover { get; set; }

		[Outlet]
		UIKit.UILabel lblEdit { get; set; }

		[Outlet]
		UIKit.UIView smallView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (smallView != null) {
				smallView.Dispose ();
				smallView = null;
			}

			if (bigView != null) {
				bigView.Dispose ();
				bigView = null;
			}

			if (btnEdit != null) {
				btnEdit.Dispose ();
				btnEdit = null;
			}

			if (imgSmall != null) {
				imgSmall.Dispose ();
				imgSmall = null;
			}

			if (lblEdit != null) {
				lblEdit.Dispose ();
				lblEdit = null;
			}

			if (imgBig != null) {
				imgBig.Dispose ();
				imgBig = null;
			}

			if (lblCover != null) {
				lblCover.Dispose ();
				lblCover = null;
			}
		}
	}
}
