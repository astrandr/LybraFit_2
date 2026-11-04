//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;

namespace UIBase
{
	//Delegate type declaration to use for handler in each adapter class
	public delegate void ViewChangeEventHandler(object sender, UpdateMessage message);

	/// <summary>
	/// Summary description for ViewController.
	/// </summary>
	public class ViewController
	{
		//The event to raise if document changes are to be reflected in UI components
		public ViewChangeEventHandler OnUpdateRequest;

        //The event to raise if document changes are to be reflected in UI components
        public ViewChangeEventHandler OnBackgroundUpdateRequest;

        //The event is sent when UI is to be reinit and all messages in the adapter are to be cleared
        public EventHandler OnClearMessages;

        /// <summary>
        /// UpdateAllUI - Method to be called after changes in view settings or the document if from the UI thread. The method notifies all registered with this controller
        /// UI control adapters to update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message">message containing type and content of the update</param>
        public void UpdateAllUI(object sender, UpdateMessage message)
		{
            if (OnUpdateRequest != null)
			{
				//Add custom event args if necesary
                OnUpdateRequest(sender, message);
			}
		}

        /// <summary>
        /// UpdateAllUI - Method to be called after changes in view settings or the document if from the UI thread. The method notifies all registered with this controller
        /// UI control adapters to update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message">message containing type and content of the update</param>
        public void UpdateAllUI(object sender, int messageType, object content)
        {
            if (OnUpdateRequest != null)
            {
                //Add custom event args if necesary
                OnUpdateRequest(sender, new UpdateMessage(messageType, content));
            }
        }

        /// <summary>
        /// UpdateAllUI - Method to be called after changes in view settings or the document if from the UI thread. The method notifies all registered with this controller
        /// UI control adapters to update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message">message containing type and content of the update</param>
        public void BackgroundUpdateAllUI(object sender, int messageType, object content)
        {
            if (OnBackgroundUpdateRequest != null)
            {
                //Add custom event args if necesary
                OnBackgroundUpdateRequest(sender, new UpdateMessage(messageType, content));
            }
        }

        /// <summary>
        /// UpdateAllUI - Method to be called after changes in view settings or the document if from the UI thread. The method notifies all registered with this controller
        /// UI control adapters to update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message">message containing type and content of the update</param>
        public void BackgroundUpdateAllUI(object sender, UpdateMessage message)
        {
            if (OnBackgroundUpdateRequest != null)
            {
                //Add custom event args if necesary
                OnBackgroundUpdateRequest(sender, message);
            }
        }

        public void CleareMessages()
        {
            if(OnClearMessages != null)
            {
                OnClearMessages(this, null);
            }
        }
    }
}
