using System.Runtime.CompilerServices;
using Eldemarkki.VoxelTerrain.Settings;
using Unity.Burst;
using Unity.Mathematics;

namespace Eldemarkki.VoxelTerrain.VoxelData
{
    /// <summary>
    /// A procedural terrain voxel data calculation job
    /// </summary>
    [BurstCompile]
    public struct ProceduralTerrainVoxelDataCalculationJob : IVoxelDataGenerationJob
    {
        /// <summary>
        /// The procedural terrain generation settings
        /// </summary>
        public ProceduralTerrainSettings ProceduralTerrainSettings { get; set; }

        /// <inheritdoc/>
        public int3 WorldPositionOffset { get; set; }

        /// <inheritdoc/>
        public VoxelDataVolume<byte> OutputVoxelData { get; set; }

        /// <summary>
        /// The execute method required for Unity's IJobParallelFor job type
        /// </summary>
        public void Execute()
        {
            float3 p1 = new float3(-64, 80, -16);
            float3 p2 = new float3(-74, 80, -2);
            Capsule capsule = new Capsule(p1, p2, 7f);
            //int3 poco = new int3(-64,80,-16);
            //int3 poct = new int3(-74, 80, -2 );

            for (int x = 0; x < OutputVoxelData.Width; x++) {
                for (int z = 0; z < OutputVoxelData.Depth; z++) {


                    //float terrainNoise = OctaveNoise(terrainPosition.x,  terrainPosition.y, ProceduralTerrainSettings.NoiseFrequency * 0.001f, ProceduralTerrainSettings.NoiseOctaveCount, ProceduralTerrainSettings.NoiseSeed) * ProceduralTerrainSettings.Amplitude;


                    //    int2 terrainPosition = new int2(x + WorldPositionOffset.x, z + WorldPositionOffset.z);
                    for (int y = 0; y < OutputVoxelData.Height; y++) {
                        float3 terP = new float3(x + WorldPositionOffset.x, y + WorldPositionOffset.y, z + WorldPositionOffset.z);

                        //Debug.Log($"{altitude}, InRadius: {Boo}");

                        //float voxelDdata =(nearEnoughToPoc1 && || nearEnoughToPoc2|| ((altitude < radius) && (altitude > -radius)))&& correctY   ? 0f : 1f;
                        float voxelDdata = capsule.Contains(terP) ? 0f : 1f;
                        OutputVoxelData.SetVoxelData((byte)math.clamp(voxelDdata * 255, 0, 255), new int3(x, y, z));
                    }

                }
            }

            //for (int x = 0; x < OutputVoxelData.Width; x++)
            //{
            //    for (int z = 0; z < OutputVoxelData.Depth; z++)
            //    {
            //        int2 terrainPosition = new int2(x + WorldPositionOffset.x, z + WorldPositionOffset.z);
            //        float terrainNoise = OctaveNoise(terrainPosition.x, terrainPosition.y, ProceduralTerrainSettings.NoiseFrequency * 0.001f, ProceduralTerrainSettings.NoiseOctaveCount, ProceduralTerrainSettings.NoiseSeed) * ProceduralTerrainSettings.Amplitude;

            //        for (int y = 0; y < OutputVoxelData.Height; y++)
            //        {
            //            int3 worldPosition = new int3(terrainPosition.x, y + WorldPositionOffset.y, terrainPosition.y);

            //            float voxelData = (worldPosition.y - ProceduralTerrainSettings.HeightOffset - terrainNoise) * 0.5f;
            //            OutputVoxelData.SetVoxelData((byte)math.clamp(voxelData * 255, 0, 255), new int3(x, y, z));
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Calculates octave noise
        /// </summary>
        /// <param name="x">Sampling point's x position</param>
        /// <param name="y">Sampling point's y position</param>
        /// <param name="frequency">The frequency of the noise</param>
        /// <param name="octaveCount">How many layers of noise to combine</param>
        /// <returns>The sampled noise value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float OctaveNoise(float x, float y, float frequency, int octaveCount, int seed)
        {
            float value = 0;

            for (int i = 0; i < octaveCount; i++)
            {
                int octaveModifier = (int)math.pow(2, i);

                // (x+1)/2 because noise.snoise returns a value from -1 to 1 so it needs to be scaled to go from 0 to 1.
                float pureNoise = (noise.snoise(new float3(octaveModifier * x * frequency, octaveModifier * y * frequency, seed)) + 1) / 2f;
                value += pureNoise / octaveModifier;
            }

            return value;
        }
    }
}