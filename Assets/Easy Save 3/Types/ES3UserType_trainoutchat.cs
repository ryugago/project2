using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("enble")]
	public class ES3UserType_trainoutchat : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_trainoutchat() : base(typeof(trainoutchat)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (trainoutchat)obj;
			
			writer.WriteProperty("enble", instance.enble, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (trainoutchat)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "enble":
						instance.enble = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_trainoutchatArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_trainoutchatArray() : base(typeof(trainoutchat[]), ES3UserType_trainoutchat.Instance)
		{
			Instance = this;
		}
	}
}