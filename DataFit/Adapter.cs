//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace UIBase
{
	/// <summary>
	/// Summary description for Adapter.
	/// </summary>
	public class Adapter
	{
		//The UI control for this adapter handles
		protected Control			control = null;
		
		//The document with data to represent in the adapter
		protected ViewController	controller = null;
		
		//Accumulated updates
		protected List<UpdateMessage> updates = new List<UpdateMessage>();

		/// <summary>
		/// Attach - Static method to create adapter and attach it to UI control and document. Can be used no adapter variable are needed in the calling module.
		/// </summary>
		/// <param name="document">instance of document inheriting object</param>
		/// <param name="control">instance of the UI control this adapter will operate</param>
		/// <param name="adapterName">Full type name of the adapter to be created. Must be declared within the same assambly</param>
		public static void Attach(ViewController controller, Control control, string adapterName)
		{
            Debug.Assert(controller != null, "NULL ViewController passed to adapter construction");
            Debug.Assert(controller != null, "NULL value for the control passed in the adapter construction");

            Assembly assembly = Assembly.GetCallingAssembly();
			Type adapterType = assembly.GetType(adapterName);
			if (adapterType != null)
			{
				ConstructorInfo constructor = adapterType.GetConstructor(new Type[] { typeof(Control), typeof(ViewController)});
				if (constructor != null)
				{
                    constructor.Invoke(new object[] { control, controller});
					return;
				}
                else
                {
                    throw new System.ArgumentException("Can create constructor for adapter of type: " + adapterName);
                }
			}
            else
            {
                throw new System.ArgumentException("Can not find adapter of type: " + adapterName);
            }

			throw new System.ArgumentException("Can not create adapter of type: "+adapterName);
		}

		/// <summary>
		/// Adapter - Public cosntructor. If no variable is needed for the created adapter in the calling module, use the Attach method isntead.
		/// </summary>
		/// <param name="document">instance of document inheriting object</param>
		/// <param name="control">instance of the UI control this adapter will operate</param>
		public Adapter(System.Windows.Forms.Control control, ViewController controller)
		{
			Debug.Assert(controller != null, "NULL ViewController passed to adapter construction");
			Debug.Assert(control != null, "NULL value for the control passed in the adapter construction");

			this.control = control;
			this.controller = controller;

			//Call virtual method of inheritant class to initialize its members
			Initialize();

			//Attach handlers
			
			//Add VisibleChange event handler. It will be called if control needs to synchronize with the document.
			control.VisibleChanged += new EventHandler(this.OnVisibleChanged);

            //Initialize the document member and a handler to its OnUpdateRequest event, Register the adapter with the controller.
            ViewChangeEventHandler handler = new ViewChangeEventHandler(this.Update);
			controller.OnUpdateRequest += handler;

            //Initialize the document member and a handler to its OnUpdateRequest event, Register the adapter with the controller.
            ViewChangeEventHandler bkHandler = new ViewChangeEventHandler(this.UpdateFromBackground);
            controller.OnBackgroundUpdateRequest += bkHandler;

            controller.OnClearMessages += new EventHandler(ClearMessages);
        }

        private void ClearMessages(object sender, EventArgs args)
        {
            updates.Clear();
        }

		/// <summary>
		/// Initialize - virtual method to call so that inheritant class can do additional initialization.
		/// </summary>
		protected virtual void Initialize(){}

		/// <summary>
		/// OnVisibleChange - method handle if control changes its state to visible and there is a 
		/// pending update to synchronize with the document
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OnVisibleChanged(object sender, EventArgs args)
		{
			//Update if this UI control adapter is marked for update
			if (updates.Count > 0)
			{
				for(int i=0; i<updates.Count;i++)
				{
                    try
                    {
                        SynchronizeWithDocument((updates[i] as UpdateMessage));
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError("Cannot update " + this.GetType().FullName + " \r\n" + e);
                    }
				}
				updates.Clear();
			}				
		}

        private delegate void BackgroundUpdateRequestHandler(UpdateMessage message);

		private void Update(object sender, UpdateMessage message)
		{
			//Do not register for update if they come from this control
			if (sender == this.control || sender == this) return;

			//Optimization for UI performance: Update control only if it is visible, otherwise only mark it for update.
			if (true/*control.Visible*/) //TODO: This optimization is to optional
			{
                try
                {
                    this.SynchronizeWithDocument(message);
                }
                catch (Exception e)
                {
                    Trace.TraceError("Cannot update " + this.GetType().FullName + " \r\n" + e);
                }

			}
			else
			{
				if (updates.Contains(message))
				{
					int index = updates.IndexOf(message);
					updates.RemoveAt(index);
				};
                updates.Add(message);
			}
		}

        private void UpdateFromBackground(object sender, UpdateMessage message)
        {
            //Do not register for update if they come from this control
            if (sender == this.control || sender == this) return;

            //Optimization for UI performance: Update control only if it is visible, otherwise only mark it for update.
            if (control.Visible)
            {
                try
                {
                    BackgroundUpdateRequestHandler handler = new BackgroundUpdateRequestHandler(this.SynchronizeWithDocument);
                    control.BeginInvoke(handler, new object[] { message });
                }
                catch (Exception e)
                {
                    Trace.TraceError("Cannot update " + this.GetType().FullName + " \r\n" + e);
                }

            }
            else
            {
                if (updates.Contains(message))
                {
                    int index = updates.IndexOf(message);
                    updates.RemoveAt(index);
                };
                updates.Add(message);
            }
        }

		/// <summary>
		/// SynchronizeWithDocument - Method to be implemented by the derived class to handle document updates and
		/// sunchronize the UI control with it. This method is called as of user calling UpdateAllUI on document object
		/// or if control becomes visible and there are pending sunchronizations as result of UpdateAllUI call. The method
		/// is not called if the attached UI control has made changes and called UpdateAllUI on the document passing itself
		/// or the this adapter as argument.
		/// </summary>
		/// <param name="documentUpdateInfo"></param>
		protected virtual void SynchronizeWithDocument(UpdateMessage update)
		{
		}


	}
}
