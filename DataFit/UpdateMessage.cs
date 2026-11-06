//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Diagnostics;

namespace UIBase
{
	/// <summary>
	/// Summary description for UpdateMessage.
	/// </summary>
	public class UpdateMessage 
	{
		public int ID;
		public int Type;
		public object Content;

		public UpdateMessage()
		{
		}
		
        public UpdateMessage(int Type, object content)
		{
			this.Type = Type;
			this.Content = content;
		}

        public override bool Equals(object obj)
        {
            Debug.Assert(obj is UpdateMessage, "Invalid object passed to UpdateMessage Equals");

            UpdateMessage message = (UpdateMessage)obj;
            return this.Type == message.Type;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
