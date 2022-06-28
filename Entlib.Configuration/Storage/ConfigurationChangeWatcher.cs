//===============================================================================
// Microsoft patterns &	practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS	CODE AND INFORMATION IS	PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND,	EITHER EXPRESSED OR	IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF	MERCHANTABILITY	AND
// FITNESS FOR A PARTICULAR	PURPOSE.
//===============================================================================

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
	/// <summary>
	/// <para>Represents an <see cref="IConfigurationChangeWatcher"/> that watches a file.</para>
	/// </summary>
	public abstract class ConfigurationChangeWatcher : IConfigurationChangeWatcher
	{
		private static readonly object configurationChangedKey = new object();
		private static int defaultPollDelayInMilliseconds = 15000;

		private int pollDelayInMilliseconds = defaultPollDelayInMilliseconds;
		private Thread pollingThread;
		private EventHandlerList eventHandlers = new EventHandlerList();
		private DateTime lastWriteTime;
		private bool polling;
		private object lockObj = new object();

		#region Testing only methods

#if  UNIT_TESTS || LONG_RUNNING_TESTS
		internal static void SetDefaultPollDelayInMilliseconds(int newDefaultPollDelayInMilliseconds)
		{
			defaultPollDelayInMilliseconds = newDefaultPollDelayInMilliseconds;
		}

		internal static void ResetDefaultPollDelay()
		{
			defaultPollDelayInMilliseconds = 15000;
		}

		internal void SetPollDelayInMilliseconds(int newDelayInMilliseconds)
		{
			pollDelayInMilliseconds = newDelayInMilliseconds;
		}
#endif
		#endregion

		/// <summary>
		/// <para>Initialize a new <see cref="ConfigurationChangeWatcher"/> class</para>
		/// </summary>
		public ConfigurationChangeWatcher()
		{
		}

		/// <summary>
		/// <para>Allows an <see cref="ConfigurationChangeFileWatcher"/> to attempt to free resources and perform other cleanup operations before the <see cref="ConfigurationChangeFileWatcher"/> is reclaimed by garbage collection.</para>
		/// </summary>
		~ConfigurationChangeWatcher()
		{
			Disposing(false);
		}

		/// <summary>
		/// Event raised when the underlying persistence mechanism for configuration notices that
		/// the persistent representation of configuration information has changed.
		/// </summary>
		public event ConfigurationChangedEventHandler ConfigurationChanged
		{
			add { eventHandlers.AddHandler(configurationChangedKey, value); }
			remove { eventHandlers.RemoveHandler(configurationChangedKey, value); }
		}

		/// <summary>
		/// <para>Gets the name of the configuration section being watched.</para>
		/// </summary>
		/// <value>
		/// <para>The name of the configuration section being watched.</para>
		/// </value>
		public abstract string SectionName
		{
			get;
		}

		/// <summary>
		/// <para>Starts watching the configuration file.</para>
		/// </summary>
		public void StartWatching()
		{
			lock (lockObj)
			{
				if (pollingThread == null)
				{
					polling = true;
					pollingThread = new Thread(new ThreadStart(Poller));
					pollingThread.IsBackground = true;
					pollingThread.Name = this.BuildThreadName();
					pollingThread.Start();
				}
			}
		}

		/// <summary>
		/// <para>Stops watching the configuration file.</para>
		/// </summary>
		public void StopWatching()
		{
			lock (lockObj)
			{
				if (pollingThread != null)
				{
					polling = false;
					pollingThread = null;
				}
			}
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="ConfigurationChangeFileWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		public void Dispose()
		{
			Disposing(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="ConfigurationChangeFileWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		/// <param name="isDisposing">
		/// <para><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</para>
		/// </param>
		protected virtual void Disposing(bool isDisposing)
		{
			if (isDisposing)
			{
				eventHandlers.Dispose();
				StopWatching();
			}
		}

		/// <summary>
		/// <para>Raises the <see cref="ConfigurationChanged"/> event.</para>
		/// </summary>
		protected virtual void OnConfigurationChanged()
		{
			ConfigurationChangedEventHandler callbacks = (ConfigurationChangedEventHandler)eventHandlers[configurationChangedKey];
			ConfigurationChangedEventArgs eventData = this.BuildEventData();

			try
			{
				if (callbacks != null)
				{
					foreach (ConfigurationChangedEventHandler callback in callbacks.GetInvocationList())
					{
						if (callback != null)
						{
							callback(this, eventData);
						}
					}
				}
			}
			catch (Exception e)
			{
				EventLog.WriteEntry(GetEventSourceName(), SR.ExceptionEventRaisingFailed + GetType().FullName + " :" + e.Message);
			}
			catch
			{
				EventLog.WriteEntry(GetEventSourceName(), SR.ExceptionEventRaisingFailed + GetType().FullName + " :" + SR.ExceptionUnknown);
			}
		}

		/// <summary>
		/// <para>Returns the <see cref="DateTime"/> of the last change of the information watched</para>
		/// </summary>
		/// <returns>The <see cref="DateTime"/> of the last modificaiton, or <code>DateTime.MinValue</code> if the information can't be retrieved</returns>
		protected abstract DateTime GetCurrentLastWriteTime();

		/// <summary>
		/// Returns the string that should be assigned to the thread used by the watcher
		/// </summary>
		/// <returns>The name for the thread</returns>
		protected abstract string BuildThreadName();

		/// <summary>
		/// Builds the change event data, in a suitable way for the specific watcher implementation
		/// </summary>
		/// <returns>The change event information</returns>
		protected abstract ConfigurationChangedEventArgs BuildEventData();

		/// <summary>
		/// Returns the source name to use when logging events
		/// </summary>
		/// <returns>The event source name</returns>
		protected abstract string GetEventSourceName();

		private void Poller()
		{
			lastWriteTime = DateTime.MinValue;
			DateTime currentLastWriteTime = DateTime.MinValue;

			while (polling)
			{
				currentLastWriteTime = GetCurrentLastWriteTime();
				if (currentLastWriteTime != DateTime.MinValue)
				{
					if (lastWriteTime.Equals(DateTime.MinValue))
					{
						lastWriteTime = currentLastWriteTime;
					}
					else
					{
						if (lastWriteTime.Equals(currentLastWriteTime) == false)
						{
							lastWriteTime = currentLastWriteTime;
							OnConfigurationChanged();
						}
					}
				}
				Thread.Sleep(pollDelayInMilliseconds);		
			}
		}
	}
}