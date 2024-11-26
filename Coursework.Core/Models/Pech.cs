using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Core.Models
{
    public class Pech
    {
        private Pech(Guid id, float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float KTeploOtd)
        {
            Id = id;
            this.TPech = tPech;
            this.Height = height;
            this.TNach = tNach;
            this.KTeplo = kTeplo;
            this.Teplo = Teplo;
            this.P = p;
            this.TPov = tPov;
            this.KTeploOtd = KTeploOtd;
            this.VremNagr = Pech.CalculateVremNagr(TPech, Height, TNach, KTeplo, this.Teplo, P, TPov, this.KTeploOtd);
            this.T = Pech.CalculateTKon(TPech, Height, TNach, KTeplo, this.Teplo, P, TPov, this.KTeploOtd);
        }

        public Guid Id { get; set; }
        public float TPech { get; set; }
        public float Height { get; set; }
        public float TNach { get; set; }
        public float KTeplo { get; set; }
        public float Teplo { get; set; }
        public float P { get; set; }
        public float TPov { get; set; }
        public float KTeploOtd { get; set; }
        public float VremNagr { get; set; }
        public float T { get; set; }
        static public float[,] Tabla = new float[25, 5]
        {
            {0.000f,0.000f,1.000f,1.000f,1.000f},
            {0.010f,0.010f,0.997f,1.000f,1.0002f},
            {0.020f,0.020f,0.993f,1.000f,1.003f},
            {0.030f,0.030f,0.990f,1.000f,1.005f},
            {0.040f,0.039f,0.987f,1.000f,1.007f},
            {0.050f,0.049f,0.984f,1.000f,1.008f},
            {0.060f,0.059f,0.980f,1.000f,1.010f},
            {0.070f,0.068f,0.977f,1.000f,1.011f},
            {0.080f,0.078f,0.974f,1.000f,1.013f},
            {0.090f,0.087f,0.971f,1.000f,1.015f},
            {0.100f,0.097f,0.967f,1.000f,1.016f},
            {0.200f,0.187f,0.936f,0.999f,1.031f},
            {0.300f,0.272f,0.906f,0.998f,1.045f},
            {0.400f,0.352f,0.877f,0.997f,1.058f},
            {0.500f,0.427f,0.850f,0.996f,1.070f},
            {0.600f,0.497f,0.824f,0.994f,1.081f},
            {0.700f,0.563f,0.799f,0.992f,1.092f},
            {0.800f,0.626f,0.775f,0.990f,1.102f},
            {0.900f,0.685f,0.752f,0.988f,1.111f},
            {1.000f,0.740f,0.730f,0.986f,1.119f},
            {1.500f,0.977f,0.635f,0.975f,1.154f},
            {2.000f,1.160f,0.559f,0.963f,1.178f},
            {3.000f,1.422f,0.447f,0.943f,1.210f},
            {4.000f,1.599f,0.370f,0.926f,1.229f},
            {5.000f,1.726f,0.315f,0.913f,1.240f},
        };
        public static (Pech pech, string Error) Create(Guid id, float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float KTeploOtd)
        {
            var error = string.Empty;
            var pech = new Pech(id, tPech, height, tNach, kTeplo, Teplo, p, tPov, KTeploOtd);
            return (pech, error);
        }
        public static float CalculateVremNagr(float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float KTeploOtd) 
        {
            float otnRaz = (tPech - tPov) / (tPech - tNach);
            float Bi = (KTeploOtd * height) / (kTeplo * 2);
            float kTemper = (kTeplo) / (Teplo * p);
            float BiUp = 0;
            float BiDown = 0;
            int index = 0;
            if (Bi > 0 && Bi < 5)
            {
                for (int i = 0; i < Pech.Tabla.GetLength(0) - 1; i++)
                {
                    BiUp = Pech.Tabla[i, 0];
                    BiDown = Pech.Tabla[i + 1, 0];
                    index = i;
                    if (BiUp < Bi && BiDown > Bi)
                        break;
                }
            }
            else
                return 0;
            float Pc = Pech.Tabla[index, 2] + ((Bi - BiUp) / (BiDown - BiUp)) * (Pech.Tabla[index + 1, 2] - Pech.Tabla[index, 2]);
            float Nc = Pech.Tabla[index, 4] + ((Bi - BiUp) / (BiDown - BiUp)) * (Pech.Tabla[index + 1, 4] - Pech.Tabla[index, 4]);
            float Uc = Pech.Tabla[index, 1] + ((Bi - BiUp) / (BiDown - BiUp)) * (Pech.Tabla[index + 1, 1] - Pech.Tabla[index, 1]);
            float vrem = (height * height) / (4 * kTemper * Uc) * (float)Math.Log(Pc / otnRaz);
            return vrem;
        }
        public static float CalculateTKon(float tPech, float height, float tNach, float kTeplo, float Teplo, float p, float tPov, float KTeploOtd)
        {
            float otnRaz = (tPech - tPov) / (tPech - tNach);
            float Bi = (KTeploOtd * height) / (kTeplo * 2);
            float kTemper = (kTeplo) / (Teplo * p);
            float BiUp = 0;
            float BiDown = 0;
            int index = 0;
            if (Bi > 0 && Bi < 5)
            {
                for (int i = 0; i < Pech.Tabla.GetLength(0) - 1; i++)
                {
                    BiUp = Pech.Tabla[i, 0];
                    BiDown = Pech.Tabla[i + 1, 0];
                    index = i;
                    if (BiUp < Bi && BiDown > Bi)
                        break;
                }
            }
            else
                return 0;
            float Pc = Pech.Tabla[index, 2] + ((Bi - BiUp) / (BiDown - BiUp)) * (Pech.Tabla[index + 1, 2] - Pech.Tabla[index, 2]);
            float Nc = Pech.Tabla[index, 4] + ((Bi - BiUp) / (BiDown - BiUp)) * (Pech.Tabla[index + 1, 4] - Pech.Tabla[index, 4]);
            float Uc = Pech.Tabla[index, 1] + ((Bi - BiUp) / (BiDown - BiUp)) * (Pech.Tabla[index + 1, 1] - Pech.Tabla[index, 1]);
            float vrem = (height * height) / (4 * kTemper * Uc) * (float)Math.Log(Pc / otnRaz);

            float fure = (4 * kTemper * vrem) / (height * height);
            float tRazn = Nc*(float)Math.Exp(-Uc * fure);
            float t = tPech - tRazn * (tPech - tNach);
            return t;
        }
    }
}
