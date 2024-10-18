using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Wotakuro
{
    public class QuiltToRenderTexture : System.IDisposable
    {
        private Material material;

        private Shader shader;

        private RenderTexture rt;

        private DisplayData displayData;

        private QuiltViewInfo quiltInfo;

        private Mesh drawMesh;

        private CommandBuffer commandBuffer;


        private List<Vector3> positions;
        private List<Vector2> uvs;
        private List<int> indecies;

        // Start is called before the first frame update
        public void Initialize()
        {
            shader = Resources.Load<Shader>("Lenticular");
            material = new Material(shader);
            commandBuffer = new CommandBuffer();
            InitMesh();
        }

        private void InitMesh()
        {
            this.drawMesh = new Mesh();
            positions = new List<Vector3>();

            positions.Add( new Vector3(-1f, -1f, 1) ) ;
            positions.Add( new Vector3(-1f, 1f, 1) );
            positions.Add( new Vector3(1f, -1f, 1));
            positions.Add ( new Vector3(1f, 1f, 1));

            uvs.Add( new Vector2(0, 0));
            uvs.Add( new Vector2(0, 1));
            uvs.Add( new Vector2(1, 0));
            uvs.Add( new Vector2(1, 1));

            this.indecies = new List<int>();
            indecies.Add(0);
            indecies.Add(2);
            indecies.Add(1);
            indecies.Add(1);
            indecies.Add(2);
            indecies.Add(3);


            drawMesh.SetVertices(positions);
            drawMesh.SetUVs(0, uvs);
            drawMesh.SetIndices(indecies, MeshTopology.Triangles,0);
        }

        public void Setup(DisplayData dispData,QuiltViewInfo quilt)
        {
            if (rt)
            {
                rt.Release();
            }
            if(this.displayData == null)
            {
                rt = new RenderTexture(dispData.screenW, dispData.screenH, 0);
            }
            else if (dispData.screenW != this.displayData.screenW &&
               dispData.screenH != this.displayData.screenH)
            {
                rt = new RenderTexture(dispData.screenW, dispData.screenH, 0);
            }

            this.displayData = dispData;
            this.quiltInfo = quilt;
        }

        private void SetupMaterialData(Material mat,DisplayData dispData, QuiltViewInfo quilt)
        {
            mat.SetFloat(LenticularProperties.screenW, dispData.screenW);
            mat.SetFloat(LenticularProperties.screenH, dispData.screenH);
            mat.SetFloat(LenticularProperties.tileCount, quilt.tileCount);
            mat.SetFloat(LenticularProperties.pitch, 
                ProcessPitch(dispData.screenW,dispData.pitch,dispData.DPI,dispData.slope));

            mat.SetFloat(LenticularProperties.slope,
                ProcessSlope(dispData.screenW,dispData.screenH,dispData.slope,dispData.flipImageX));
            mat.SetFloat(LenticularProperties.center, dispData.center );
            mat.SetFloat(LenticularProperties.subpixelSize,
                (float)1 / (3 * dispData.screenW) * (dispData.flipImageX >= 0.5f ? -1 : 1));

            mat.SetVector(LenticularProperties.tile, new Vector4(
                quilt.tileXNum,
                quilt.tileYNum,
                quilt.tileCount,
                quilt.tileXNum * quilt.tileYNum
            ));
            mat.SetVector(LenticularProperties.viewPortion, new Vector4(
                quilt.ViewPortionHorizontal,
                quilt.ViewPortionVertical
            ));

            mat.SetVector(LenticularProperties.aspect, new Vector4(
                quilt.renderAspect,
                quilt.renderAspect
            ));

            bool shouldDimEdgeViews = true;

            mat.SetInt(LenticularProperties.filterMode, 1);
            mat.SetInt(LenticularProperties.cellPatternType, 0);

            mat.SetInt(LenticularProperties.filterEdge, shouldDimEdgeViews ? 1 : 0);
            mat.SetFloat(LenticularProperties.filterEnd, 0.05f);
            mat.SetFloat(LenticularProperties.filterSize, 0.15f);
            mat.SetFloat(LenticularProperties.gaussianSigma, 0.01f);
            mat.SetFloat(LenticularProperties.edgeThreshold, 0.01f);
            mat.SetInt(LenticularProperties.subpixelCellCount, 0);

        }

        public static float ProcessPitch(float screenW, float pitch, float dpi, float slope)
        {
            return pitch * screenW / dpi * Mathf.Cos(Mathf.Atan(1 / slope));
        }
        public static float ProcessSlope(float screenW, float screenH, float slope, float flipImageX)
        {
            return screenH / (screenW * slope) * (flipImageX >= 0.5f ? -1 : 1);
        }


        public RenderTexture RenderFromQuilt(Texture quilt)
        {
            commandBuffer.Clear();
            this.SetupMaterialData(this.material, this.displayData, this.quiltInfo);
            this.material.mainTexture = quilt;

            commandBuffer.SetRenderTarget(this.rt);
            commandBuffer.ClearRenderTarget(true,true,Color.white);
            commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
            commandBuffer.DrawMesh(this.drawMesh, Matrix4x4.identity, this.material);

            Graphics.ExecuteCommandBuffer(commandBuffer);
            return this.rt;
        }

        public void Dispose()
        {
            if (rt)
            {
                rt.Release();
                rt = null;
            }
        }
    }

}