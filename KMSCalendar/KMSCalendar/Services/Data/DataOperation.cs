using System;
using System.Threading;

namespace KMSCalendar.Services.Data
{
	public class DataOperation
	{
		//* Private Properties
		private dynamic function;
		private dynamic param;
		private dynamic data;

		//* Public Properties
		public EventWaitHandle WaitHandle { get; set; }

		//* Public Methods

		/// <summary>
		/// Attempts to get data from the backend, if it fails, it opens a network fail page.
		/// </summary>
		/// <typeparam name="TParam">The type of param. (i.e. Guid)</typeparam>
		/// <typeparam name="TResult">The return type of the function.</typeparam>
		/// <param name="function">The function used to get data from the backend.</param>
		/// <param name="param">The parameter passed into the backend function.</param>
		/// <returns>The data retrieved from the database.</returns>
		public TResult ConnectToBackend<TParam, TResult>(Func<TParam, TResult> function, TParam param)
		{
			data = null;
			this.function = function;
			this.param = param;

			if (TryToGetData())
				return data;

			return default;
		}

		public TParam ConnectToBackendWithoutParam<TParam>(Func<TParam> function)
		{
			data = null;
			param = null;
			this.function = function;

			if (TryToGetData())
				return data;

			return data;
		}

		/// <summary>
		/// Attempts to get data from the database, updates the data field if possible.
		/// </summary>
		/// <returns>Whether or not the database access was successful. (bool)</returns>
		public bool TryToGetData()
		{
			if (param != null)
			{
				try
				{
					data = function(param);
					return true;
				}
				catch { return false; }
			}
			else
			{
				try
				{
					data = function();
					return true;
				}
				catch { return false; }
			}
		}
	}
}