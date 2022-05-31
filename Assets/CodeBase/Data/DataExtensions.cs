using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector)
        {
            return new Vector3Data(vector.x, vector.y, vector.z);
        }


        public static Vector3 AsUnityvector(this Vector3Data vectorData)
        {
            return new Vector3(vectorData.X,vectorData.Y,vectorData.Z);
        }

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T ToDeserealized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
