using UnityEditor;

namespace calco.Editor
{
    [CustomPropertyDrawer(typeof(quaternion))]
    class QuaternionDrawer : PostNormalizedVectorDrawer
    {
        protected override SerializedProperty GetVectorProperty(SerializedProperty property)
        {
            return property.FindPropertyRelative("value");
        }

        protected override float4 Normalize(float4 value)
        {
            return math.normalizesafe(new quaternion(value)).value;
        }
    }
}
