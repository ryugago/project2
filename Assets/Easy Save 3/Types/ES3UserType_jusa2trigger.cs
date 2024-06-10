using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("jusa", "jusaimg", "jusaobj", "blurEffect", "mental_breakdown", "hand3ma", "hand3ren", "trigger2", "before", "after", "clip")]
	public class ES3UserType_jusa2trigger : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_jusa2trigger() : base(typeof(jusa2trigger)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (jusa2trigger)obj;
			
			writer.WritePropertyByRef("jusa", instance.jusa);
			writer.WritePropertyByRef("jusaimg", instance.jusaimg);
			writer.WritePropertyByRef("jusaobj", instance.jusaobj);
			writer.WritePropertyByRef("blurEffect", instance.blurEffect);
			writer.WritePropertyByRef("mental_breakdown", instance.mental_breakdown);
			writer.WritePropertyByRef("hand3ma", instance.hand3ma);
			writer.WritePropertyByRef("hand3ren", instance.hand3ren);
			writer.WritePrivateField("trigger2", instance);
			writer.WritePropertyByRef("before", instance.before);
			writer.WritePropertyByRef("after", instance.after);
			writer.WritePropertyByRef("clip", instance.clip);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (jusa2trigger)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "jusa":
						instance.jusa = reader.Read<UnityEngine.Animator>();
						break;
					case "jusaimg":
						instance.jusaimg = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "jusaobj":
						instance.jusaobj = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "blurEffect":
						instance.blurEffect = reader.Read<NovaSamples.Effects.BlurEffect>();
						break;
					case "mental_breakdown":
						instance.mental_breakdown = reader.Read<UnityEngine.Material>(ES3Type_Material.Instance);
						break;
					case "hand3ma":
						instance.hand3ma = reader.Read<UnityEngine.Material>(ES3Type_Material.Instance);
						break;
					case "hand3ren":
						instance.hand3ren = reader.Read<UnityEngine.Renderer>();
						break;
					case "trigger2":
					instance = (jusa2trigger)reader.SetPrivateField("trigger2", reader.Read<System.Boolean>(), instance);
					break;
					case "before":
						instance.before = reader.Read<jusa2before>();
						break;
					case "after":
						instance.after = reader.Read<jusa2after>();
						break;
					case "clip":
						instance.clip = reader.Read<UnityEngine.AudioClip>(ES3Type_AudioClip.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_jusa2triggerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_jusa2triggerArray() : base(typeof(jusa2trigger[]), ES3UserType_jusa2trigger.Instance)
		{
			Instance = this;
		}
	}
}