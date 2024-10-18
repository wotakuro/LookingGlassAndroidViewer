
using UnityEngine;


namespace Wotakuro
{
    internal class LenticularProperties
    {

        public static readonly int pitch = Shader.PropertyToID("pitch");
        public static readonly int slope = Shader.PropertyToID("slope");
        public static readonly int center = Shader.PropertyToID("center");
        public static readonly int subpixelSize = Shader.PropertyToID("subpixelSize");
        public static readonly int screenW = Shader.PropertyToID("screenW");
        public static readonly int screenH = Shader.PropertyToID("screenH");
        public static readonly int tileCount = Shader.PropertyToID("tileCount");
        public static readonly int viewPortion = Shader.PropertyToID("viewPortion");
        public static readonly int tile = Shader.PropertyToID("tile");

        public static readonly int subpixelCellCount = Shader.PropertyToID("subpixelCellCount");
        public static readonly int subpixelCells = Shader.PropertyToID("subpixelCells");
        public static readonly int filterMode = Shader.PropertyToID("filterMode");
        public static readonly int cellPatternType = Shader.PropertyToID("cellPatternType");

        public static readonly int filterEdge = Shader.PropertyToID("filterEdge");
        public static readonly int filterEnd = Shader.PropertyToID("filterEnd");
        public static readonly int filterSize = Shader.PropertyToID("filterSize");

        public static readonly int gaussianSigma = Shader.PropertyToID("gaussianSigma");
        public static readonly int edgeThreshold = Shader.PropertyToID("edgeThreshold");

        public static readonly int aspect = Shader.PropertyToID("aspect");
    }

}