using UnityEngine;

namespace Game.Common
{
    public class JsonConfigsService : IConfigsService
    {
        public bool Load<T>(string path, out T value)
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>(path);
            if (jsonTextFile != null)
            {
                T config = JsonUtility.FromJson<T>(jsonTextFile.text);

                if (config != null)
                {
                    value = config;
                    return true;
                }
            }

            value = default(T);
            return false;
        }
    }
}