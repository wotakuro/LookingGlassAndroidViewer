using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wotakuro
{
    [System.Serializable]
    public class DisplayData
    {
        [SerializeField]
        public string serial;
        [SerializeField]
        public float pitch;
        [SerializeField]
        public float slope;
        [SerializeField]
        public float center;
        [SerializeField]
        public float fringe;
        [SerializeField]
        public float viewCone;
        [SerializeField]
        public int invView;
        [SerializeField]
        public float verticalAngle;
        [SerializeField]
        public float DPI;
        [SerializeField]
        public int screenW;
        [SerializeField]
        public int screenH;
        [SerializeField]
        public int flipImageX;
        [SerializeField]
        public int flipImageY;
        [SerializeField]
        public int flipSubp;
    }


    [System.Serializable]
    public class OriginalDisplayJson
    {
        [System.Serializable]
        public class IntProperty
        {
            [SerializeField]
            public int value;
        }
        [System.Serializable]
        public class FloatProperty
        {
            [SerializeField]
            public float value;
        }
        [System.Serializable]
        public class StringProperty
        {
            [SerializeField]
            public string value;
        }
        [SerializeField]
        public string configVersion;

        [SerializeField]
        public string serial;

        [SerializeField]
        public FloatProperty pitch;
        [SerializeField]
        public FloatProperty slope;
        [SerializeField]
        public FloatProperty center;
        [SerializeField]
        public FloatProperty fringe;
        [SerializeField]
        public FloatProperty viewCone;
        [SerializeField]
        public IntProperty invView;
        [SerializeField]
        public FloatProperty verticalAngle;
        [SerializeField]
        public FloatProperty DPI;
        [SerializeField]
        public IntProperty screenW;
        [SerializeField]
        public IntProperty screenH;
        [SerializeField]
        public IntProperty flipImageX;
        [SerializeField]
        public IntProperty flipImageY;
        [SerializeField]
        public IntProperty flipSubp;

        public static OriginalDisplayJson LoadFromFile(string path)
        {
            var fileStr = System.IO.File.ReadAllText(path);
            var obj = JsonUtility.FromJson<OriginalDisplayJson>(fileStr);
            return obj;
        }
        public static OriginalDisplayJson CreateFromString(string str)
        {
            var obj = JsonUtility.FromJson<OriginalDisplayJson>(str);
            return obj;
        }


        public DisplayData ConvertToData()
        {
            OriginalDisplayJson original = this;
            DisplayData data = new DisplayData();
            data.serial = original.serial;
            data.pitch = original.pitch.value;

            data.slope = original.slope.value;
            data.center = original.center.value;
            data.fringe = original.fringe.value;

            data.viewCone = original.viewCone.value;
            data.invView = original.invView.value;
            data.verticalAngle = original.verticalAngle.value;

            data.DPI = original.DPI.value;
            data.screenW = original.screenW.value;
            data.screenH = original.screenH.value;
            data.flipImageX = original.flipImageX.value;
            data.flipImageY = original.flipImageY.value;
            data.flipSubp = original.flipSubp.value;

            return data;
        }

    }
}