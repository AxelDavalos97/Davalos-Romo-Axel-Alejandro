using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace ExamenParcial2.Models {
	public class Linq2TwitterManager {

		static readonly Lazy<Linq2TwitterManager> lazy = new Lazy<Linq2TwitterManager> (() => new Linq2TwitterManager ());
		public static Linq2TwitterManager SharedInstance { get => lazy.Value; }

		public event EventHandler<TweetsFetchedEventArgs> TweetsFetched;
		public event EventHandler<TweetsFetchedFailedEventArgs> FailedTweetsFetched;

		CancellationTokenSource cancellationTokenSource;
		SingleUserAuthorizer auth;
		TwitterContext twitterContext;
		Linq2TwitterManager ()
		{
			auth = new SingleUserAuthorizer {
				CredentialStore = new InMemoryCredentialStore {
					ConsumerKey = "4SrnUuH6tdHyWcI1NJuVhLb5h",
					ConsumerSecret = "xLki1TUQeEHsKDIQwQgXjtLQNGoU7Suaw7H1KXNrF2Alw52im4",
					OAuthToken = "2393451408-vB88YGg0UP9CQegSJQrwGLaJuxFJYkG8frfilC7",
					OAuthTokenSecret = "UkkoQ8nhwBUUidMQNTJPBEXTllhSkIH9b9GBM8zZyvq2m"
				}
			};
			twitterContext = new TwitterContext (auth);
			cancellationTokenSource = new CancellationTokenSource ();
		}
		public void SearchTweets (string query)
		{
			if (cancellationTokenSource.IsCancellationRequested)
				cancellationTokenSource.Cancel ();

			cancellationTokenSource = new CancellationTokenSource ();
			var cancellationToken = cancellationTokenSource.Token;

			Task.Factory.StartNew (async () =>
			{
				try {

					var tweets = await SearchTweetsAsync (query, cancellationToken);
					var e = new TweetsFetchedEventArgs (tweets);
					TweetsFetched?.Invoke (this, e);
				} catch (Exception ex) {


					var e = new TweetsFetchedFailedEventArgs (ex.Message);
					FailedTweetsFetched?.Invoke (this, e);

				}
			}, cancellationToken);
		}
		public async Task<List<Status>> SearchTweetsAsync (string query, CancellationToken cancelationToken)
		{

			if (string.IsNullOrWhiteSpace (query))
				return new List<Status> ();


			cancelationToken.ThrowIfCancellationRequested ();
			Search searchResponse = await
			    (from search in twitterContext.Search
			     where search.Type == SearchType.Search &&
			     search.Query == query &&
			     search.TweetMode == TweetMode.Extended
			     select search).SingleOrDefaultAsync ();

			cancelationToken.ThrowIfCancellationRequested ();

			return searchResponse?.Statuses;
			/*	Task.Factory.StartNew (async () => {
			//		try {
						var tweets = await SearchTweetsAsync (query,);
					} catch (Exception ex) {

			//		}
				},cancelationToken

				);*/

		}
		//1 configurar QAuth
		//Acounts and users
		//SingleUSERAUthorizer
	}
	public class TweetsFetchedEventArgs : EventArgs {
		public List<Status> Tweets { get; private set; }
		public TweetsFetchedEventArgs (List<Status> tweets)
		{
			Tweets = tweets;
		}

	}
	public class TweetsFetchedFailedEventArgs : EventArgs {
		//propiedad que se llama ErrorMesage
		public String ErrorMessage { get; private set; }
		public TweetsFetchedFailedEventArgs (String errorMessage)
		{
			ErrorMessage = errorMessage;
		}
	}
}
