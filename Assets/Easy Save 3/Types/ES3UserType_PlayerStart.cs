using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("ChatText", "delayTime", "autoProceedDelay", "skipButton", "writerText", "start_camera", "playerct", "image", "targettxt", "player", "enabl")]
	public class ES3UserType_PlayerStart : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerStart() : base(typeof(PlayerStart)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayerStart)obj;
			
			writer.WritePropertyByRef("ChatText", instance.ChatText);
			writer.WriteProperty("delayTime", instance.delayTime, ES3Type_float.Instance);
			writer.WriteProperty("autoProceedDelay", instance.autoProceedDelay, ES3Type_float.Instance);
			writer.WriteProperty("skipButton", instance.skipButton, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<UnityEngine.KeyCode>)));
			writer.WriteProperty("writerText", instance.writerText, ES3Type_string.Instance);
			writer.WritePropertyByRef("start_camera", instance.start_camera);
			writer.WriteProperty("playerct", instance.playerct, ES3Type_bool.Instance);
			writer.WritePropertyByRef("image", instance.image);
			writer.WritePropertyByRef("targettxt", instance.targettxt);
			writer.WritePropertyByRef("player", instance.player);
			writer.WritePrivateField("enabl", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerStart)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "ChatText":
						instance.ChatText = reader.Read<TMPro.TMP_Text>();
						break;
					case "delayTime":
						instance.delayTime = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "autoProceedDelay":
						instance.autoProceedDelay = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "skipButton":
						instance.skipButton = reader.Read<System.Collections.Generic.List<UnityEngine.KeyCode>>();
						break;
					case "writerText":
						instance.writerText = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "start_camera":
						instance.start_camera = reader.Read<UnityEngine.Animator>();
						break;
					case "playerct":
						instance.playerct = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "image":
						instance.image = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "targettxt":
						instance.targettxt = reader.Read<TMPro.TMP_Text>();
						break;
					case "player":
						instance.player = reader.Read<PlayerController>();
						break;
					case "enabl":
					instance = (PlayerStart)reader.SetPrivateField("enabl", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerStartArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerStartArray() : base(typeof(PlayerStart[]), ES3UserType_PlayerStart.Instance)
		{
			Instance = this;
		}
	}
}