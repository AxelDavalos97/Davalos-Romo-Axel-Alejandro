using System;

using Foundation;
using UIKit;

namespace ExamenParcial2.Views {
	public partial class TweetTableViewCell : UITableViewCell {
		public static readonly NSString Key = new NSString ("TweetTableViewCell");
		

		public String TweetText {
			get => txtTweet.Text;
			set => txtTweet.Text = value;
		}
		public UILabel TweetLabel {
			get;
			set;
		}
		public String RetweetedText {
			get => lblRetweeted.Text;
			set => lblRetweeted.Text = value;
		}
		public String FavoritedText {
			get => lblFavorited.Text;
			set =>  lblFavorited.Text = value;
		}
		public NSIndexPath IndexPath {
			get;
			set;
		}

		public TweetTableViewCell (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	}
}
