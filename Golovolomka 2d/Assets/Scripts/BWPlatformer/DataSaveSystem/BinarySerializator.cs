using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BWPlatformer
{
	public static class BinarySerializator
	{
		public static void Serialize<T>(T saveData, Stream serializationStream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(serializationStream, saveData);
		}

		public static T Deserialize<T>(Stream serializationStream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			try
			{
				T data = (T)bf.Deserialize(serializationStream);
				return data;
			}
			catch
			{
				return default;
			}
		}
	}
}