// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ExamenParcial2.Views
{
	[Register ("TweetTableViewCell")]
	partial class TweetTableViewCell 
	{
		[Outlet]
		UIKit.UILabel lblFavorited { get; set; }

		[Outlet]
		UIKit.UILabel lblRetweeted { get; set; }

		[Outlet]
		UIKit.UILabel txtTweet { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtTweet != null) {
				txtTweet.Dispose ();
				txtTweet = null;
			}

			if (lblFavorited != null) {
				lblFavorited.Dispose ();
				lblFavorited = null;
			}

			if (lblRetweeted != null) {
				lblRetweeted.Dispose ();
				lblRetweeted = null;
			}
		}
	}
}
